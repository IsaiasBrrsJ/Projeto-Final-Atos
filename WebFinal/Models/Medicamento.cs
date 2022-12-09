using System.Text.Json.Serialization;

namespace APiProjetoFinal.Models
{
    public class Medicamento
    {
        public int Id { get; set; } 

        public string Nome { get; set; }

        public string TipoMedicamento { get; set; } 

        public DateTime DataDeValidade { get; set; }

        public int Estoque { get; set; }

        public string descricao { get; set; }

        [JsonIgnore]
        public ICollection<PacienteMedicamento> Pacientes { get; set; } 
    }
}
