using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjectSaborLatino.Models;

// Hereda Email, PhoneNumber, contraseña, etc. de Identity; aquí solo lo que falta
public class ApplicationUser : IdentityUser
{
    [MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}