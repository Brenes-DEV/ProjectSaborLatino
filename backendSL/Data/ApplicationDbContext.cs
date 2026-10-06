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
}