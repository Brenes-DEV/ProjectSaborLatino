using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Models;

namespace ProjectSaborLatino.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // =============== Catálogo (Fase 1) ===============
    // Cada DbSet se convierte en una tabla de la base de datos

    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<Paquete> Paquetes => Set<Paquete>();
    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<PaqueteEquipo> PaquetesEquipos => Set<PaqueteEquipo>();
    public DbSet<ImagenGaleria> ImagenesGaleria => Set<ImagenGaleria>();
    public DbSet<PreguntaFrecuente> PreguntasFrecuentes => Set<PreguntaFrecuente>();

    // =============== Solicitudes y cotizaciones (Fase 2) ===============

    public DbSet<Solicitud> Solicitudes => Set<Solicitud>();
    public DbSet<Cotizacion> Cotizaciones => Set<Cotizacion>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Identity necesita configurar sus propias tablas primero
        base.OnModelCreating(builder);

        builder.Entity<Solicitud>(e =>
        {
            // El estado se guarda como texto ("Pendiente") y no como número (0)
            e.Property(s => s.Estado).HasConversion<string>().HasMaxLength(20);

            // Si se borra el usuario, la solicitud queda sin dueño pero no se pierde
            e.HasOne(s => s.ClienteUsuario)
                .WithMany()
                .HasForeignKey(s => s.ClienteUsuarioId)
                .OnDelete(DeleteBehavior.SetNull);

            // No se puede borrar un paquete que tiene solicitudes
            e.HasOne(s => s.Paquete)
                .WithMany()
                .HasForeignKey(s => s.PaqueteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Cotizacion>(e =>
        {
            e.Property(c => c.Estado).HasConversion<string>().HasMaxLength(20);

            // Si se borra la solicitud, se borran sus cotizaciones
            e.HasOne(c => c.Solicitud)
                .WithMany(s => s.Cotizaciones)
                .HasForeignKey(c => c.SolicitudId)
                .OnDelete(DeleteBehavior.Cascade);

            // No se puede borrar un usuario que creó cotizaciones
            e.HasOne(c => c.CreadaPorUsuario)
                .WithMany()
                .HasForeignKey(c => c.CreadaPorUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}