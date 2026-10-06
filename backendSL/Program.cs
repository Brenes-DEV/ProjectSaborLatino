using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Data;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

var builder = WebApplication.CreateBuilder(args);

// =============== Servicios ===============

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Errores en JSON estándar (ProblemDetails)
builder.Services.AddProblemDetails();

// Base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity: usuarios, roles y login con token
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

// Services del catálogo (Fase 1)
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IPaqueteService, PaqueteService>();
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IImagenGaleriaService, ImagenGaleriaService>();
builder.Services.AddScoped<IPreguntaFrecuenteService, PreguntaFrecuenteService>();

// CORS para el frontend React
const string PoliticaFrontend = "Frontend";
var origenesPermitidos = builder.Configuration
    .GetSection("Cors:OrigenesPermitidos").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(PoliticaFrontend, policy =>
        policy.WithOrigins(origenesPermitidos)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// =============== Datos iniciales ===============

await DbSeeder.SembrarAsync(app.Services);

// =============== Pipeline HTTP (el orden importa) ===============

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(PoliticaFrontend);
app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/identity").MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();