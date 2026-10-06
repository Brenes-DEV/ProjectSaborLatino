using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Data;
using ProjectSaborLatino.DTOs.Catalogo;
using ProjectSaborLatino.Models;

namespace ProjectSaborLatino.Services;

public interface IEquipoService
{
    Task<List<EquipoDto>> ObtenerTodosAsync(bool soloActivos);
    Task<EquipoDto?> ObtenerPorIdAsync(int id, bool soloActivos);
    Task<EquipoDto> CrearAsync(EquipoGuardarDto dto);
    Task<EquipoDto?> ActualizarAsync(int id, EquipoGuardarDto dto);
    Task<bool> DesactivarAsync(int id);
}

public class EquipoService : IEquipoService
{
    private readonly ApplicationDbContext _db;

    public EquipoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<EquipoDto>> ObtenerTodosAsync(bool soloActivos)
    {
        var lista = await _db.Equipos
            .Where(x => !soloActivos || x.Activo)
            .OrderBy(x => x.Nombre)
            .ToListAsync();

        return lista.Select(ADto).ToList();
    }

    public async Task<EquipoDto?> ObtenerPorIdAsync(int id, bool soloActivos)
    {
        var entidad = await _db.Equipos
            .FirstOrDefaultAsync(x => x.Id == id && (!soloActivos || x.Activo));

        return entidad is null ? null : ADto(entidad);
    }

    public async Task<EquipoDto> CrearAsync(EquipoGuardarDto dto)
    {
        var entidad = new Equipo();
        CopiarDatos(dto, entidad);

        _db.Equipos.Add(entidad);
        await _db.SaveChangesAsync();

        return ADto(entidad);
    }

    public async Task<EquipoDto?> ActualizarAsync(int id, EquipoGuardarDto dto)
    {
        var entidad = await _db.Equipos.FindAsync(id);
        if (entidad is null)
        {
            return null;
        }

        CopiarDatos(dto, entidad);
        await _db.SaveChangesAsync();

        return ADto(entidad);
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var entidad = await _db.Equipos.FindAsync(id);
        if (entidad is null)
        {
            return false;
        }

        entidad.Activo = false;
        await _db.SaveChangesAsync();
        return true;
    }

    private static void CopiarDatos(EquipoGuardarDto dto, Equipo entidad)
    {
        entidad.Nombre = dto.Nombre;
        entidad.Tipo = dto.Tipo;
        entidad.CantidadTotal = dto.CantidadTotal;
        entidad.Activo = dto.Activo;
    }

    private static EquipoDto ADto(Equipo x) => new()
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Tipo = x.Tipo,
        CantidadTotal = x.CantidadTotal,
        Activo = x.Activo
    };
}