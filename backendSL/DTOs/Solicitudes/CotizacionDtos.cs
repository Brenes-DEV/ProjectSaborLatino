using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Solicitudes;

// Lo que el API devuelve
public class CotizacionDto
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public decimal Monto { get; set; }
    public decimal Descuento { get; set; }

    // Lo que paga el cliente: Monto - Descuento
    public decimal Total { get; set; }

    public string? Detalle { get; set; }
    public DateTime VigenteHasta { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}

// Lo que manda el admin al crear una cotización
public class CotizacionCrearDto
{
    [Range(0.01, 99999999.99, ErrorMessage = "El monto debe ser mayor que 0.")]
    public decimal Monto { get; set; }

    [Range(0, 99999999.99, ErrorMessage = "El descuento no puede ser negativo.")]
    public decimal Descuento { get; set; }

    [MaxLength(2000)]
    public string? Detalle { get; set; }

    [Required]
    public DateTime? VigenteHasta { get; set; }
}