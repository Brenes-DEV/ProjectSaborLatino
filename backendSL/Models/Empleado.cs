using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Persona que trabaja en los eventos (técnico de sonido, músico, montador...)
public class Empleado
{
    public int Id { get; set; }

    // Única: no puede haber dos empleados con la misma cédula
    [MaxLength(20)]
    public string Cedula { get; set; } = string.Empty;

    [MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Puesto { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Telefono { get; set; }

    public bool Activo { get; set; } = true;

    // Cuenta con rol Trabajador para que vea sus eventos (opcional)
    public string? UsuarioId { get; set; }
    public ApplicationUser? Usuario { get; set; }
}