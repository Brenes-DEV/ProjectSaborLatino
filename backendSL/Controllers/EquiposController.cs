using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Catalogo;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Equipo físico del negocio
[ApiController]
[Route("api/equipos")]
[Authorize(Roles = Roles.Administrador + "," + Roles.Superadministrador)]
public class EquiposController : ControllerBase
{
    private readonly IEquipoService _service;

    public EquiposController(IEquipoService service)
    {
        _service = service;
    }

    // GET api/equipos → lista pública (solo activos).
    // Un admin puede pedir también los inactivos con ?incluirInactivos=true
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<EquipoDto>>> ObtenerTodos(bool incluirInactivos = false)
    {
        return await _service.ObtenerTodosAsync(SoloActivos(incluirInactivos));
    }

    // GET api/equipos/5
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<EquipoDto>> ObtenerPorId(int id, bool incluirInactivos = false)
    {
        var dto = await _service.ObtenerPorIdAsync(id, SoloActivos(incluirInactivos));
        return dto is null ? NotFound() : dto;
    }

    // POST api/equipos → crear (solo Administrador y Superadministrador)
    [HttpPost]
    public async Task<ActionResult<EquipoDto>> Crear(EquipoGuardarDto dto)
    {
        var creado = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    // PUT api/equipos/5 → editar
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EquipoDto>> Actualizar(int id, EquipoGuardarDto dto)
    {
        var actualizado = await _service.ActualizarAsync(id, dto);
        return actualizado is null ? NotFound() : actualizado;
    }

    // DELETE api/equipos/5 → desactivar (Activo = false), no borra la fila
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