namespace ProjectSaborLatino.Models;

// Etapas por las que pasa una solicitud. Se guarda como texto en la base de datos.
public enum EstadoSolicitud
{
    Pendiente,   // recién enviada, sin cotización
    Cotizada,    // el admin ya envió al menos una cotización
    Aceptada,    // se aceptó una cotización (el "contrato")
    Rechazada,   // el negocio no puede atenderla
    Cancelada    // el cliente ya no la quiere
}