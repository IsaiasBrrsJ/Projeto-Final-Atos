using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APiProjetoFinal.Models
{
    public class Paciente
    {
        public int Id { get; set; }

    
        [Required(ErrorMessage ="Preencha o campo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        
        public int Idade { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
   
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Preencha o campo")]
        public string Endereco { get; set; }

        public ICollection<PacienteMedicamento> Medicamentos { get; set; }
    }
}
