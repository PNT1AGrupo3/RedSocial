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
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail:")]
        [Required(ErrorMessage = "Ingrese su email")]
        [Key]
        public string email { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "La contraseña {0} debe tener al menos {2} caracteres de longitud", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "La contraseña debe tener al menos una letra minuscula, una mayuscula y un caracter especial. Longitud minima 6 caracteres, máxima 12")]
             
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string password { get; set; }

        //public int userId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Pregunta secreta:")]
        [Required(ErrorMessage = "Ingrese una pregunta secreta")]
        public string preguntaSecreta { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Respuesta pregunta secreta:")]
        [Required(ErrorMessage = "Ingrese la respuesta de su pregunta secreta")]
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
