using ArteViva.Data;
using ArteViva.Models;
using ArteViva.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArteViva.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var agora = DateTime.Now;

        var viewModel = new HomeViewModel
        {
            
            ProximosEventos = await _db.Eventos
                .AsNoTracking()
                .Include(e => e.Cidade)
                .Include(e => e.Categoria)
                .Where(e => e.DataHora >= agora && e.Status == "aprovado")
                .OrderBy(e => e.DataHora)
                .Take(6)
                .ToListAsync(),

            Cidades = new SelectList(
                await _db.Cidades.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
                nameof(Cidade.Id),
                nameof(Cidade.Nome)),

            Categorias = new SelectList(
                await _db.Categorias.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
                nameof(Categoria.Id),
                nameof(Categoria.Nome))
        };

        
        return View("ViewHome", viewModel);
    }
}
