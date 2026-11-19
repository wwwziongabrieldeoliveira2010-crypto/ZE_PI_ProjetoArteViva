using System.Security.Claims;
using ArteViva.Data;
using ArteViva.Models;
using ArteViva.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArteViva.Controllers;

public class UsuariosController : Controller
{
    private readonly AppDbContext _db;
    private readonly PasswordHasher<Usuario> _hasher = new();

    public UsuariosController(AppDbContext db) => _db = db;

    private async Task CarregarCidades(int? selecionada = null) =>
        ViewBag.Cidades = new SelectList(
            await _db.Cidades.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", selecionada);

    // ---------- CADASTRO ----------

    [HttpGet]
    public async Task<IActionResult> Cadastro()
    {
        await CarregarCidades();
        return View(new CadastroViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastro(CadastroViewModel vm)
    {
        var email = vm.Email.Trim().ToLowerInvariant();

        // RN01: e-mail único
        if (await _db.Usuarios.AnyAsync(u => u.Email == email))
            ModelState.AddModelError(nameof(vm.Email), "Este e-mail já está cadastrado");

        if (!ModelState.IsValid)
        {
            await CarregarCidades(vm.CidadeId);
            return View(vm);
        }

        var usuario = new Usuario
        {
            Nome = (vm.Nome ?? "").Trim(),
            Email = email,
            CidadeId = vm.CidadeId
        };
        usuario.Senha = _hasher.HashPassword(usuario, vm.Senha);   // nunca salvar a senha pura

        try
        {
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();   // INSERT no MySQL
        }
        catch (DbUpdateException)
        {
            // cobre o caso de dois cadastros simultâneos com o mesmo e-mail (índice único)
            ModelState.AddModelError(nameof(vm.Email), "Este e-mail já está cadastrado");
            await CarregarCidades(vm.CidadeId);
            return View(vm);
        }

        await EntrarAsync(usuario, lembrar: false);
        return RedirectToAction("Index", "Home");
    }

    // ---------- LOGIN ----------

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(vm);

        var email = (vm.Email ?? "").Trim().ToLowerInvariant();
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        var ok = usuario != null &&
                 _hasher.VerifyHashedPassword(usuario, usuario.Senha, vm.Senha)
                     != PasswordVerificationResult.Failed;

        if (!ok)
        {
            // mesma mensagem para e-mail inexistente e senha errada
            ModelState.AddModelError("", "E-mail ou senha inválidos");
            return View(vm);
        }

        await EntrarAsync(usuario!, vm.Lembrar);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("~/Views/ViewHome.cshtml", vm);
    }

    // ---------- LOGOUT ----------

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    // ---------- auxiliar ----------

    private async Task EntrarAsync(Usuario u, bool lembrar)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, u.Id.ToString()),
            new(ClaimTypes.Name, u.Nome),
            new(ClaimTypes.Email, u.Email)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = lembrar });
    }
}