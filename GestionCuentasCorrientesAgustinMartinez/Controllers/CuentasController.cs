using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCuentasCorrientesAgustinMartinez.Data;
using GestionCuentasCorrientesAgustinMartinez.Models;

namespace GestionCuentasCorrientesAgustinMartinez.Controllers
{
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cuentas
        public async Task<IActionResult> Index()
        {
              return _context.Cuentas != null ? 
                          View(await _context.Cuentas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cuentas'  is null.");
        }

        // GET: Cuentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cuentas == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // GET: Cuentas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cuenta cuenta, int IdCliente)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == IdCliente);
            if (cliente == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid && cliente.Estado != 0)
            {
                _context.Cuentas.Add(cuenta);
                await _context.SaveChangesAsync();
                if (cuenta.Descripcion == "Debito")
                {
                    cliente.Saldo -= cuenta.Importe;
                    _context.Clientes.Update(cliente);
                }
                if (cuenta.Descripcion == "Credito")
                {
                    cliente.Saldo += cuenta.Importe;
                    _context.Clientes.Update(cliente);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cuentas == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Importe,Descripcion,IdCliente")] Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaExists(cuenta.Id))
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
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cuentas == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cuentas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cuentas'  is null.");
            }
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta != null)
            {
                _context.Cuentas.Remove(cuenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaExists(int id)
        {
          return (_context.Cuentas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
