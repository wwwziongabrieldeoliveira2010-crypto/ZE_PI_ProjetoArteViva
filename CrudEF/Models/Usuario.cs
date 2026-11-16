using System.ComponentModel.DataAnnotations;

namespace ArteViva.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8)]
    public string Senha { get; set; } = string.Empty;

    [StringLength(10)]
    public string Tipo { get; set; } = "comum";

    public DateTime CriadoEm { get; set; } = DateTime.Now;

    public int CidadeId { get; set; }
    public Cidade? Cidade { get; set; }

    public List<Evento> Eventos { get; set; } = new();
}
