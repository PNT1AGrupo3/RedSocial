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
        public int UserId { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PreguntaSecreta { get; set; }

        public string RespuestaSecreta { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<Publicacion> Publicaciones { get; set; }
    }
}
