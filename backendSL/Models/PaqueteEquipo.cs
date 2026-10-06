using Microsoft.EntityFrameworkCore;

namespace ProjectSaborLatino.Models;

// Tabla intermedia: qué equipo y cuántas unidades lleva cada paquete.
// La clave es la pareja (PaqueteId, EquipoId): un equipo no se repite en el mismo paquete.
[PrimaryKey(nameof(PaqueteId), nameof(EquipoId))]
public class PaqueteEquipo
{
    public int PaqueteId { get; set; }
    public Paquete? Paquete { get; set; }

    public int EquipoId { get; set; }
    public Equipo? Equipo { get; set; }

    public int Cantidad { get; set; }
}