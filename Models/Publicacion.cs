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
        [DataType(DataType.Text)]
        [Display(Name = "Descripción de la publicación")]
        [Required(ErrorMessage = "Ingrese una descripción")]
        public string texto { get; set; }

        [NotMapped]
        public virtual ICollection<Usuario> likes { get; set; } //circular
        //[NotMapped]
        
        public virtual ICollection<Comentario> comentarios { get; set; }
        //[NotMapped]

        // POR ALGUN MOTIVO NO VALIDA CUANDO TIENE INFORMACIÓN DE ARCHIVOS
        //[DataType(DataType.EmailAddress)]
        
        public virtual ICollection<Imagen> imagenes { get; set; }
        //public virtual String imagenes { get; set; }


        /*public Publicacion()
        {
            likes = new List<Usuario>();
            comentarios = new List<Comentario>();
            imagenes = new List<Imagen>();
        }*/
    }
}
