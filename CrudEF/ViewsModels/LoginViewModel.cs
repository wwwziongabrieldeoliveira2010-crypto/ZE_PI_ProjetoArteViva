using System.ComponentModel.DataAnnotations;

namespace ArteViva.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o e-mail"), EmailAddress]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Informe a senha"), DataType(DataType.Password)]
    public string Senha { get; set; } = "";

    public bool Lembrar { get; set; }
}