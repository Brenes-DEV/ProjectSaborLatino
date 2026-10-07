namespace ProjectSaborLatino.Models;

// Etapas de una cotización. Se guarda como texto en la base de datos.
public enum EstadoCotizacion
{
    Enviada,     // esperando respuesta
    Aceptada,    // se aceptó; solo puede haber una por solicitud
    Rechazada,   // la rechazó el cliente o el admin, o se aceptó otra
    Vencida      // pasó la fecha VigenteHasta sin aceptarse
}