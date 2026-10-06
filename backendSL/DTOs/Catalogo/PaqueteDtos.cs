using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Catalogo;

// Lo que el API devuelve
public class PaqueteDto
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public string ServicioNombre { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PrecioBase { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Activo { get; set; }
    public List<PaqueteEquipoDto> Equipos { get; set; } = [];
}

// Un equipo dentro del paquete, ya con su nombre para mostrarlo
public class PaqueteEquipoDto
{
    public int EquipoId { get; set; }
    public string EquipoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
}

// Lo que el API recibe al crear o editar
public class PaqueteGuardarDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un servicio válido.")]
    public int ServicioId { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Descripcion { get; set; }

    [Range(0, 99999999.99, ErrorMessage = "El precio no puede ser negativo.")]
    public decimal PrecioBase { get; set; }

    [MaxLength(500)]
    public string? ImagenUrl { get; set; }

    public bool Activo { get; set; } = true;

    // Equipo que incluye (puede venir vacío)
    public List<PaqueteEquipoGuardarDto> Equipos { get; set; } = [];
}

public class PaqueteEquipoGuardarDto
{
    [Range(1, int.MaxValue)]
    public int EquipoId { get; set; }

    [Range(1, 1000, ErrorMessage = "La cantidad debe ser al menos 1.")]
    public int Cantidad { get; set; } = 1;
}