using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSaborLatino.DTOs.Eventos;
using ProjectSaborLatino.Models;
using ProjectSaborLatino.Services;

namespace ProjectSaborLatino.Controllers;

// Empleados del negocio (solo Administrador y Superadministrador)
[ApiController]
[Route("api/empleados")]
[Authorize(Roles = Roles.Administrador + "," + Roles.Superadministrador)]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoService _service;

    public EmpleadosController(IEmpleadoService service)
    {
        _service = service;
    }

    // GET api/empleados → activos; con ?incluirInactivos=true también los inactivos
    [HttpGet]
    public async Task<ActionResult<List<EmpleadoDto>>> ObtenerTodos(bool incluirInactivos = false)
    {
        return await _service.ObtenerTodosAsync(soloActivos: !incluirInactivos);
    }

    // GET api/empleados/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpleadoDto>> ObtenerPorId(int id)
    {
        var dto = await _service.ObtenerPorIdAsync(id);
        return dto is null ? NotFound() : dto;
    }

    // POST api/empleados
    [HttpPost]
    public async Task<ActionResult<EmpleadoDto>> Crear(EmpleadoGuardarDto dto)
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

    // PUT api/empleados/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmpleadoDto>> Actualizar(int id, EmpleadoGuardarDto dto)
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

    // DELETE api/empleados/5 → desactivar (Activo = false), no borra la fila
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        return await _service.DesactivarAsync(id) ? NoContent() : NotFound();
    }

    private ActionResult DatosInvalidos(DatosInvalidosException ex)
    {
        ModelState.AddModelError("Empleado", ex.Message);
        return ValidationProblem(ModelState);
    }
}