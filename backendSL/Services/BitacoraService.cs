using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Data;
using ProjectSaborLatino.DTOs.Sistema;
using ProjectSaborLatino.Models;

namespace ProjectSaborLatino.Services;

public interface IBitacoraService
{
    Task RegistrarAsync(string? usuarioId, string accion, string entidad, string? entidadId, string? detalle);
    Task<List<BitacoraDto>> ObtenerAsync(DateTime? desde, DateTime? hasta, string? usuarioId, string? entidad);
}

public class BitacoraService : IBitacoraService
{
    // Para no devolver miles de filas de una vez
    private const int MaximoFilas = 500;

    private readonly ApplicationDbContext _db;

    public BitacoraService(ApplicationDbContext db)
    {
        _db = db;
    }

    // Registro manual, para lo que la bitácora automática no ve (por ejemplo, cambios de usuarios)
    public async Task RegistrarAsync(string? usuarioId, string accion, string entidad, string? entidadId, string? detalle)
    {
        _db.Bitacora.Add(new Bitacora
        {
            UsuarioId = usuarioId,
            Accion = accion,
            Entidad = entidad,
            EntidadId = entidadId,
            Detalle = detalle
        });

        await _db.SaveChangesAsync();
    }

    // Más recientes primero; todos los filtros son opcionales
    public async Task<List<BitacoraDto>> ObtenerAsync(DateTime? desde, DateTime? hasta, string? usuarioId, string? entidad)
    {
        var consulta = _db.Bitacora.AsQueryable();

        if (desde is not null)
        {
            consulta = consulta.Where(b => b.Fecha >= desde);
        }

        if (hasta is not null)
        {
            consulta = consulta.Where(b => b.Fecha < hasta);
        }

        if (!string.IsNullOrWhiteSpace(usuarioId))
        {
            consulta = consulta.Where(b => b.UsuarioId == usuarioId);
        }

        if (!string.IsNullOrWhiteSpace(entidad))
        {
            consulta = consulta.Where(b => b.Entidad == entidad);
        }

        return await consulta
            .OrderByDescending(b => b.Fecha)
            .ThenByDescending(b => b.Id)
            .Take(MaximoFilas)
            .Select(b => new BitacoraDto
            {
                Id = b.Id,
                UsuarioId = b.UsuarioId,
                Accion = b.Accion,
                Entidad = b.Entidad,
                EntidadId = b.EntidadId,
                Detalle = b.Detalle,
                Fecha = b.Fecha
            })
            .ToListAsync();
    }
}