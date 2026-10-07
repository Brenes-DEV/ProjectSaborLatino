using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Lo que pide un visitante o un cliente: "quiero tal paquete para tal evento"
public class Solicitud
{
    public int Id { get; set; }

    // Cliente registrado que la envió (null si la mandó un visitante sin cuenta)
    public string? ClienteUsuarioId { get; set; }
    public ApplicationUser? ClienteUsuario { get; set; }

    // Datos de contacto: se guardan siempre, aunque sea un visitante
    [MaxLength(150)]
    public string NombreContacto { get; set; } = string.Empty;

    [MaxLength(256)]
    public string CorreoContacto { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? TelefonoContacto { get; set; }

    // Paquete que le interesa (opcional)
    public int? PaqueteId { get; set; }
    public Paquete? Paquete { get; set; }

    // Ej. "Boda", "Quince años", "Cumpleaños"
    [MaxLength(100)]
    public string TipoEvento { get; set; } = string.Empty;

    public DateTime FechaEvento { get; set; }

    [MaxLength(300)]
    public string Lugar { get; set; } = string.Empty;

    public int? CantidadInvitados { get; set; }

    [MaxLength(2000)]
    public string? Comentarios { get; set; }

    public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // El admin puede responder con una o varias cotizaciones
    public List<Cotizacion> Cotizaciones { get; set; } = [];
}
