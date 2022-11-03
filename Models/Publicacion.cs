using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.Models
{
    public class Publicacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicacionId { get; set; }

        public DateTime Fecha { get; set; }

        public string Texto { get; set; }

        public List<Imagen> imagenes;

        public List<int> likes;
        
        public List<Comentario> comentarios;
    }
}
