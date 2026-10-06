using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Catalogo;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Preguntas frecuentes de la página pública
[ApiController]
[Route("api/preguntas-frecuentes")]
[Authorize(Roles = Roles.Administrador + "," + Roles.Superadministrador)]
public class PreguntasFrecuentesController : ControllerBase
{
    private readonly IPreguntaFrecuenteService _service;

    public PreguntasFrecuentesController(IPreguntaFrecuenteService service)
    {
        _service = service;
    }

    // GET api/preguntas-frecuentes → lista pública (solo activos).
    // Un admin puede pedir también los inactivos con ?incluirInactivos=true
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PreguntaFrecuenteDto>>> ObtenerTodos(bool incluirInactivos = false)
    {
        return await _service.ObtenerTodosAsync(SoloActivos(incluirInactivos));
    }

    // GET api/preguntas-frecuentes/5
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<PreguntaFrecuenteDto>> ObtenerPorId(int id, bool incluirInactivos = false)
    {
        var dto = await _service.ObtenerPorIdAsync(id, SoloActivos(incluirInactivos));
        return dto is null ? NotFound() : dto;
    }

    // POST api/preguntas-frecuentes → crear (solo Administrador y Superadministrador)
    [HttpPost]
    public async Task<ActionResult<PreguntaFrecuenteDto>> Crear(PreguntaFrecuenteGuardarDto dto)
    {
        var creado = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    // PUT api/preguntas-frecuentes/5 → editar
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PreguntaFrecuenteDto>> Actualizar(int id, PreguntaFrecuenteGuardarDto dto)
    {
        var actualizado = await _service.ActualizarAsync(id, dto);
        return actualizado is null ? NotFound() : actualizado;
    }

    // DELETE api/preguntas-frecuentes/5 → desactivar (Activo = false), no borra la fila
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        return await _service.DesactivarAsync(id) ? NoContent() : NotFound();
    }

    // Solo un admin puede ver lo inactivo; para el público siempre es "solo activos"
    private bool SoloActivos(bool incluirInactivos)
    {
        var esAdmin = User.IsInRole(Roles.Administrador) || User.IsInRole(Roles.Superadministrador);
        return !(incluirInactivos && esAdmin);
    }
}