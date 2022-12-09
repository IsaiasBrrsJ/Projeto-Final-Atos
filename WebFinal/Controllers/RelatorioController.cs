﻿using APiProjetoFinal.Data;
using APiProjetoFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebFinal.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly DataContext _context;

        public RelatorioController([FromServices] DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var medicamentoPaciente = await _context.PacienteMedicamentos
                .Include(x => x.Medicamento)
                .Include(x => x.Paciente)
                .AsNoTracking().ToListAsync<PacienteMedicamento>();

            var medicamento = await _context.Medicamentos.AsNoTracking().ToListAsync<Medicamento>();

            ViewBag.PacienteMedicamento = medicamentoPaciente;
            ViewBag.Medicamento = medicamento;


            return View();
        }
    }
}