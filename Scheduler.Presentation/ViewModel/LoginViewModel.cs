using System.ComponentModel.DataAnnotations;

namespace Scheduler.Presentation.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Obligatorio")]
        // [RegularExpression()]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        // [RegularExpression()]
        public string Password { get; set; }
    }
}
