using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APiProjetoFinal.Models
{
    public class Paciente
    {
        public int Id { get; set; }

    
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Preencha o campo")]
        public string Nome { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Preencha o campo")]
        
        public int Idade { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Preencha o campo")]
        [MaxLength(11)]
        public string CPF { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Preencha o campo")]
        
        public string Endereco { get; set; }

        public ICollection<PacienteMedicamento> Medicamentos { get; set; }
    }
}
