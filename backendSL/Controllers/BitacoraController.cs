using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Sistema;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Consulta de la bitácora (solo Superadministrador)
[ApiController]
[Route("api/bitacora")]
[Authorize(Roles = Roles.Superadministrador)]
public class BitacoraController : ControllerBase
{
    private readonly IBitacoraService _service;

    public BitacoraController(IBitacoraService service)
    {
        _service = service;
    }

    // GET api/bitacora?desde=&hasta=&usuarioId=&entidad=Paquete → más recientes primero, máximo 500
    [HttpGet]
    public async Task<ActionResult<List<BitacoraDto>>> Obtener(
        DateTime? desde = null, DateTime? hasta = null, string? usuarioId = null, string? entidad = null)
    {
        return await _service.ObtenerAsync(desde, hasta, usuarioId, entidad);
    }
}