using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RedSocial.Models
{
    public partial class Comentario
    {
        public int PublicacionId { get; set; }
        public string Email { get; set; }
        public string Texto { get; set; }

        public virtual Usuario EmailNavigation { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}
