using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RedSocial.Models
{
    public partial class Amistad
    {
        public int Id { get; set; }
        public string EmailSender { get; set; }
        public string EmailReciver { get; set; }
        public bool Aceptada { get; set; }

        public virtual Usuario EmailReciverNavigation { get; set; }
        public virtual Usuario EmailSenderNavigation { get; set; }
    }
}
