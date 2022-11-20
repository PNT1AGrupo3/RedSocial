using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RedSocial.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AmistadEmailReciverNavigation = new HashSet<Amistad>();
            AmistadEmailSenderNavigation = new HashSet<Amistad>();
            Comentario = new HashSet<Comentario>();
            Like = new HashSet<Like>();
            Publicacion = new HashSet<Publicacion>();
        }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail:")]
        [Required(ErrorMessage = "Ingrese su email")]
        [Key]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingrese una contraseña")]
        [StringLength(12, ErrorMessage = "La contraseña {0} debe tener al menos {2} caracteres de longitud", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "La contraseña debe tener al menos una letra minuscula, una mayuscula y un caracter especial. Longitud minima 6 caracteres, máxima 12")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ingrese su nombre")]
        [StringLength(50, ErrorMessage = "Maximo {0} caracteres")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese su apellido")]
        [StringLength(50, ErrorMessage = "Maximo {0} caracteres")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Pregunta secreta:")]
        [Required(ErrorMessage = "Ingrese una pregunta secreta")]
        public string PreguntaSecreta { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Respuesta pregunta secreta:")]
        [Required(ErrorMessage = "Ingrese la respuesta de su pregunta secreta")]
        public string RespuestaSecreta { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Amistad> AmistadEmailReciverNavigation { get; set; }
        public virtual ICollection<Amistad> AmistadEmailSenderNavigation { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<Publicacion> Publicacion { get; set; }
    }
}
