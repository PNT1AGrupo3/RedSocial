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
    [Migration("20221027015149_Inicial")]
    partial class Inicial
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
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("usuarioUserId")
                        .HasColumnType("int");

                    b.HasKey("ComentarioId");

                    b.HasIndex("usuarioUserId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("RedSocial.Models.Imagen", b =>
                {
                    b.Property<int>("ImagenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImagenId");

                    b.ToTable("Imagen");
                });

            modelBuilder.Entity("RedSocial.Models.Publicacion", b =>
                {
                    b.Property<int>("PublicacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioUserId")
                        .HasColumnType("int");

                    b.HasKey("PublicacionId");

                    b.HasIndex("UsuarioUserId");

                    b.ToTable("Publicaciones");
                });

            modelBuilder.Entity("RedSocial.Models.Usuario", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreguntaSecreta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RespuestaSecreta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("RedSocial.Models.Comentario", b =>
                {
                    b.HasOne("RedSocial.Models.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioUserId");
                });

            modelBuilder.Entity("RedSocial.Models.Publicacion", b =>
                {
                    b.HasOne("RedSocial.Models.Usuario", null)
                        .WithMany("Publicaciones")
                        .HasForeignKey("UsuarioUserId");
                });
#pragma warning restore 612, 618
        }
    }
}