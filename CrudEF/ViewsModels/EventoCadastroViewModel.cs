using System.ComponentModel.DataAnnotations;

namespace ArteViva.ViewModels;

public class EventoCadastroViewModel
{
    [Required(ErrorMessage = "Informe o título do evento")]
    [StringLength(150, ErrorMessage = "Máximo de 150 caracteres")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = "";

    [Required(ErrorMessage = "Informe a descrição")]
    [StringLength(2000, ErrorMessage = "Máximo de 2000 caracteres")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = "";

    [Required(ErrorMessage = "Informe a data e a hora")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Data e hora")]
    public DateTime? DataHora { get; set; }

    [Required(ErrorMessage = "Informe o local")]
    [StringLength(200, ErrorMessage = "Máximo de 200 caracteres")]
    [Display(Name = "Local")]
    public string Local { get; set; } = "";

    [Range(1, int.MaxValue, ErrorMessage = "Escolha a cidade")]
    [Display(Name = "Cidade")]
    public int CidadeId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Escolha a categoria")]
    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }

    [Display(Name = "Evento gratuito")]
    public bool Gratuito { get; set; } = true;
}