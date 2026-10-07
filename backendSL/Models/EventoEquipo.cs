using Microsoft.EntityFrameworkCore;

namespace ProjectSaborLatino.Models;

// Equipo reservado para un evento y cuántas unidades
[PrimaryKey(nameof(EventoId), nameof(EquipoId))]
public class EventoEquipo
{
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }

    public int EquipoId { get; set; }
    public Equipo? Equipo { get; set; }

    public int Cantidad { get; set; }
}