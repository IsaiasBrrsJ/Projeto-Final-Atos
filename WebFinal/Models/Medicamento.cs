using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebFinal.Models;

namespace APiProjetoFinal.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        public string TipoMedicamento { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        public DateTime DataDeValidade { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        public int Estoque { get; set; }
        [Required(ErrorMessage = "Preencha o campo")]
        public string descricao { get; set; }

       
        public ICollection<PacienteMedicamento> Pacientes { get; set; } 
    }
}
