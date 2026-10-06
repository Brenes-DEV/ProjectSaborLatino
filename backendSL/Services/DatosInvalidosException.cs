namespace ProjectSaborLatino.Services;

// Error en los datos que el cliente puede corregir; el controller lo convierte en 400
public class DatosInvalidosException : Exception
{
    public DatosInvalidosException(string mensaje) : base(mensaje)
    {
    }
}