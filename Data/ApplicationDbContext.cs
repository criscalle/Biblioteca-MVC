using Biblioteca_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Historial>()
        .Property(h => h.Id)
        .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<Persona>()
            .Property(p => p.Id)
            .ValueGeneratedNever(); 

        modelBuilder.Entity<MaterialAcademico>()
            .Property(m => m.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Persona>()
            .HasOne(p => p.Rol)
            .WithMany(r => r.Personas)
            .HasForeignKey(p => p.IdRol);

        modelBuilder.Entity<Historial>()
            .HasOne(h => h.MaterialAcademico)
            .WithMany(m => m.Historiales)
            .HasForeignKey(h => h.IdMaterial);

        modelBuilder.Entity<Historial>()
            .HasOne(h => h.Persona)
            .WithMany(p => p.Historiales)
            .HasForeignKey(h => h.IdPersona);

        modelBuilder.Entity<MaterialAcademico>()
            .HasOne(m => m.TipoMaterial)
            .WithMany(t => t.MaterialesAcademicos)
            .HasForeignKey(m => m.IdTipoMaterial);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Persona> Tb_Persona { get; set; }
    public DbSet<Rol> Tb_Rol { get; set; }
    public DbSet<Historial> Tb_Historial { get; set; }
    public DbSet<MaterialAcademico> Tb_MaterialAcademico { get; set; }
    public DbSet<TipoMaterial> Tb_TipoMaterial { get; set; }
}
