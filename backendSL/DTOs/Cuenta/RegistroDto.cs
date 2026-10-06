using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Cuenta;

public class RegistroDto
{
    [Required, MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Phone]
    public string? Telefono { get; set; }
}