using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Parámetros del sistema que el Superadministrador puede cambiar sin tocar el código
public class Configuracion
{
    // La clave es el nombre del parámetro, por ejemplo "Fidelidad.EventosMinimos"
    [Key]
    [MaxLength(100)]
    public string Clave { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Valor { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Descripcion { get; set; }
}