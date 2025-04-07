using Biblioteca_MVC.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public class Persona : BaseDomainModel
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public int IdRol { get; set; }
    public virtual Rol? Rol { get; set; }
    public virtual ICollection<Historial>? Historiales { get; set; }
}
