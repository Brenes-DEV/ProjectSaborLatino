using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Cuenta;

public class ActualizarPerfilDto
{
    [Required, MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Phone]
    public string? Telefono { get; set; }
}