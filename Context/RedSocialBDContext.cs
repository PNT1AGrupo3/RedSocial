using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RedSocial.Models
{
    public partial class RedSocialBDContext : DbContext
    {
        public RedSocialBDContext()
        {
        }

        public RedSocialBDContext(DbContextOptions<RedSocialBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amistad> Amistad { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\ORT2022C2;Database=RedSocialBD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amistad>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aceptada).HasColumnName("aceptada");

                entity.Property(e => e.EmailReciver)
                    .IsRequired()
                    .HasColumnName("emailReciver")
                    .HasMaxLength(50);

                entity.Property(e => e.EmailSender)
                    .IsRequired()
                    .HasColumnName("emailSender")
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmailReciverNavigation)
                    .WithMany(p => p.AmistadEmailReciverNavigation)
                    .HasForeignKey(d => d.EmailReciver)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Amistad_UsuariosReceiver");

                entity.HasOne(d => d.EmailSenderNavigation)
                    .WithMany(p => p.AmistadEmailSenderNavigation)
                    .HasForeignKey(d => d.EmailSender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Amistad_UsuariosSender");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => new { e.PublicacionId, e.Email })
                    .HasName("PK_Comentarios");

                entity.Property(e => e.PublicacionId)
                    .HasColumnName("publicacionId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasMaxLength(200);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comentario_Usuario");

                entity.HasOne(d => d.Publicacion)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.PublicacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comentario_Publicacion");
            });

            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.Property(e => e.ImagenId).HasColumnName("imagenId");

                entity.Property(e => e.FullPath)
                    .IsRequired()
                    .HasColumnName("fullPath")
                    .HasMaxLength(200);

                entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");

                entity.HasOne(d => d.Publicacion)
                    .WithMany(p => p.Imagen)
                    .HasForeignKey(d => d.PublicacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Imagen_Publicacion");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.PublicacionId, e.Email })
                    .HasName("PK_Likes");

                entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_Usuario");

                entity.HasOne(d => d.Publicacion)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.PublicacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_Publicacion");
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasMaxLength(200);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("userEmail")
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Publicacion)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Publicacion_UsuarioCreador");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK_Usuarios");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasMaxLength(50);

                entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(12);

                entity.Property(e => e.PreguntaSecreta)
                    .IsRequired()
                    .HasColumnName("preguntaSecreta");

                entity.Property(e => e.RespuestaSecreta)
                    .IsRequired()
                    .HasColumnName("respuestaSecreta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
