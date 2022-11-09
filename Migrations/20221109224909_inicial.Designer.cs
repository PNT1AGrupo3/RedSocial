﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedSocial.Context;

namespace RedSocial.Migrations
{
    [DbContext(typeof(RedSocialDatabaseContext))]
    [Migration("20221109224909_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RedSocial.Models.Comentario", b =>
                {
                    b.Property<int>("comentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PublicacionputlicationId")
                        .HasColumnType("int");

                    b.Property<string>("texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("comentarioId");

                    b.HasIndex("PublicacionputlicationId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("RedSocial.Models.Imagen", b =>
                {
                    b.Property<int>("imagenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PublicacionputlicationId")
                        .HasColumnType("int");

                    b.Property<string>("fullPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("imagenId");

                    b.HasIndex("PublicacionputlicationId");

                    b.ToTable("Imagen");
                });

            modelBuilder.Entity("RedSocial.Models.Publicacion", b =>
                {
                    b.Property<int>("putlicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UsuariouserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("putlicationId");

                    b.HasIndex("UsuariouserId");

                    b.ToTable("Publicaciones");
                });

            modelBuilder.Entity("RedSocial.Models.Usuario", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UsuariouserId")
                        .HasColumnType("int");

                    b.Property<int?>("comentarioId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("preguntaSecreta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("respuestaSecreta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.HasIndex("UsuariouserId");

                    b.HasIndex("comentarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("RedSocial.Models.Comentario", b =>
                {
                    b.HasOne("RedSocial.Models.Publicacion", null)
                        .WithMany("comentarios")
                        .HasForeignKey("PublicacionputlicationId");
                });

            modelBuilder.Entity("RedSocial.Models.Imagen", b =>
                {
                    b.HasOne("RedSocial.Models.Publicacion", null)
                        .WithMany("imagenes")
                        .HasForeignKey("PublicacionputlicationId");
                });

            modelBuilder.Entity("RedSocial.Models.Publicacion", b =>
                {
                    b.HasOne("RedSocial.Models.Usuario", null)
                        .WithMany("publicaciones")
                        .HasForeignKey("UsuariouserId");
                });

            modelBuilder.Entity("RedSocial.Models.Usuario", b =>
                {
                    b.HasOne("RedSocial.Models.Usuario", null)
                        .WithMany("amigos")
                        .HasForeignKey("UsuariouserId");

                    b.HasOne("RedSocial.Models.Comentario", null)
                        .WithMany("usuarios")
                        .HasForeignKey("comentarioId");
                });
#pragma warning restore 612, 618
        }
    }
}