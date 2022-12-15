using APiProjetoFinal.Models;
using Microsoft.EntityFrameworkCore;
using WebFinal.Models;

namespace APiProjetoFinal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> context) : base(context) { }


        public DbSet<Medicamento> Medicamentos { get; set; }    

        public DbSet<Paciente> Pacientes { get; set;}

        public DbSet<PacienteMedicamento> PacienteMedicamentos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PacienteMedicamento>()
                .HasKey(k => new {k.Id});

            modelBuilder.Entity<PacienteMedicamento>()
                .HasOne(p => p.Paciente)
                .WithMany(m => m.Medicamentos)
                .HasForeignKey(fk => fk.PacienteId);

            modelBuilder.Entity<PacienteMedicamento>()
                .HasOne(m => m.Medicamento)
                .WithMany(p => p.Pacientes)
                .HasForeignKey(fk => fk.MedicamentoId);
        }
    }
}
