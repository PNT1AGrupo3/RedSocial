using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedSocial.Models;

namespace RedSocial.Controllers
{
    public class ImagenesController : Controller
    {
        private readonly RedSocialBDContext _context;

        public ImagenesController(RedSocialBDContext context)
        {
            _context = context;
        }

        // GET: Imagenes
        public async Task<IActionResult> Index()
        {
            
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            var redSocialBDContext = _context.Imagen.Include(i => i.Publicacion);
            return View(await redSocialBDContext.ToListAsync());
        }

        // GET: Imagenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen
                .Include(i => i.Publicacion)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (imagen == null)
            {
                return NotFound();
            }

            return View(imagen);
        }

        // GET: Imagenes/Create
        public IActionResult Create()
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            ViewData["PublicacionId"] = new SelectList(_context.Publicacion, "PublicacionId", "Texto");
            return View();
        }

        // POST: Imagenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagenId,FullPath,PublicacionId")] Imagen imagen)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (ModelState.IsValid)
            {
                _context.Add(imagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublicacionId"] = new SelectList(_context.Publicacion, "PublicacionId", "Texto", imagen.PublicacionId);
            return View(imagen);
        }

        // GET: Imagenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen.FindAsync(id);
            if (imagen == null)
            {
                return NotFound();
            }
            ViewData["PublicacionId"] = new SelectList(_context.Publicacion, "PublicacionId", "Texto", imagen.PublicacionId);
            return View(imagen);
        }

        // POST: Imagenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImagenId,FullPath,PublicacionId")] Imagen imagen)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id != imagen.ImagenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagenExists(imagen.ImagenId))
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
            ViewData["PublicacionId"] = new SelectList(_context.Publicacion, "PublicacionId", "Texto", imagen.PublicacionId);
            return View(imagen);
        }

        // GET: Imagenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen
                .Include(i => i.Publicacion)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (imagen == null)
            {
                return NotFound();
            }

            return View(imagen);
        }

        // POST: Imagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!Autenticacion.estaAutenticado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            var imagen = await _context.Imagen.FindAsync(id);
            _context.Imagen.Remove(imagen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagenExists(int id)
        {
            return _context.Imagen.Any(e => e.ImagenId == id);
        }
    }
}
