using System.Security.Claims;
using ArteViva.Data;
using ArteViva.Models;
using ArteViva.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArteViva.Controllers;

public class EventosController : Controller
{
    private readonly AppDbContext _db;

    public EventosController(AppDbContext db) => _db = db;

    private async Task CarregarListas(int? cidadeId = null, int? categoriaId = null)
    {
        ViewBag.Cidades = new SelectList(
            await _db.Cidades.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
            "Id", "Nome", cidadeId);

        ViewBag.Categorias = new SelectList(
            await _db.Categorias.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
            "Id", "Nome", categoriaId);
    }

    // ---------- LISTAGEM (usada pelos links da Home e do menu) ----------

    [HttpGet]
    public async Task<IActionResult> Index(int? cidadeId, int? categoriaId)
    {
        var query = _db.Eventos
            .AsNoTracking()
            .Include(e => e.Cidade)
            .Include(e => e.Categoria)
            .Where(e => e.Status == "aprovado" && e.DataHora >= DateTime.Now);

        if (cidadeId.HasValue) query = query.Where(e => e.CidadeId == cidadeId);
        if (categoriaId.HasValue) query = query.Where(e => e.CategoriaId == categoriaId);

        var vm = new EventoListaViewModel
        {
            Eventos = await query.OrderBy(e => e.DataHora).ToListAsync(),
            CidadeId = cidadeId,
            CategoriaId = categoriaId,
            Cidades = new SelectList(
                await _db.Cidades.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
                "Id", "Nome", cidadeId),
            Categorias = new SelectList(
                await _db.Categorias.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(),
                "Id", "Nome", categoriaId)
        };

        return View(vm);
    }

    // ---------- CADASTRAR EVENTO ----------

    [Authorize, HttpGet]
    public async Task<IActionResult> Criar()
    {
        await CarregarListas();
        return View(new EventoCadastroViewModel());
    }

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(EventoCadastroViewModel vm)
    {
        // o evento precisa acontecer no futuro
        if (vm.DataHora.HasValue && vm.DataHora.Value <= DateTime.Now)
            ModelState.AddModelError(nameof(vm.DataHora), "A data do evento deve ser futura");

        // cidade e categoria precisam existir
        if (!await _db.Cidades.AnyAsync(c => c.Id == vm.CidadeId))
            ModelState.AddModelError(nameof(vm.CidadeId), "Cidade inválida");

        if (!await _db.Categorias.AnyAsync(c => c.Id == vm.CategoriaId))
            ModelState.AddModelError(nameof(vm.CategoriaId), "Categoria inválida");

        if (!ModelState.IsValid)
        {
            await CarregarListas(vm.CidadeId, vm.CategoriaId);
            return View(vm);
        }

        var evento = new Evento
        {
            Titulo = vm.Titulo.Trim(),
            Descricao = vm.Descricao.Trim(),
            DataHora = vm.DataHora!.Value,
            Local = vm.Local.Trim(),
            Gratuito = vm.Gratuito,
            CidadeId = vm.CidadeId,
            CategoriaId = vm.CategoriaId,
            UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            // Status = "aprovado" e CriadoEm já têm valor padrão no model
        };

        _db.Eventos.Add(evento);
        await _db.SaveChangesAsync();   // INSERT no MySQL

        TempData["Sucesso"] = "Evento cadastrado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
    

    // ---------- MEUS EVENTOS ----------

    [Authorize, HttpGet]
    public async Task<IActionResult> MeusEventos()
    {
        var eventos = await _db.Eventos
            .AsNoTracking()
            .Include(e => e.Cidade)
            .Include(e => e.Categoria)
            .Where(e => e.UsuarioId == UsuarioLogadoId())
            .OrderBy(e => e.DataHora)
            .ToListAsync();

        return View("~/Views/Eventos/MeusEventos.cshtml", eventos);
    }

    // ---------- EXCLUIR EVENTO ----------

    [Authorize, HttpGet]
    public async Task<IActionResult> Excluir(int id)
    {
        // só encontra o evento se ele for do usuário logado (RN07)
        var evento = await _db.Eventos
            .AsNoTracking()
            .Include(e => e.Cidade)
            .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId());

        if (evento == null) return NotFound();

        return View("~/Views/Eventos/Excluir.cshtml", evento);
    }

    [Authorize, HttpPost, ActionName("Excluir"), ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(int id)
    {
        var evento = await _db.Eventos
            .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId());

        if (evento == null) return NotFound();

        _db.Eventos.Remove(evento);
        await _db.SaveChangesAsync();   // os interesses do evento saem junto (RN14)

        TempData["Sucesso"] = "Evento excluído com sucesso.";
        return RedirectToAction(nameof(MeusEventos));
    }

    private int UsuarioLogadoId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);}