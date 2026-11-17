using ArteViva.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArteViva.ViewModels;

public class EventoListaViewModel
{
    public List<Evento> Eventos { get; set; } = new();
    public SelectList? Cidades { get; set; }
    public SelectList? Categorias { get; set; }
    public int? CidadeId { get; set; }
    public int? CategoriaId { get; set; }
}