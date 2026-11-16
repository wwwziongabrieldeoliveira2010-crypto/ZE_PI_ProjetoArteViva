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

public class Categoria
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Nome { get; set; } = string.Empty;
}

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
public class Interesse
    {
    public int Id { get; set; }
    public DateTime RegistradoEm { get; set; } = DateTime.Now;

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public int EventoId { get; set; }
    public Evento? Evento { get; set; }
    }
