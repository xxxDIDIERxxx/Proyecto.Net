using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class TareasController : Controller
    {
        private readonly ASPNETCoreIdentityCustomContext _context;

        public TareasController(ASPNETCoreIdentityCustomContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var ultimoContext = _context.Tareas.Include(t => t.CategoriaNavigation);
            return View(await ultimoContext.ToListAsync());
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["Categoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarea,NombreTarea,DescripcionTarea,Categoria,FechaInicio,FechaFin")] Tarea tarea)
        {
            if (tarea.NombreTarea.Length > 30)
            {
                ModelState.AddModelError("NombreTarea", "El nombre debe tener minimo 30 caracteres.");
            }
            if (tarea.FechaInicio.Date < DateTime.Today)
            {
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser una fecha pasada.");
            }
            else if (tarea.FechaFin.Date < tarea.FechaInicio.Date)
            {
                ModelState.AddModelError("FechaFin", "La fecha de finalización debe ser posterior a la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Categoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", tarea.Categoria);
            return View(tarea);
        }



        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["Categoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", tarea.Categoria);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,NombreTarea,DescripcionTarea,Categoria,FechaInicio,FechaFin")] Tarea tarea)
        {
            if (id != tarea.IdTarea)
            {
                return NotFound();
            }
            if (tarea.NombreTarea.Length > 30)
            {
                ModelState.AddModelError("NombreTarea", "El nombre debe tener minimo 30 caracteres.");
            }
            if (tarea.FechaInicio.Date < DateTime.Today)
            {
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser una fecha pasada.");
            }
            else if (tarea.FechaFin.Date < tarea.FechaInicio.Date)
            {
                ModelState.AddModelError("FechaFin", "La fecha de finalización debe ser posterior a la fecha de inicio.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.IdTarea))
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
            ViewData["Categoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", tarea.Categoria);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.CategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarea == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tareas == null)
            {
                return Problem("Entity set 'UltimoContext.Tareas'  is null.");
            }
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return (_context.Tareas?.Any(e => e.IdTarea == id)).GetValueOrDefault();
        }
    }
}
