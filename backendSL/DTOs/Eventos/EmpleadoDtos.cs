using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Eventos;

// Lo que el API devuelve
public class EmpleadoDto
{
    public int Id { get; set; }
    public string Cedula { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Puesto { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public bool Activo { get; set; }
    public string? UsuarioId { get; set; }
}

// Lo que el API recibe al crear o editar
public class EmpleadoGuardarDto
{
    [Required, MaxLength(20)]
    public string Cedula { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Puesto { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Telefono { get; set; }

    public bool Activo { get; set; } = true;

    // Opcional: Id de una cuenta con rol Trabajador
    public string? UsuarioId { get; set; }
}