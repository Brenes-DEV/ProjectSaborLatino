using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Registro de cada cambio de estado: de cuál a cuál, quién y cuándo
public class EventoHistorialEstado
{
    public int Id { get; set; }

    public int EventoId { get; set; }
    public Evento? Evento { get; set; }

    // null en el primer registro (cuando se crea el evento)
    public EstadoEvento? EstadoAnterior { get; set; }

    public EstadoEvento EstadoNuevo { get; set; }

    // Usuario que hizo el cambio
    public string UsuarioId { get; set; } = string.Empty;
    public ApplicationUser? Usuario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Comentario { get; set; }
}