using Biblioteca_MVC.Data;
using Biblioteca_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.BLL;

public class HistorialService 
{
    private readonly ApplicationDbContext _context;

    public HistorialService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> PrestarMaterial(int personaId, int materialId)
    {
        var persona = await _context.Tb_Persona.Include(p => p.Rol)
                                               .FirstOrDefaultAsync(p => p.Id == personaId);
        if (persona == null) return "Persona no encontrada.";

        var material = await _context.Tb_MaterialAcademico.FindAsync(materialId);
        if (material == null) return "Material no encontrado.";

        int prestamosActuales = await _context.Tb_Historial
            .Where(h => h.IdPersona == personaId && h.LastModifiedDate == null)
            .CountAsync();

        if (prestamosActuales >= persona.Rol.LimitePrestamo)
            return "No puedes solicitar más materiales.";

        if (material.CantidadDisponible <= 0)
            return "No hay ejemplares disponibles.";


        var historial = new Historial
        {
            IdMaterial = materialId,
            IdPersona = personaId
        };
        _context.Tb_Historial.Add(historial);


        material.CantidadDisponible--;

        await _context.SaveChangesAsync();
        return "Préstamo registrado correctamente.";
    }

    public async Task<string> DevolverMaterial(int personaId, int materialId)
    {
        var historial = await _context.Tb_Historial
            .Where(h => h.IdPersona == personaId && h.IdMaterial == materialId && h.LastModifiedDate == null)
            .FirstOrDefaultAsync();

        if (historial == null) return "No se encontró un préstamo activo.";


        historial.LastModifiedDate = DateTime.UtcNow;

        var material = await _context.Tb_MaterialAcademico.FindAsync(materialId);
        if (material != null)
            material.CantidadDisponible++;

        await _context.SaveChangesAsync();
        return "Material devuelto correctamente.";
    }
}
