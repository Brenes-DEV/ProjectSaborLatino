using System.ComponentModel.DataAnnotations;

namespace ProjectSaborLatino.Models;

// Registro de quién hizo qué y cuándo (auditoría)
public class Bitacora
{
    public int Id { get; set; }

    // Usuario que hizo el cambio (null si fue anónimo o el sistema).
    // Sin relación con AspNetUsers a propósito: así borrar un usuario no se bloquea ni borra su historial.
    [MaxLength(450)]
    public string? UsuarioId { get; set; }

    // "Crear", "Modificar", "Eliminar" o acciones de usuarios como "CambiarRol"
    [MaxLength(20)]
    public string Accion { get; set; } = string.Empty;

    // Tipo de registro afectado, por ejemplo "Paquete" o "Usuario"
    [MaxLength(100)]
    public string Entidad { get; set; } = string.Empty;

    // Id del registro afectado (texto, porque puede ser un número, un GUID o una clave doble)
    [MaxLength(100)]
    public string? EntidadId { get; set; }

    [MaxLength(2000)]
    public string? Detalle { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}