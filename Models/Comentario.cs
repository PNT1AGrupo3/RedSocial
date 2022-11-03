using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.Models
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int comentarioId { get; set; }
        public string texto { get; set; }
        //public List<Usuario> usuarios;
        public virtual ICollection<Usuario> usuarios { get; set; }
        /*public Comentario()
        {
            usuarios = new List<Usuario>();
        }*/

    }
}
