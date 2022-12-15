using System.ComponentModel.DataAnnotations;

namespace WebFinal.Models
{
	public class Usuario
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o Login")]
        public string User { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        public string Password { get; set; }

        
	}
}
