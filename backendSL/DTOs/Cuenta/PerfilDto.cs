namespace ProjectSaborLatino.DTOs.Cuenta;

public class PerfilDto
{
    public string Id { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public IList<string> Roles { get; set; } = [];
    public DateTime FechaRegistro { get; set; }
}