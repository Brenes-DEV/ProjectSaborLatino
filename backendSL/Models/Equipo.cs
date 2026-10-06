using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Equipo físico del negocio: parlantes, micrófonos, consola, luces...
public class Equipo
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    // Ej. "Audio", "Iluminación", "Instrumento"
    [MaxLength(50)]
    public string? Tipo { get; set; }

    // Cuántas unidades tiene el negocio en total
    public int CantidadTotal { get; set; }

    public bool Activo { get; set; } = true;
}