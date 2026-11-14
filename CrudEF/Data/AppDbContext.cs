using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEF.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(
        @"server=localhost;
        database=meubanco;
        user=root;
        password=18062010;");
    }
}

