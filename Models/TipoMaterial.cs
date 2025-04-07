namespace Biblioteca_MVC.Models;

public class TipoMaterial
{
    public int Id { get; set; }
    public string? Descripcion { get; set; }
    public ICollection<MaterialAcademico>? MaterialesAcademicos { get; set; }
}
