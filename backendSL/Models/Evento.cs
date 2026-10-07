using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// El evento contratado: nace de una cotización aceptada
public class Evento
{
    public int Id { get; set; }

    // Cotización aceptada de la que sale (una sola vez por cotización)
    public int CotizacionId { get; set; }
    public Cotizacion? Cotizacion { get; set; }

    // Se copia de la solicitud al crear el evento
    public int? PaqueteId { get; set; }
    public Paquete? Paquete { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    [MaxLength(300)]
    public string Lugar { get; set; } = string.Empty;

    public EstadoEvento Estado { get; set; } = EstadoEvento.Programado;

    [MaxLength(500)]
    public string? MotivoCancelacion { get; set; }

    [MaxLength(2000)]
    public string? Notas { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Equipo reservado para el evento
    public List<EventoEquipo> Equipos { get; set; } = [];

    // Empleados asignados
    public List<EventoTrabajador> Trabajadores { get; set; } = [];

    // Cada cambio de estado queda registrado
    public List<EventoHistorialEstado> Historial { get; set; } = [];
}