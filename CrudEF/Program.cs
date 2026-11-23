using ArteViva.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
/////////////////////////////////////////////////////////////////////
// Criar o banco de dados e as tabelas com dados iniciais
var builder = WebApplication.CreateBuilder(args);

// Configuração do MVC
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Usuarios/Login";   // redireciona quem não está logado
    });

// Configuração do Entity Framework com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")!
    )
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o => o.LoginPath = "/Conta/Login");

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

app.UseAuthentication();
app.UseAuthorization();

// Rota inicial
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Cadastro}/{id?}"
);

app.Run();
