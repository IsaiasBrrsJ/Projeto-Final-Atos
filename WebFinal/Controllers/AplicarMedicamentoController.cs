using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APiProjetoFinal.Data;
using APiProjetoFinal.Models;
using WebFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebFinal.Controllers
{
    public class AplicarMedicamentoController : Controller
    {
        private readonly DataContext _context;

        public AplicarMedicamentoController([FromServices] DataContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PacienteMedicamentos
                .Include(p => p.Medicamento)
                .Include(p => p.Paciente);

            return View(await dataContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PacienteMedicamentos == null)
            {
                return NotFound();
            }

            var pacienteMedicamento = await _context.PacienteMedicamentos
                .Include(p => p.Medicamento)
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacienteMedicamento == null)
            {
                return NotFound();
            }

            return View(pacienteMedicamento);
        }


        [Authorize]
        public IActionResult Create()
        {
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos.Where(p => p.Estoque > 0), "Id", "Nome");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nome");
            ViewData["CPF"] = new SelectList(_context.Pacientes, "Id", "CPF");
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, MedicamentoId,PacienteId,HoraAplicacaoMedicamento")] PacienteMedicamento pacienteMedicamento)
        {
            
                try
                {

                     var medicamentoEstoque = await _context.Medicamentos.FirstOrDefaultAsync(m => m.Id == pacienteMedicamento.MedicamentoId);
                     
                     medicamentoEstoque.Estoque--;
                     _context.PacienteMedicamentos.Add(pacienteMedicamento);
                     _context.Medicamentos.Update(medicamentoEstoque);    
                     await _context.SaveChangesAsync();
                    
                }
                catch(Exception ex)
                {
                    if (!PacienteMedicamentoExists(pacienteMedicamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            

        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PacienteMedicamentos == null)
            {
                return NotFound();
            }

            var pacienteMedicamento = await _context.PacienteMedicamentos.FindAsync(id);
            if (pacienteMedicamento == null)
            {
                return NotFound();
            }
            ViewData["MedicamentoNome"] = new SelectList(_context.Medicamentos, "Id", "Nome", pacienteMedicamento.MedicamentoId);
            ViewData["PacienteNome"] = new SelectList(_context.Pacientes, "Id", "Nome", pacienteMedicamento.PacienteId);
            ViewData["PacienteCPF"] = new SelectList(_context.Pacientes, "Id", "CPF", pacienteMedicamento.PacienteId);
            return View(pacienteMedicamento);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicamentoId,PacienteId,HoraAplicacaoMedicamento")] PacienteMedicamento pacienteMedicamento)
        {
            if (id != pacienteMedicamento.Id)
            {
                return NotFound();
            }

          
                try
                {
                    _context.PacienteMedicamentos.Update(pacienteMedicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteMedicamentoExists(pacienteMedicamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            

        }
      
        private bool PacienteMedicamentoExists(int id)
        {
          return _context.PacienteMedicamentos.Any(e => e.Id == id);
        }
    }
}
