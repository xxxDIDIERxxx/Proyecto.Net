using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ASPNETCoreIdentityCustomContext _context;

        public ProyectosController(ASPNETCoreIdentityCustomContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            var proyectos = await _context.Proyectos.Include(p => p.CedulaClienteNavigation).ToListAsync();
            return View(proyectos);
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.CedulaClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "Cedula", "Cedula");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CedulaCliente,Fecha,TipoRiesgo")] Proyecto proyecto)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "Cedula", "Cedula", proyecto.CedulaCliente);
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "Cedula", "Cedula", proyecto.CedulaCliente);
            return View(proyecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CedulaCliente,Fecha,TipoRiesgo")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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

            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "Cedula", "Cedula", proyecto.CedulaCliente);
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.CedulaClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
