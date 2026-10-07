using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Solicitudes;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Aceptar o rechazar cotizaciones (se crean desde api/solicitudes/{id}/cotizaciones)
[ApiController]
[Route("api/cotizaciones")]
public class CotizacionesController : ControllerBase
{
    private const string RolesAdmin = Roles.Administrador + "," + Roles.Superadministrador;

    private readonly ICotizacionService _cotizaciones;

    public CotizacionesController(ICotizacionService cotizaciones)
    {
        _cotizaciones = cotizaciones;
    }

    // PATCH api/cotizaciones/5/aceptar → el "contrato" (solo admin)
    [HttpPatch("{id:int}/aceptar")]
    [Authorize(Roles = RolesAdmin)]
    public async Task<ActionResult<CotizacionDto>> Aceptar(int id)
    {
        try
        {
            var aceptada = await _cotizaciones.AceptarAsync(id);
            return aceptada is null ? NotFound() : aceptada;
        }
        catch (DatosInvalidosException ex)
        {
            return DatosInvalidos(ex);
        }
    }

    // PATCH api/cotizaciones/5/rechazar → el admin, o el cliente dueño de la solicitud
    [HttpPatch("{id:int}/rechazar")]
    [Authorize(Roles = RolesAdmin + "," + Roles.Cliente)]
    public async Task<ActionResult<CotizacionDto>> Rechazar(int id)
    {
        try
        {
            var rechazada = await _cotizaciones.RechazarAsync(id, EsAdmin() ? null : UsuarioId());
            return rechazada is null ? NotFound() : rechazada;
        }
        catch (DatosInvalidosException ex)
        {
            return DatosInvalidos(ex);
        }
    }

    private string? UsuarioId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

    private bool EsAdmin() =>
        User.IsInRole(Roles.Administrador) || User.IsInRole(Roles.Superadministrador);

    private ActionResult DatosInvalidos(DatosInvalidosException ex)
    {
        ModelState.AddModelError("Cotizacion", ex.Message);
        return ValidationProblem(ModelState);
    }
}