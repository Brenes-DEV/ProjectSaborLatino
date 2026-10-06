using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.DTOs.Catalogo;

// Lo que el API devuelve
public class PreguntaFrecuenteDto
{
    public int Id { get; set; }
    public string Pregunta { get; set; } = string.Empty;
    public string Respuesta { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; }
}

// Lo que el API recibe al crear o editar
public class PreguntaFrecuenteGuardarDto
{
    [Required, MaxLength(300)]
    public string Pregunta { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Respuesta { get; set; } = string.Empty;

    [Range(0, 1000)]
    public int Orden { get; set; }

    public bool Activo { get; set; } = true;
}