using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjectSaborLatino.Models;

// Empleado asignado a un evento y qué hace ahí
[PrimaryKey(nameof(EventoId), nameof(EmpleadoId))]
public class EventoTrabajador
{
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }

    public int EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }

    // Ej. "Técnico de sonido", "DJ", "Montaje"
    [MaxLength(100)]
    public string? Funcion { get; set; }
}