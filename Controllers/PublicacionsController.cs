using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        public async Task<IActionResult> Create([Bind("putlicationId,fecha,texto")] Publicacion publicacion, String[] imagenes)
        {
            
            if (ModelState.IsValid && this.sonImagenesValidas(imagenes))
            {
                // COMO OBTENER DATOS DE LAS IMAGENES
                //si puedo crear las imagenes 
                //
                


                publicacion.fecha = DateTime.Now;
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                //select IDENT_CURRENT('Publicaciones') or SCOPE_IDENTITY()
                var tmpPublicacion = await _context.Publicaciones
                .FirstOrDefaultAsync(m => m.putlicationId == id)


                Imagen prueba = new Imagen();
                prueba.fullPath = "prueba";


                _context.Add(prueba);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicacion);
        }
        private Boolean sonImagenesValidas(String[] imagenes)
        {
            //AVISAR AL USUARIO QUE NO SE CREO LA IMAGEN
            Boolean resultado = true;
            FileInfo info;
            int i = 0;
            /*foreach(String imagen in imagenes)
            {
                
                info = new FileInfo(imagen);
                if (info.Extension != ".jpg")
                {
                    resultado = false;
                }
                Console.WriteLine(info.Extension);
            }*/
            while (i<imagenes.Length && resultado == true)
            {
                info = new FileInfo(imagenes[i]);
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
