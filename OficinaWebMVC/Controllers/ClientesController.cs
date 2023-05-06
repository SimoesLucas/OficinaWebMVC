using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using OficinaWebMVC.Database.Contexto;
using OficinaWebMVC.Database.Entities;
using OficinaWebMVC.Models;

namespace OficinaWebMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly OficinaDBContexto _context;

        public ClientesController(OficinaDBContexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            List<Cliente> clientes = await _context.Clientes.ToListAsync();
            List<ClienteModel> clientesModels = new List<ClienteModel>();



            foreach (Cliente  c in clientes)
            {
                ClienteModel clienteM = new ClienteModel();
               clienteM.Id = c.Id;
                clienteM.Nome= c.Nome;
                clienteM.Telefone= c.Telefone;
                clienteM.Email= c.Email;
                clienteM.Endereco = c.Endereco;
                clientesModels.Add(clienteM);
            }
          

              return _context.Clientes != null ? 
                          View(clientesModels) :
                          Problem("Entity set 'OficinaDBContexto.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (cliente == null)
            {
                return NotFound();
            }
            ClienteModel clienteModel = new ClienteModel();
            clienteModel.Id = cliente.Id;
            clienteModel.Nome = cliente.Nome;
            clienteModel.Telefone = cliente.Telefone;
            clienteModel.Email = cliente.Email;
            clienteModel.Endereco = cliente.Endereco;

            return View(clienteModel);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Endereco,Telefone")] ClienteModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();

                cliente.Nome = clienteModel.Nome;
                cliente.Telefone = clienteModel.Telefone;
                cliente.Email = clienteModel.Email;
                cliente.Endereco = clienteModel.Endereco;
                   
                cliente.Id = Guid.NewGuid();
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteModel);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ClienteModel clienteModel = new ClienteModel();
            clienteModel.Id = cliente.Id;
            clienteModel.Nome = cliente.Nome;
            clienteModel.Telefone = cliente.Telefone;
            clienteModel.Email = cliente.Email;
            clienteModel.Endereco = cliente.Endereco;
            return View(clienteModel);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Email,Endereco,Telefone,Id")] ClienteModel clienteModel)
        {

            if (id != clienteModel.Id)
            {
                return NotFound();
            }

                      

            if (ModelState.IsValid)
            {
                try
                {

                    Cliente cliente = new Cliente();
                    cliente.Id = clienteModel.Id;
                    cliente.Nome = clienteModel.Nome;
                    cliente.Telefone = clienteModel.Telefone;
                    cliente.Email = clienteModel.Email;
                    cliente.Endereco = clienteModel.Endereco;
                    
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(clienteModel.Id))
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
            return View(clienteModel);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }


            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ClienteModel clienteModel = new ClienteModel();
            clienteModel.Id = cliente.Id;
            clienteModel.Nome = cliente.Nome;
            clienteModel.Telefone = cliente.Telefone;
            clienteModel.Email = cliente.Email;
            clienteModel.Endereco = cliente.Endereco;

            return View(clienteModel);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'OficinaDBContexto.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
