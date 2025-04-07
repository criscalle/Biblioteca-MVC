using Biblioteca_MVC.Models.Common;

namespace Biblioteca_MVC.Models;

public class MaterialAcademico : BaseDomainModel
{
    public int IdTipoMaterial { get; set; }
    public string? Titulo { get; set; }
    public int CantidaRegistrada { get; set; }
    public int CantidadDisponible { get; set; }
    public TipoMaterial? TipoMaterial { get; set; }
    public ICollection<Historial>? Historiales { get; set; } 

}
