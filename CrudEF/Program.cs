using ArteViva.Data;
using Microsoft.EntityFrameworkCore;
/////////////////////////////////////////////////////////////////////
// Criar o banco de dados e as tabelas com dados iniciais
var builder = WebApplication.CreateBuilder(args);

// Configuração do MVC
builder.Services.AddControllersWithViews();

// Configuração do Entity Framework com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")!
    )
);

var app = builder.Build();

// Criar o banco e aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    Console.WriteLine("Banco criado com sucesso!");
}

// Configuração do servidor web
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota inicial
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cadastro}/{action=Index}/{id?}"
);

app.Run();
