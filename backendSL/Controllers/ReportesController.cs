using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Sistema;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Reportes y fidelidad (Administrador y Superadministrador)
[ApiController]
[Route("api/reportes")]
[Authorize(Roles = Roles.Administrador + "," + Roles.Superadministrador)]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _service;

    public ReportesController(IReporteService service)
    {
        _service = service;
    }

    // GET api/reportes/resumen?anio=2027&mes=5 → por defecto el mes actual
    [HttpGet("resumen")]
    public async Task<ActionResult<ResumenDto>> Resumen(int? anio = null, int? mes = null)
    {
        try
        {
            return await _service.ObtenerResumenAsync(anio, mes);
        }
        catch (DatosInvalidosException ex)
        {
            ModelState.AddModelError("Reporte", ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    // GET api/solicitudes/5/fidelidad (la "/" inicial ignora el prefijo api/reportes)
    [HttpGet("/api/solicitudes/{id:int}/fidelidad")]
    public async Task<ActionResult<FidelidadDto>> Fidelidad(int id)
    {
        var dto = await _service.ObtenerFidelidadAsync(id);
        return dto is null ? NotFound() : dto;
    }
}