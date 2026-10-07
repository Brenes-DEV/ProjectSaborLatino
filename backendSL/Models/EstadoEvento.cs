namespace ProjectSaborLatino.Models;

// Etapas de un evento, en este orden. Se guarda como texto en la base de datos.
public enum EstadoEvento
{
    Programado,  // creado a partir de una cotización aceptada
    EnMontaje,   // el equipo está llegando e instalándose
    EnServicio,  // el evento está ocurriendo
    Finalizado,  // terminó
    Cancelado    // se canceló (solo el admin, con motivo)
}