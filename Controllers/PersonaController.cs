using Biblioteca_MVC.Controllers.Features;
using Biblioteca_MVC.Data;
using Biblioteca_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PersonaController(ApplicationDbContext context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
    {
        return await _context.Tb_Persona.ToListAsync();
    }

    /// <param name="id">ID de la persona</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Persona>> GetPersona(int id)
    {
        var persona = await _context.Tb_Persona.FindAsync(id);
        if (persona == null)
            return NotFound();

        return persona;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Persona>> Create([FromBody] PersonaDTO persona)
    {
        if (persona == null)
        {
            return BadRequest("Los datos de la persona son requeridos.");
        }

        var existePersona = await _context.Tb_Persona.AnyAsync(p => p.Id == persona.Id);
        if (existePersona)
        {
            return Conflict("Ya existe una persona con esta cedula.");
        }

        var personaEntity = new Persona
        {
            Id = persona.Id,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            IdRol = persona.IdRol,
            CreatedDate = DateTime.UtcNow

        };

        _context.Tb_Persona.Add(personaEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPersona), new { Id = personaEntity.Id }, personaEntity);

    }

    [HttpPut("Update/{id}")]
    public async Task<ActionResult<Persona>> Update(int id, [FromBody] PersonaDTO persona)
    {
        if (persona == null)
        {
            return BadRequest("Los datos de la persona son requeridos.");
        }

        var personaEntity = await _context.Tb_Persona.FirstOrDefaultAsync(p => p.Id == id);
        if (personaEntity == null)
        {
            return NotFound("No se encontró una persona con este documento.");
        }

        personaEntity.Nombre = persona.Nombre;
        personaEntity.Apellido = persona.Apellido;
        personaEntity.IdRol = persona.IdRol;
        personaEntity.LastModifiedDate = DateTime.UtcNow;

        _context.Tb_Persona.Update(personaEntity);
        await _context.SaveChangesAsync();

        return Ok(personaEntity);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var personaEntity = await _context.Tb_Persona.FirstOrDefaultAsync(p => p.Id == id);
        if (personaEntity == null)
        {
            return NotFound("No se encontró una persona con este documento.");
        }

        bool tienePrestamosPendientes = await _context.Tb_Historial
            .AnyAsync(h => h.IdPersona == id && h.LastModifiedDate == null);

        if (tienePrestamosPendientes)
        {
            return BadRequest("No se puede eliminar la persona porque tiene préstamos pendientes.");
        }

        _context.Tb_Persona.Remove(personaEntity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}



