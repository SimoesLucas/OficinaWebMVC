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
    public class ServicosController : Controller
    {
        private readonly OficinaDBContexto _context;

        public ServicosController(OficinaDBContexto context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            List<Servico> servicos = await _context.Servicos.ToListAsync();
            List<ServicoModel> servicoM = new List<ServicoModel>();
            foreach (Servico s in servicos)
            {
                ServicoModel servicoModel = new()
                {
                    Id = s.Id,
                    Descricao = s.Descricao,
                    TipoServico = s.TipoServico,
                    Preco = s.Preco,
                    Observacao = s.Observacao
                };
                servicoM.Add(servicoModel);

            }
            
              return _context.Servicos != null ? 
                          View(servicoM) :
                          Problem("Entity set 'OficinaDBContexto.Servicos'  is null.");
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            
            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.Id == id);


            if (servico == null)
            {
                return NotFound();
            }
            ServicoModel servicoModel = new ServicoModel();

            servicoModel.Id = servico.Id;
            servicoModel.Observacao = servico.Observacao;
            servicoModel.Preco = servico.Preco;
            servicoModel.TipoServico = servico.TipoServico;
            servicoModel.Descricao = servico.Descricao;

            return View(servicoModel);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Observacao,Preco,TipoServico,Id")] ServicoModel servicoModel)
        {

            if (ModelState.IsValid)
            {
                Servico servico = new Servico();

                servico.Id = Guid.NewGuid();
                servico.TipoServico = servicoModel.TipoServico;
                servico.Observacao = servicoModel.Observacao;
                servico.Preco = servicoModel.Preco;
                servico.Descricao= servicoModel.Descricao;
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicoModel);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            ServicoModel servicoModel = new ServicoModel
            {
                Id = servico.Id,
                Descricao = servico.Descricao,
                Observacao = servico.Observacao,
                Preco = servico.Preco,
                TipoServico = servico.TipoServico
                
            };
            return View(servicoModel);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Descricao,Observacao,Preco,TipoServico,Id")] ServicoModel servicoModel)
        {
            
            if (id != servicoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {

                    Servico servico = new Servico
                    {
                        Observacao = servicoModel.Observacao,
                        Descricao = servicoModel.Descricao,
                        Id = servicoModel.Id,
                        Preco = servicoModel.Preco,
                        TipoServico = servicoModel.TipoServico  
                    };
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servicoModel.Id))
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
            return View(servicoModel);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.Id == id);


            if (servico == null)
            {
                return NotFound();
            }

            ServicoModel servicoModel = new ServicoModel
            {
                Id = servico.Id,
                Descricao = servico.Descricao,
                Observacao = servico.Observacao,
                Preco= servico.Preco,
                TipoServico = servico.TipoServico

            };

            return View(servicoModel);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Servicos == null)
            {
                return Problem("Entity set 'OficinaDBContexto.Servicos'  is null.");
            }
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(Guid id)
        {
          return (_context.Servicos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
