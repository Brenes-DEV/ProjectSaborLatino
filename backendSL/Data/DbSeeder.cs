using Microsoft.AspNetCore.Identity;
using ProjectSaborLatino.Models;

namespace ProjectSaborLatino.Data;

// Crea los 4 roles y el primer Superadministrador al iniciar la API. No duplica nada.
public static class DbSeeder
{
    public static async Task SembrarAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DbSeeder");

        // 1. Roles
        foreach (var rol in Roles.Todos)
        {
            if (!await roleManager.RoleExistsAsync(rol))
            {
                await roleManager.CreateAsync(new IdentityRole(rol));
            }
        }

        // 2. Superadministrador (los datos vienen de Secretos de usuario)
        var email = config["SuperAdmin:Email"];
        var password = config["SuperAdmin:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning("Falta SuperAdmin:Email o SuperAdmin:Password. No se creó el Superadministrador.");
            return;
        }

        if (await userManager.FindByEmailAsync(email) is not null)
        {
            return;
        }

        var superAdmin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            NombreCompleto = "Superadministrador"
        };

        var resultado = await userManager.CreateAsync(superAdmin, password);
        if (!resultado.Succeeded)
        {
            logger.LogError("No se pudo crear el Superadministrador: {Errores}",
                string.Join("; ", resultado.Errors.Select(e => e.Description)));
            return;
        }

        await userManager.AddToRoleAsync(superAdmin, Roles.Superadministrador);
        logger.LogInformation("Superadministrador creado: {Email}", email);
    }
}