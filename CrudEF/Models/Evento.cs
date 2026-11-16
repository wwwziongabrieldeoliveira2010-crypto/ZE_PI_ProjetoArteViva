using System.ComponentModel.DataAnnotations;

namespace ArteViva.Models;

public class Evento
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(2000)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public DateTime DataHora { get; set; }

    [Required, StringLength(200)]
    public string Local { get; set; } = string.Empty;

    public bool Gratuito { get; set; } = true;

    [StringLength(15)]
    public string Status { get; set; } = "aprovado";

    public DateTime CriadoEm { get; set; } = DateTime.Now;

    public int CidadeId { get; set; }
    public Cidade? Cidade { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}