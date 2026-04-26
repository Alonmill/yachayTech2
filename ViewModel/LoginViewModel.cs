using Azure.Messaging;
using System.ComponentModel.DataAnnotations;

namespace YachayTech_p_cov.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
