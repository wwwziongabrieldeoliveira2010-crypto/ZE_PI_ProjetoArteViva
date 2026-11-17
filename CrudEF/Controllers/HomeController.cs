// using System.Diagnostics;
// using ArteViva.Data;
// using ArteViva.ViewModels;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;

// namespace ArteViva.Controllers;

// public class HomeController : Controller
// {
//     private readonly AppDbContext _db;
//     public HomeController(AppDbContext db) => _db = db;

//     // GET /  (página inicial)
//     public async Task<IActionResult> Index()
//     {
//         var vm = new HomeViewModel
//         {
//             ProximosEventos = await _db.Eventos
//                 .Include(e => e.Cidade)
//                 .Include(e => e.Categoria)
//                 .Where(e => e.DataHora >= DateTime.Now)
//                 .OrderBy(e => e.DataHora)
//                 .Take(6)                       // só os 6 mais próximos
//                 .ToListAsync(),

//             Cidades = new SelectList(await _db.Cidades.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome"),
//             Categorias = new SelectList(await _db.Categorias.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome")
//         };

//         return View(vm);
//     }

//     // GET /Home/Sobre
//     public IActionResult Sobre() => View();

//     // GET /Home/Error
//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error() =>
//         View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
// }