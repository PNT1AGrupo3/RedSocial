using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.Models
{
    public class Imagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imagenId { get; set; }
        public string fullPath { get; set; }
        public string getFileName()
        {
            return "ver como se conforma el full path en el web server";
        }

    }
}
