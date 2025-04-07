using Biblioteca_MVC.BLL;
using Biblioteca_MVC.Controllers.Features;
using Biblioteca_MVC.Data;
using Biblioteca_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly HistorialService _historialService;

    public HistorialController(ApplicationDbContext context, HistorialService historialService)
    {
        _context = context;
        _historialService = historialService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Historial>>> GetHistorial()
    {
        return await _context.Tb_Historial.ToListAsync();
    }

    [HttpGet("{idPersona}")]
    public async Task<ActionResult<Historial>> GetHistorialById(int idPersona)
    {
        var historial = await _context.Tb_Historial
        .Where(h => h.IdPersona == idPersona)
        .ToListAsync();

        if (historial == null || historial.Count == 0)
            return NotFound("No se encontraron registros para esta persona.");

        return Ok(historial);
    }


    /*[HttpPost("CreateHistorial")]
    public async Task<ActionResult<Historial>> CreateHistorial([FromBody] HistorialDTO historial)
    {
        if (historial == null)
        {
            return BadRequest("Los datos del prestamo son requeridos.");
        }

        var existePersona = await _context.Tb_Persona.AnyAsync(p => p.Id == historial.IdPersona);
        if (!existePersona)
        {
            return Conflict("La persona no se encuentra registrada");
        }

        var historialEntity = new Historial
        {
            Id = historial.Id,
            IdMaterial = historial.IdMaterial,
            IdPersona = historial.IdPersona,
            CreatedDate = DateTime.UtcNow
        };

        _context.Tb_Historial.Add(historialEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHistorial), new { Id = historialEntity.Id }, historialEntity);
       
    }

    [HttpPut("UpdateHistorial/{id}")]
    public async Task<ActionResult<Historial>> UpdateHistorial(int id, [FromBody] HistorialDTO historial)
    {
        if (historial == null)
        {
            return BadRequest("Los datos del prestamo son requeridos.");
        }

        var historialEntity = await _context.Tb_Historial.FirstOrDefaultAsync(p => p.Id == id);
        if (historialEntity == null)
        {
            return NotFound("No se encontró un historial con .");
        }

        historialEntity.Id = historial.Id;
        historialEntity.IdMaterial = historial.IdMaterial;
        historialEntity.IdPersona = historial.IdPersona;
        historialEntity.LastModifiedDate = DateTime.UtcNow;

        _context.Tb_Historial.Update(historialEntity);
        await _context.SaveChangesAsync();

        return Ok(historialEntity);
    }
    */
    [HttpPost("prestar")]
    public async Task<IActionResult> PrestarMaterial(int personaId, int materialAcademicoId)
    {
        var resultado = await _historialService.PrestarMaterial(personaId, materialAcademicoId);
        return Ok(resultado);
    }

    [HttpPost("devolver")]
    public async Task<IActionResult> DevolverMaterial(int personaId, int materialAcademicoId)
    {
        var resultado = await _historialService.DevolverMaterial(personaId, materialAcademicoId);
        return Ok(resultado);
    }

}
