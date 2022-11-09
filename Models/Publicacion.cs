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
        public int putlicationId { get; set; }
        public DateTime fecha { get; set; }
        public string texto { get; set; }


        //public List<Usuario> likes;
        //public List<Comentario> comentarios;
        //public List<Imagen> imagenes;
        [NotMapped]
        public virtual ICollection<Usuario> likes { get; set; } //circular
        //[NotMapped]
        public virtual ICollection<Comentario> comentarios { get; set; }
        //[NotMapped]
        public virtual ICollection<Imagen> imagenes { get; set; }


        /*public Publicacion()
        {
            likes = new List<Usuario>();
            comentarios = new List<Comentario>();
            imagenes = new List<Imagen>();
        }*/
    }
}
