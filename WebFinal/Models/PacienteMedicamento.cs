using APiProjetoFinal.Models;

namespace APiProjetoFinal.Models
{
    public class PacienteMedicamento
    {
       
        public int Id { get; set; }
        public Medicamento Medicamento { get; set; }

        public int MedicamentoId { get; set; }

        public Paciente Paciente { get; set; }

        public int PacienteId { get; set; }

        public DateTime HoraAplicacaoMedicamento { get; set; }
    }
}
