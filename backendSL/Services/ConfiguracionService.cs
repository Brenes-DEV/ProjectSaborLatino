using Microsoft.EntityFrameworkCore;
using ProjectSaborLatino.Data;
using ProjectSaborLatino.DTOs.Sistema;

namespace ProjectSaborLatino.Services;

public interface IConfiguracionService
{
    Task<List<ConfiguracionDto>> ObtenerTodasAsync();
    Task<ConfiguracionDto?> ActualizarAsync(string clave, string valor);
    Task<int> ObtenerEnteroAsync(string clave, int valorPorDefecto);
}

public class ConfiguracionService : IConfiguracionService
{
    public const string EventosMinimos = "Fidelidad.EventosMinimos";
    public const string PorcentajeDescuento = "Fidelidad.PorcentajeDescuento";

    private readonly ApplicationDbContext _db;

    public ConfiguracionService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<ConfiguracionDto>> ObtenerTodasAsync()
    {
        return await _db.Configuraciones
            .OrderBy(c => c.Clave)
            .Select(c => new ConfiguracionDto { Clave = c.Clave, Valor = c.Valor, Descripcion = c.Descripcion })
            .ToListAsync();
    }

    // Solo se pueden cambiar parámetros que ya existen (null → 404)
    public async Task<ConfiguracionDto?> ActualizarAsync(string clave, string valor)
    {
        var config = await _db.Configuraciones.FindAsync(clave);
        if (config is null)
        {
            return null;
        }

        valor = valor.Trim();

        // Los parámetros de fidelidad tienen que ser números enteros válidos
        if (clave.StartsWith("Fidelidad."))
        {
            if (!int.TryParse(valor, out var numero) || numero < 0)
            {
                throw new DatosInvalidosException($"{clave} debe ser un número entero mayor o igual a 0.");
            }

            if (clave == PorcentajeDescuento && numero > 100)
            {
                throw new DatosInvalidosException("El porcentaje de descuento no puede ser mayor que 100.");
            }

            valor = numero.ToString();
        }

        config.Valor = valor;
        await _db.SaveChangesAsync();

        return new ConfiguracionDto { Clave = config.Clave, Valor = config.Valor, Descripcion = config.Descripcion };
    }

    // Lee un parámetro numérico; si no existe o no es número, usa el valor por defecto
    public async Task<int> ObtenerEnteroAsync(string clave, int valorPorDefecto)
    {
        var valor = await _db.Configuraciones
            .Where(c => c.Clave == clave)
            .Select(c => c.Valor)
            .FirstOrDefaultAsync();

        return int.TryParse(valor, out var numero) ? numero : valorPorDefecto;
    }
}