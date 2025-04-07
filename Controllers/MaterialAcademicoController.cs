using Biblioteca_MVC.Controllers.Features;
using Biblioteca_MVC.Data;
using Biblioteca_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialAcademicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaterialAcademicoController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialAcademico>>> Get()
        {
            return await _context.Tb_MaterialAcademico.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialAcademico>> GetMaterial(int id)
        {
            var material = await _context.Tb_MaterialAcademico.FindAsync(id);
            if (material == null)
                return NotFound();

            return material;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MaterialAcademico>> Create([FromBody] MaterialAcademicoDTO material)
        {
            if (material == null)
            {
                return BadRequest("Los datos del material son requeridos.");
            }
            if(material.CantidadDisponible > material.CantidaRegistrada)
            {
                return BadRequest("La cantidad disponible no puede ser mayor a la cantidad registrada");
            }

            var existeMaterial = await _context.Tb_MaterialAcademico.AnyAsync(p => p.Id == material.Id);
            if (existeMaterial)
            {
                return Conflict("Ya existe una persona con esta cedula.");
            }

            var materialEntity = new MaterialAcademico
            {
                Id = material.Id,
                IdTipoMaterial = material.IdTipoMaterial,
                Titulo = material.Titulo,
                CantidaRegistrada = material.CantidaRegistrada,
                CantidadDisponible = material.CantidadDisponible,
                CreatedDate = DateTime.UtcNow
            };

            _context.Tb_MaterialAcademico.Add(materialEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaterial), new { Id = materialEntity.Id }, materialEntity);

        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Persona>> Update(int id, [FromBody] MaterialAcademicoDTO material)
        {
            if (material == null)
            {
                return BadRequest("Los datos del material son requeridos.");
            }

            var materialEntity = await _context.Tb_MaterialAcademico.FirstOrDefaultAsync(p => p.Id == id);
            if (materialEntity == null)
            {
                return NotFound("No se encontró una persona con este documento.");
            }

            materialEntity.IdTipoMaterial = material.IdTipoMaterial;
            materialEntity.Titulo = material.Titulo;
            materialEntity.CantidadDisponible = material.CantidadDisponible;
            materialEntity.CantidaRegistrada = material.CantidaRegistrada;
            materialEntity.LastModifiedDate = DateTime.UtcNow;

            _context.Tb_MaterialAcademico.Update(materialEntity);
            await _context.SaveChangesAsync();

            return Ok(materialEntity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var materialEntity = await _context.Tb_MaterialAcademico.FirstOrDefaultAsync(p => p.Id == id);
            if (materialEntity == null)
            {
                return NotFound("No se encontró un material académico con este ID.");
            }

            _context.Tb_MaterialAcademico.Remove(materialEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
