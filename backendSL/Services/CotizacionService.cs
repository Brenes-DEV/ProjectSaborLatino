using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Data;
using ProjectSaborLatino.DTOs.Solicitudes;
using ProjectSaborLatino.Models;

namespace ProjectSaborLatino.Services;

public interface ICotizacionService
{
    Task<CotizacionDto?> CrearAsync(int solicitudId, CotizacionCrearDto dto, string creadaPorUsuarioId);
    Task<CotizacionDto?> AceptarAsync(int id);
    Task<CotizacionDto?> RechazarAsync(int id, string? soloDelClienteId);
}

public class CotizacionService : ICotizacionService
{
    private readonly ApplicationDbContext _db;

    public CotizacionService(ApplicationDbContext db)
    {
        _db = db;
    }

    // Devuelve null si la solicitud no existe (→ 404)
    public async Task<CotizacionDto?> CrearAsync(int solicitudId, CotizacionCrearDto dto, string creadaPorUsuarioId)
    {
        var solicitud = await _db.Solicitudes.FindAsync(solicitudId);
        if (solicitud is null)
        {
            return null;
        }

        if (solicitud.Estado is EstadoSolicitud.Aceptada or EstadoSolicitud.Rechazada or EstadoSolicitud.Cancelada)
        {
            throw new DatosInvalidosException($"No se puede cotizar una solicitud {solicitud.Estado}.");
        }

        if (dto.Descuento > dto.Monto)
        {
            throw new DatosInvalidosException("El descuento no puede ser mayor que el monto.");
        }

        var vigenteHasta = dto.VigenteHasta!.Value;
        if (vigenteHasta <= DateTime.Now)
        {
            throw new DatosInvalidosException("La fecha de vigencia debe ser en el futuro.");
        }

        var cotizacion = new Cotizacion
        {
            SolicitudId = solicitudId,
            Monto = dto.Monto,
            Descuento = dto.Descuento,
            Detalle = dto.Detalle,
            VigenteHasta = vigenteHasta,
            CreadaPorUsuarioId = creadaPorUsuarioId
        };

        _db.Cotizaciones.Add(cotizacion);
        solicitud.Estado = EstadoSolicitud.Cotizada;
        await _db.SaveChangesAsync();

        return ADto(cotizacion);
    }

    // Aceptar = "contrato": esta queda Aceptada, las otras enviadas se rechazan y la solicitud pasa a Aceptada
    public async Task<CotizacionDto?> AceptarAsync(int id)
    {
        var cotizacion = await _db.Cotizaciones
            .Include(c => c.Solicitud)
                .ThenInclude(s => s!.Cotizaciones)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (cotizacion is null)
        {
            return null;
        }

        if (cotizacion.Estado != EstadoCotizacion.Enviada)
        {
            throw new DatosInvalidosException($"Solo se puede aceptar una cotización Enviada (esta está {cotizacion.Estado}).");
        }

        if (cotizacion.VigenteHasta < DateTime.Now)
        {
            // Se guarda como Vencida antes de avisar del error
            cotizacion.Estado = EstadoCotizacion.Vencida;
            await _db.SaveChangesAsync();
            throw new DatosInvalidosException("La cotización está vencida y no se puede aceptar.");
        }

        var solicitud = cotizacion.Solicitud!;
        if (solicitud.Estado is EstadoSolicitud.Aceptada or EstadoSolicitud.Rechazada or EstadoSolicitud.Cancelada)
        {
            throw new DatosInvalidosException($"La solicitud está {solicitud.Estado} y ya no se puede aceptar.");
        }

        cotizacion.Estado = EstadoCotizacion.Aceptada;

        foreach (var otra in solicitud.Cotizaciones.Where(c => c.Id != id && c.Estado == EstadoCotizacion.Enviada))
        {
            otra.Estado = EstadoCotizacion.Rechazada;
        }

        solicitud.Estado = EstadoSolicitud.Aceptada;
        await _db.SaveChangesAsync();

        return ADto(cotizacion);
    }

    // Si soloDelClienteId trae valor, la solicitud tiene que ser de ese cliente (si no, null → 404)
    public async Task<CotizacionDto?> RechazarAsync(int id, string? soloDelClienteId)
    {
        var cotizacion = await _db.Cotizaciones
            .Include(c => c.Solicitud)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cotizacion is null ||
            (soloDelClienteId is not null && cotizacion.Solicitud!.ClienteUsuarioId != soloDelClienteId))
        {
            return null;
        }

        if (cotizacion.Estado != EstadoCotizacion.Enviada)
        {
            throw new DatosInvalidosException($"Solo se puede rechazar una cotización Enviada (esta está {cotizacion.Estado}).");
        }

        cotizacion.Estado = EstadoCotizacion.Rechazada;
        await _db.SaveChangesAsync();

        return ADto(cotizacion);
    }

    // Público y estático para que SolicitudService también lo use
    public static CotizacionDto ADto(Cotizacion c) => new()
    {
        Id = c.Id,
        SolicitudId = c.SolicitudId,
        Monto = c.Monto,
        Descuento = c.Descuento,
        Total = c.Monto - c.Descuento,
        Detalle = c.Detalle,
        VigenteHasta = c.VigenteHasta,
        Estado = c.Estado.ToString(),
        FechaCreacion = c.FechaCreacion
    };
}