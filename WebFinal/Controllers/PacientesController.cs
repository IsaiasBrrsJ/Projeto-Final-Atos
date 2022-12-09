using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APiProjetoFinal.Data;
using APiProjetoFinal.Models;

namespace WebFinal.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DataContext _context;

        public PacientesController([FromServices] DataContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pacientes.ToListAsync());
        }

       
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

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,CPF,Endereco")] Paciente paciente)
        {

            var cpfCadastrado = await _context.Pacientes.FirstOrDefaultAsync(p => p.CPF == paciente.CPF);

            if (long.TryParse(paciente.CPF, out long valor) == true && cpfCadastrado == null)
            {
                await _context.Pacientes.AddAsync(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            else
             ViewBag.CPFcadastrado = "CPF já cadastrado";
            
            
            
            return View();
         
        }

 
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,CPF,Endereco")] Paciente paciente)
        {
           
            try
            {
                var pacient = await _context.Pacientes.FirstOrDefaultAsync(p => p.CPF == paciente.CPF);

                if (long.TryParse(paciente.CPF, out long valor) == true && pacient == null)
                {

                    _context.Pacientes.Update(paciente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                    ViewBag.CPFcadastrado = "CPF já Cadastrado";

            }
            catch (DbUpdateConcurrencyException)
            {

              throw;
                
           }

            return View();
         
        }
          
        


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
