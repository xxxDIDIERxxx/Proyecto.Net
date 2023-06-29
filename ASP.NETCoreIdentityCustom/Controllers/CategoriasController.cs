using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Models;
namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ASPNETCoreIdentityCustomContext _context;

        public CategoriasController(ASPNETCoreIdentityCustomContext context)
        {
            _context = context;
        }
        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return _context.Categorias != null ?
                        View(await _context.Categorias.ToListAsync()) :
                        Problem("Entity set 'ASPNETCoreIdentityCustomContext.Categorias'  is null.");
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
        public async Task<IActionResult> Create([Bind("IdCategoria, NombreCategoria, ColorCategoria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria, NombreCategoria, ColorCategoria")] Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return NotFound();
            }

            var tieneTareasAsociadas = await _context.Tareas.AnyAsync(t => t.IdTarea == id);

            if (tieneTareasAsociadas)
            {
                ModelState.AddModelError(string.Empty, "No se puede editar el nombre de la categoría porque hay tareas asociadas a ella.");
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(categoria.IdCategoria))
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
            return View(categoria);
        }


        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            // Verificar si existen tareas relacionadas con la categoría
            var tareasRelacionadas = await _context.Tareas.Where(t => t.CategoriaNavigation == categoria).ToListAsync();

            if (tareasRelacionadas.Any())
            {
                TempData["Mensaje"] = "No se puede eliminar la categoría porque hay tareas relacionadas.";
            }
            else
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "La categoría ha sido eliminada correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return (_context.Categorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
