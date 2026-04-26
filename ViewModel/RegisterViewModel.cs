using System.ComponentModel.DataAnnotations;

namespace YachayTech_p_cov.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(120)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        [StringLength(180)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmacion de contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
