using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APiProjetoFinal.Data;
using APiProjetoFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebFinal.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DataContext _context;

        public PacientesController([FromServices] DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,CPF,Endereco")] Paciente paciente)
        {

            var cpfCadastrado = await _context.Pacientes.FirstOrDefaultAsync(p => p.CPF == paciente.CPF);
            bool convertetCPF = long.TryParse(paciente.CPF, out long valor);
            if (!convertetCPF)
            {
                ViewBag.CPFcadastrado = "CPF Incorreto";
            
            }
            else if (cpfCadastrado != null && cpfCadastrado.CPF.ToString() == paciente.CPF)
            {
                ViewBag.CPFcadastrado = "CPF já cadastrado";
            }
            else
            {
                await _context.Pacientes.AddAsync(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
         
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,CPF,Endereco")] Paciente paciente)
        {
           
            try
            {
                
                var pacient = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == paciente.Id);


                if (long.TryParse(paciente.CPF, out long valor) == true && pacient != null)
                {
                    pacient.CPF = paciente.CPF;
                    pacient.Nome = paciente.Nome;
                    pacient.Idade = paciente.Idade;
                    pacient.Endereco = paciente.Endereco;
       
                   _context.Pacientes.Update(pacient);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
              throw;
            }

            return View();
         
        }



        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacientes == null)
            {
                return Problem("Entity set 'DataContext.Pacientes'  is null.");
            }
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
