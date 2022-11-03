using Microsoft.EntityFrameworkCore;
using RedSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Context
{
    public class RedSocialDatabaseContext : DbContext
    {
        public RedSocialDatabaseContext(DbContextOptions<RedSocialDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        
    }
    

}
