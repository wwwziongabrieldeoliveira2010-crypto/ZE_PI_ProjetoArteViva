using System.ComponentModel.DataAnnotations;

namespace ArteViva.Models;

public class Cidade
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, StringLength(2)]
    public string Uf { get; set; } = string.Empty;
}
