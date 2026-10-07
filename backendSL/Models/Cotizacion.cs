using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjectSaborLatino.Models;

// Respuesta del negocio a una solicitud: precio, descuento y hasta cuándo vale
public class Cotizacion
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public Solicitud? Solicitud { get; set; }

    [Precision(10, 2)]
    public decimal Monto { get; set; }

    [Precision(10, 2)]
    public decimal Descuento { get; set; } = 0;

    [MaxLength(2000)]
    public string? Detalle { get; set; }

    // Después de esta fecha ya no se puede aceptar
    public DateTime VigenteHasta { get; set; }

    public EstadoCotizacion Estado { get; set; } = EstadoCotizacion.Enviada;

    // Admin que la creó
    public string CreadaPorUsuarioId { get; set; } = string.Empty;
    public ApplicationUser? CreadaPorUsuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}