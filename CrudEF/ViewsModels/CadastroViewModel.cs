using System.ComponentModel.DataAnnotations;

namespace ArteViva.ViewModels;

public class CadastroViewModel
{
    [Required(ErrorMessage = "Informe o nome")]
    [StringLength(100)]
    public string Nome { get; set; } = "";

    [Required, EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = "";

    [Required, MinLength(6, ErrorMessage = "Mínimo de 6 caracteres")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = "";

    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    [DataType(DataType.Password)]
    public string ConfirmarSenha { get; set; } = "";

    [Required(ErrorMessage = "Escolha a cidade")]
    public int CidadeId { get; set; }
}