using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedSocial.Models;

namespace RedSocial.Controllers
{
    public class PublicacionesController : Controller
    {
        private readonly RedSocialBDContext _context;
        private IWebHostEnvironment _environment;

        public PublicacionesController(RedSocialBDContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Publicaciones
        public async Task<IActionResult> Index()
        {
            var redSocialBDContext = _context.Publicacion.Include(p => p.UserEmailNavigation);
            return View(await redSocialBDContext.ToListAsync());
        }

        // GET: Publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicacion
                .Include(p => p.UserEmailNavigation)
                .FirstOrDefaultAsync(m => m.PublicacionId == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // GET: Publicaciones/Create
        public IActionResult Create()
        {
            ViewData["UserEmail"] = new SelectList(_context.Usuario, "Email", "Email");
            return View();
        }

        // POST: Publicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicacionId,Fecha,Texto,UserEmail")] Publicacion publicacion, List<IFormFile> imagenes)
        {
            
            if (!this.sonImagenesValidas(imagenes))
            {
                ModelState.AddModelError("imagenes", "Agregue una o mas imagenes en formato .JPG");
            }
            if (ModelState.IsValid)
            {
                publicacion.Fecha = DateTime.Now;
                foreach (IFormFile imagen in imagenes)
                {
                    Imagen tmpImagen = new Imagen();
                    //AREGLAR EL FILE PATH CON EL ID PUBLICACION Y EL INDEX DE LA IMAGEN
                    tmpImagen.FullPath = imagen.FileName;
                    publicacion.Imagen.Add(tmpImagen);
                }

                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                await uploadFiles(imagenes, publicacion.PublicacionId);
                return RedirectToAction(nameof(Index));
            }
            //VER QUE HACE ESTO
            //ViewData["UserEmail"] = new SelectList(_context.Usuario, "Email", "Email", publicacion.UserEmail);
            return View(publicacion);
        }
        private Boolean sonImagenesValidas(List<IFormFile> imagenes)
        {
            Boolean resultado = true;
            if (imagenes.Count == 0) resultado = false;
            int i = 0;
            while (i < imagenes.Count && resultado == true)
            {
                if (!imagenes[i].FileName.EndsWith(".jpg"))
                {
                    resultado = false;
                }
                else
                {
                    i++;
                }
            }
            return resultado;
        }
        public async Task<IActionResult> uploadFiles(List<IFormFile> files, int publicationID)
        {
            long size = files.Sum(f => f.Length);
            int i = 0;
            foreach (var formFile in files)
            {

                if (formFile.Length > 0)
                {
                    String extension = Path.GetExtension(formFile.FileName);

                    var filePath = _environment.WebRootPath + "\\images\\" + publicationID + "-" + i + extension;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                i++;
            }
            return Ok(new { count = files.Count, size });
        }
        // GET: Publicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicacion.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            ViewData["UserEmail"] = new SelectList(_context.Usuario, "Email", "Email", publicacion.UserEmail);
            return View(publicacion);
        }

        // POST: Publicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicacionId,Fecha,Texto,UserEmail")] Publicacion publicacion)
        {
            if (id != publicacion.PublicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacionExists(publicacion.PublicacionId))
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
            ViewData["UserEmail"] = new SelectList(_context.Usuario, "Email", "Email", publicacion.UserEmail);
            return View(publicacion);
        }

        // GET: Publicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicacion
                .Include(p => p.UserEmailNavigation)
                .FirstOrDefaultAsync(m => m.PublicacionId == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // POST: Publicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicacion = await _context.Publicacion.FindAsync(id);
            _context.Publicacion.Remove(publicacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacionExists(int id)
        {
            return _context.Publicacion.Any(e => e.PublicacionId == id);
        }
    }
}
