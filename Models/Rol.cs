﻿namespace Biblioteca_MVC.Models;

public class Rol
{
    public int Id { get; set; }
    public string? Descripcion { get; set; }
    public int LimitePrestamo { get; set; }
    public ICollection<Persona>? Personas { get; set; }
}
