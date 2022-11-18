using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedSocial.Context;
using RedSocial.Models;
using RedSocial.Repositories;


namespace RedSocial.Controllers
{
    public class PublicacionsController : Controller
    {
        private readonly RedSocialDatabaseContext _context;
        private IImagenRepository _repository;
        private IWebHostEnvironment _environment;
        public PublicacionsController(RedSocialDatabaseContext context, IWebHostEnvironment environment, IImagenRepository repository)
        {
            _repository = repository;
            _environment = environment;
            _context = context;
        }
        // GET: Imagenes de publicacion
        public IActionResult GetImage(int id)
        {
            Imagen requestedImage = _repository.GetImagenById(id);
            if (requestedImage != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requestedImage.fullPath;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, "image/jpeg");// requestedCupcake.ImageMimeType);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Publicacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Publicaciones.ToListAsync());
        }

        // GET: Publicacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .FirstOrDefaultAsync(m => m.putlicationId == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // GET: Publicacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publicacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("putlicationId,fecha,texto")] Publicacion publicacion, List<IFormFile> imagenes)
        {
            //
            //String[] imagenes=new string[10];
            if (!this.sonImagenesValidas(imagenes))
            {
                ModelState.AddModelError("imagenes", "Agregue una o mas imagenes en formato .JPG");
            }


            // SI IMAGENES ESTA VACIO NO LE AVISA AL USUARIO
            if (ModelState.IsValid)
            {

                publicacion.fecha = DateTime.Now;
                publicacion.imagenes = new List<Imagen>();
                //AGREGANDO IMAGENES DIRECTO A LA LISTA DE PUBLICACION
                foreach (IFormFile imagen in imagenes)
                {
                    Imagen tmpImagen = new Imagen();
                    tmpImagen.fullPath = imagen.FileName;
                    publicacion.imagenes.Add(tmpImagen);
                }
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                //SUBIR IMAGENES AL SERVIDOR
                await uploadFiles(imagenes, publicacion.putlicationId);
                return RedirectToAction(nameof(Index));
            }
            return View(publicacion);
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

                    var filePath = _environment.WebRootPath + "\\Images\\" + publicationID + "-" + i + extension;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                i++;
            }
            return Ok(new { count = files.Count, size });
        }

        private Boolean sonImagenesValidas(List<IFormFile> imagenes)
        {
            //AVISAR AL USUARIO QUE NO SE CREO LA IMAGEN
            Boolean resultado = true;
            FileInfo info;
            int i = 0;
            while (i < imagenes.Count && resultado == true)
            {
                info = new FileInfo(imagenes[i].FileName);
                if (info.Extension != ".jpg")
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

        // GET: Publicacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return View(publicacion);
        }

        // POST: Publicacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("putlicationId,fecha,texto")] Publicacion publicacion)
        {
            if (id != publicacion.putlicationId)
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
                    if (!PublicacionExists(publicacion.putlicationId))
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
            return View(publicacion);
        }

        // GET: Publicacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .FirstOrDefaultAsync(m => m.putlicationId == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // POST: Publicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);
            _context.Publicaciones.Remove(publicacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //UPLOAD FILEs

        private bool PublicacionExists(int id)
        {
            return _context.Publicaciones.Any(e => e.putlicationId == id);
        }
    }
}
