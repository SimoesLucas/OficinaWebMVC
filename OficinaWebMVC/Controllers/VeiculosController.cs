using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficinaWebMVC.Database.Contexto;
using OficinaWebMVC.Database.Entities;
using OficinaWebMVC.Models;

namespace OficinaWebMVC.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly OficinaDBContexto _context;

        public VeiculosController(OficinaDBContexto context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            return _context.Veiculo != null ?
                        View(await _context.Veiculo.ToListAsync()) :
                        Problem("Entity set 'OficinaDBContexto.Veiculo'  is null.");
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult CreateMoto()
        {
            return View();
        }
        public async Task<IActionResult> CreateCarro()
        {
            var clienteBanco = await _context.Clientes.ToListAsync();
            var listaCliente = new List<SelectListItem>();

            foreach (var cliente in clienteBanco)
            {
                listaCliente.Add(new SelectListItem
                {
                    Text = cliente.Nome,
                    Value = cliente.Id.ToString(),

                });

            }

            ViewBag.Clientes = listaCliente;

            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarro([Bind("Placa,Ano,CodChassi,IdCliente")] CarroModel veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Carro carro = new Carro();

                    carro.Id = Guid.NewGuid();
                    carro.ModeloCarro = veiculo.ModeloCarro;
                    carro.Placa = veiculo.Placa;
                    carro.Ano = veiculo.Ano;
                    carro.CodChassi = veiculo.CodChassi;
                    carro.ClienteId = veiculo.IdCliente;

                    _context.Add(carro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(veiculo);
            }
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMoto([Bind("Placa,Ano,CodChassi,Id")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.Id = Guid.NewGuid();
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Placa,Ano,CodChassi,Id")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Veiculo == null)
            {
                return Problem("Entity set 'OficinaDBContexto.Veiculo'  is null.");
            }
            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculo.Remove(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(Guid id)
        {
            return (_context.Veiculo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
