using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedSocial.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var redSocialBDContext = _context.Publicacion.Include(p => p.Imagen);
            return View(await redSocialBDContext.ToListAsync());
        }

        // GET: Publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
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

            //ViewData["UserEmail"] = new SelectList(_context.Usuario, "Email", "Email");
            return View();
        }

        // POST: Publicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicacionId,Fecha,Texto,UserEmail")] Publicacion publicacion, List<IFormFile> imagenes)
        {
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
            String user =Autenticacion.getSessionId(HttpContext).ToLower();
            String fileName = "";
            int i = 0;
            if (!this.sonImagenesValidas(imagenes))
            {
                ModelState.AddModelError("imagenes", "Agregue una o mas imagenes en formato .JPG");
            }
            if (ModelState.IsValid)
            {
                publicacion.Fecha = DateTime.Now;
                publicacion.UserEmail = user;
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                foreach (IFormFile imagen in imagenes)
                {
                    Imagen tmpImagen = new Imagen();
                    fileName = publicacion.PublicacionId + "-" + i;
                    tmpImagen.FullPath = "../images/" + fileName + ".jpg";
                    publicacion.Imagen.Add(tmpImagen);
                    await uploadFile(imagen, fileName);
                    i++;
                }
                _context.Update(publicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> uploadFile(IFormFile file, String filename)
        {
            String extension = Path.GetExtension(file.FileName);
            var filePath = _environment.WebRootPath + "\\images\\" + filename + extension;
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { count = 1, file.Length });
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
            
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
           
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicacion.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            //cambiar por el usuario autenticado
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
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id != publicacion.PublicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                publicacion.UserEmail = Autenticacion.getSessionId(HttpContext);

                publicacion.Fecha = DateTime.Now;
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
        /*public ActionResult GetImage(int imagenId)
        {
            //return 1;
        }*/
        // GET: Publicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
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
            if (!Autenticacion.estaAutenticado(HttpContext))
            {
                return RedirectToAction("Login", "Usuarios");
            }
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
