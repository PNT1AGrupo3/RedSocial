using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RedSocial.Models
{
    public partial class Publicacion
    {
        public Publicacion()
        {
            Comentario = new HashSet<Comentario>();
            Imagen = new HashSet<Imagen>();
            Like = new HashSet<Like>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicacionId { get; set; }
        public DateTime Fecha { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Descripción de la publicación")]
        [Required(ErrorMessage = "Ingrese una descripción")]
        public string Texto { get; set; }
        public string UserEmail { get; set; }

        public virtual Usuario UserEmailNavigation { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual ICollection<Imagen> Imagen { get; set; }
        public virtual ICollection<Like> Like { get; set; }
    }
}
