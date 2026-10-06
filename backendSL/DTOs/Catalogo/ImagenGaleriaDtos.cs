using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Catalogo;

// Lo que el API devuelve
public class ImagenGaleriaDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string ImagenUrl { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; }
}

// Lo que el API recibe al crear o editar
public class ImagenGaleriaGuardarDto
{
    [Required, MaxLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string ImagenUrl { get; set; } = string.Empty;

    [Range(0, 1000)]
    public int Orden { get; set; }

    public bool Activo { get; set; } = true;
}