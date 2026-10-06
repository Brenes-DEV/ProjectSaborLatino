using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Catalogo;

// Lo que el API devuelve
public class EquipoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Tipo { get; set; }
    public int CantidadTotal { get; set; }
    public bool Activo { get; set; }
}

// Lo que el API recibe al crear o editar
public class EquipoGuardarDto
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Tipo { get; set; }

    [Range(0, 10000, ErrorMessage = "La cantidad no puede ser negativa.")]
    public int CantidadTotal { get; set; }

    public bool Activo { get; set; } = true;
}