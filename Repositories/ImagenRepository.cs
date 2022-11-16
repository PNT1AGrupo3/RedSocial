using RedSocial.Context;
using RedSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Repositories
{
    public class ImagenRepository : IImagenRepository
    {
        private RedSocialDatabaseContext _context;
        public ImagenRepository(RedSocialDatabaseContext context)
        {
            _context = context;
        }
        public void CreateImagen(Imagen imagen)
        {
            if (imagen.fullPath != "")
            {
                //copiar imagen en el servidor
            }
            
        }

        public void DeleteImagen(int id)
        {
            var imagen = _context.Imagen.SingleOrDefault(c => c.imagenId == id);
            _context.Imagen.Remove(imagen);
            _context.SaveChanges();
        }

        public Imagen GetImagenById(int id)
        {
            return (Imagen) _context.Imagen.Where(a => a.imagenId == id);
        }

        public IEnumerable<Imagen> GetImagenes()
        {
            return _context.Imagen.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
