using Biblioteca_MVC.Models.Common;

namespace Biblioteca_MVC.Models;

public class Historial : BaseDomainModel
{
    public int IdMaterial { get; set; }
    public int IdPersona { get; set; }
    public Persona? Persona { get; set; }
    public MaterialAcademico? MaterialAcademico { get; set; }
}
