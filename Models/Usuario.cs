using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string preguntaSecreta { get; set; }
        public string respuestaSecreta { get; set; }
        public DateTime fechaCreacion { get; set; }

        //public List<Publicacion> publicaciones;
        //public List<Usuario> amigos;
        //[NotMapped]
        public virtual ICollection<Publicacion> publicaciones { get; set; }
        //[NotMapped]
        public virtual ICollection<Usuario> amigos { get; set; }
        //public Usuario()
        //{
        //    publicaciones = new List<Publicacion>();
        //}
    }
}
