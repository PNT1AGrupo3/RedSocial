using RedSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Repositories
{
    public interface IImagenRepository
    {
        IEnumerable<Imagen> GetImagenes();
        Imagen GetImagenById(int id);
        void CreateImagen(Imagen cupcake);
        void DeleteImagen(int id);
        void SaveChanges();
        //IQueryable<Bakery> PopulateBakeriesDropDownList();
    }
}
