using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Catalogo;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Paquetes que el cliente puede contratar, con su equipo incluido
[ApiController]
[Route("api/paquetes")]
[Authorize(Roles = Roles.Administrador + "," + Roles.Superadministrador)]
public class PaquetesController : ControllerBase
{
    private readonly IPaqueteService _service;

    public PaquetesController(IPaqueteService service)
    {
        _service = service;
    }

    // GET api/paquetes → lista pública (solo activos).
    // Filtro opcional por servicio: api/paquetes?servicioId=1
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PaqueteDto>>> ObtenerTodos(int? servicioId = null, bool incluirInactivos = false)
    {
        return await _service.ObtenerTodosAsync(SoloActivos(incluirInactivos), servicioId);
    }

    // GET api/paquetes/5
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<PaqueteDto>> ObtenerPorId(int id, bool incluirInactivos = false)
    {
        var dto = await _service.ObtenerPorIdAsync(id, SoloActivos(incluirInactivos));
        return dto is null ? NotFound() : dto;
    }

    // POST api/paquetes → crear (solo Administrador y Superadministrador)
    [HttpPost]
    public async Task<ActionResult<PaqueteDto>> Crear(PaqueteGuardarDto dto)
    {
        try
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (DatosInvalidosException ex)
        {
            return DatosInvalidos(ex);
        }
    }

    // PUT api/paquetes/5 → editar (también reemplaza la lista de equipo)
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PaqueteDto>> Actualizar(int id, PaqueteGuardarDto dto)
    {
        try
        {
            var actualizado = await _service.ActualizarAsync(id, dto);
            return actualizado is null ? NotFound() : actualizado;
        }
        catch (DatosInvalidosException ex)
        {
            return DatosInvalidos(ex);
        }
    }

    // DELETE api/paquetes/5 → desactivar (Activo = false), no borra la fila
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        return await _service.DesactivarAsync(id) ? NoContent() : NotFound();
    }

    private bool SoloActivos(bool incluirInactivos)
    {
        var esAdmin = User.IsInRole(Roles.Administrador) || User.IsInRole(Roles.Superadministrador);
        return !(incluirInactivos && esAdmin);
    }

    // Convierte el error del Service en un 400 con el mensaje
    private ActionResult DatosInvalidos(DatosInvalidosException ex)
    {
        ModelState.AddModelError("Paquete", ex.Message);
        return ValidationProblem(ModelState);
    }
}