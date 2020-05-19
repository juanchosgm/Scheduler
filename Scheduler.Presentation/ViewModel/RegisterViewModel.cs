using System.ComponentModel.DataAnnotations;

namespace Scheduler.Presentation.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [Compare(nameof(Password), ErrorMessage = "Información Incorrecta")]
        public string ConfirmPassword { get; set; }
    }
}
