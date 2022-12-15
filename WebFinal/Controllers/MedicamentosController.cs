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
    public class MedicamentosController : Controller
    {
        private readonly DataContext _context;

        public MedicamentosController([FromServices] DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Medicamentos.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicamentos == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,TipoMedicamento,DataDeValidade,Estoque,descricao")] Medicamento medicamento)
        {
            try
            {
                _context.Medicamentos.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

              
            } catch (Exception ex) {
                return View(medicamento);
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Medicamentos == null)
                {
                    return NotFound();
                }

                var medicamento = await _context.Medicamentos.FindAsync(id);
                if (medicamento == null)
                {
                    return NotFound();
                }
                return View(medicamento);
            }
            catch(Exception ex)
            { }

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,TipoMedicamento,DataDeValidade,Estoque,descricao")] Medicamento medicamento)
        {
            if (id != medicamento.Id)
            {
                return NotFound();
            }
            
                try
                {
                    _context.Medicamentos.Update(medicamento);
                    await _context.SaveChangesAsync();

                     return RedirectToAction(nameof(Index));
                 }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
           
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicamentos == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicamentos == null)
            {
                return Problem("Entity set 'DataContext.Medicamentos'  is null.");
            }
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento != null)
            {
                _context.Medicamentos.Remove(medicamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentoExists(int id)
        {
          return _context.Medicamentos.Any(e => e.Id == id);
        }
    }
}
