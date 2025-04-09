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
