using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/mecanicos")]
[ApiController]
public class MecanicosController : ControllerBase
{
    private List<Mecanicos> listaMecanicos;

    public MecanicosController()
    {
        // Inicializar la lista de mecánicos (puedes cambiar esto según tus necesidades)
        listaMecanicos = new List<Mecanicos>();
    }

    // GET: api/mecanicos
    [HttpGet]
    public ActionResult<IEnumerable<Mecanicos>> ObtenerTodosMecanicos()
    {
        return Ok(listaMecanicos);
    }

    // POST: api/mecanicos
    [HttpPost]
    public ActionResult AgregarMecanico([FromBody] Mecanicos mecanico)
    {
        if (mecanico == null)
        {
            return BadRequest("Datos inválidos");
        }

        // Asignar un ID único al nuevo mecánico
        mecanico.IdMecanico = listaMecanicos.Count + 1;

        // Calcular el sueldo total (puedes personalizar esto según tus necesidades)
        mecanico.SueldoTotal = mecanico.SueldoBase + mecanico.GratTitulo;

        // Agregar el mecánico a la lista
        listaMecanicos.Add(mecanico);

        // Devolver respuesta 201 Created con la ubicación del nuevo recurso
        return CreatedAtAction(nameof(ObtenerMecanicoPorId), new { id = mecanico.IdMecanico }, mecanico);
    }

    // PUT: api/mecanicos/5
    [HttpPut("{id}")]
    public ActionResult ActualizarMecanico(int id, [FromBody] Mecanicos mecanicoActualizado)
    {
        if (mecanicoActualizado == null || id != mecanicoActualizado.IdMecanico)
        {
            return BadRequest("Datos inválidos");
        }

        // Buscar el mecánico en la lista por ID
        var mecanico = listaMecanicos.FirstOrDefault(m => m.IdMecanico == id);

        if (mecanico == null)
        {
            return NotFound("Mecánico no encontrado");
        }

        // Actualizar la información del mecánico
        mecanico.Nombre = mecanicoActualizado.Nombre;
        mecanico.Edad = mecanicoActualizado.Edad;
        mecanico.Domicilio = mecanicoActualizado.Domicilio;
        mecanico.Titulo = mecanicoActualizado.Titulo;
        mecanico.Especialidad = mecanicoActualizado.Especialidad;
        mecanico.SueldoBase = mecanicoActualizado.SueldoBase;
        mecanico.GratTitulo = mecanicoActualizado.GratTitulo;

        // Recalcular el sueldo total después de la actualización
        mecanico.SueldoTotal = mecanico.SueldoBase + mecanico.GratTitulo;

        return NoContent(); // Devolver respuesta 204 No Content
    }

    // DELETE: api/mecanicos/5
    [HttpDelete("{id}")]
    public ActionResult EliminarMecanico(int id)
    {
        // Buscar el mecánico en la lista por ID
        var mecanico = listaMecanicos.FirstOrDefault(m => m.IdMecanico == id);

        if (mecanico == null)
        {
            return NotFound("Mecánico no encontrado");
        }

        // Eliminar el mecánico de la lista
        listaMecanicos.Remove(mecanico);

        return NoContent(); // Devolver respuesta 204 No Content
    }

    // Método adicional para obtener un mecánico por ID
    [HttpGet("{id}")]
    public ActionResult<Mecanicos> ObtenerMecanicoPorId(int id)
    {
        var mecanico = listaMecanicos.FirstOrDefault(m => m.IdMecanico == id);

        if (mecanico == null)
        {
            return NotFound("Mecánico no encontrado");
        }

        return Ok(mecanico);
    }
}
