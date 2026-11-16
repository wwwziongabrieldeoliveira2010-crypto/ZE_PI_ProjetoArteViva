using System.ComponentModel.DataAnnotations;

namespace ArteViva.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Nome { get; set; } = string.Empty;
}
