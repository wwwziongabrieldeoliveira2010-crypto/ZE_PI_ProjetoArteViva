using ArteViva.Models;
using Microsoft.EntityFrameworkCore;

namespace ArteViva.Data;

public class AppDbContext : DbContext
{

public AppDbContext() { }
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseMySQL("Server=localhost;Database=arteviva;User=root;Password=Senac2026;");
   
}





    public DbSet<Cidade> Cidades => Set<Cidade>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Interesse> Interesses => Set<Interesse>(); // opcional    public DbSet<Interesse> Interesses => Set<Interesse>(); 
    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Índices e unicidade
        mb.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();       // RN01
        mb.Entity<Categoria>().HasIndex(c => c.Nome).IsUnique();
        mb.Entity<Evento>().HasIndex(e => e.DataHora);                // ordenação por data
        mb.Entity<Interesse>().HasIndex(i => new { i.UsuarioId, i.EventoId }).IsUnique(); // RN15

        // Apagar cidade ou categoria não pode apagar usuários e eventos
        mb.Entity<Usuario>().HasOne(u => u.Cidade).WithMany()
          .HasForeignKey(u => u.CidadeId).OnDelete(DeleteBehavior.Restrict);
        mb.Entity<Evento>().HasOne(e => e.Cidade).WithMany()
          .HasForeignKey(e => e.CidadeId).OnDelete(DeleteBehavior.Restrict);
        mb.Entity<Evento>().HasOne(e => e.Categoria).WithMany()
          .HasForeignKey(e => e.CategoriaId).OnDelete(DeleteBehavior.Restrict);

        // Apagar usuário apaga seus eventos; apagar evento apaga os interesses (RN14)
        mb.Entity<Evento>().HasOne(e => e.Usuario).WithMany(u => u.Eventos).HasForeignKey(e => e.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Interesse>().HasOne(i => i.Evento).WithMany().HasForeignKey(i => i.EventoId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Interesse>().HasOne(i => i.Usuario).WithMany().HasForeignKey(i => i.UsuarioId).OnDelete(DeleteBehavior.Cascade);

        // Dados iniciais
        mb.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Música" },
            new Categoria { Id = 2, Nome = "Teatro" },
            new Categoria { Id = 3, Nome = "Exposições de arte" },
            new Categoria { Id = 4, Nome = "Feiras culturais" },
            new Categoria { Id = 5, Nome = "Dança" },
            new Categoria { Id = 6, Nome = "Cinema" },
            new Categoria { Id = 7, Nome = "Oficinas" },
            new Categoria { Id = 8, Nome = "Outros" });

        mb.Entity<Cidade>().HasData(
            new Cidade { Id = 1, Nome = "São José", Uf = "SC" },
            new Cidade { Id = 2, Nome = "Florianópolis", Uf = "SC" },
            new Cidade { Id = 3, Nome = "Palhoça", Uf = "SC" },
            new Cidade { Id = 4, Nome = "Biguaçu", Uf = "SC" });
    }
}