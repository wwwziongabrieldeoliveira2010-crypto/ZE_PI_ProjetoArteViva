using System.ComponentModel.DataAnnotations;

namespace ArteViva.Models;

public class Interesse
    {
    public int Id { get; set; }
    public DateTime RegistradoEm { get; set; } = DateTime.Now;

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public int EventoId { get; set; }
    public Evento? Evento { get; set; }
    }