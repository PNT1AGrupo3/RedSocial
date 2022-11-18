using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSocial.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    preguntaSecreta = table.Column<string>(nullable: false),
                    respuestaSecreta = table.Column<string>(nullable: false),
                    fechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuariouserId = table.Column<int>(nullable: true),
                    comentarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_UsuariouserId",
                        column: x => x.UsuariouserId,
                        principalTable: "Usuarios",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    putlicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(nullable: false),
                    texto = table.Column<string>(nullable: false),
                    UsuariouserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.putlicationId);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Usuarios_UsuariouserId",
                        column: x => x.UsuariouserId,
                        principalTable: "Usuarios",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);

                    // agregado por los likes  9/11/2022 
                    table.ForeignKey(
                        name: "FK_Likes_Usuarios_UsuariouserId",
                        column: x => x.UsuariouserId,
                        principalTable: "Usuarios",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    // fin agregado por los likes

                }



                );

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    comentarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    texto = table.Column<string>(nullable: false),
                    PublicacionputlicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.comentarioId);
                    table.ForeignKey(
                        name: "FK_Comentario_Publicaciones_PublicacionputlicationId",
                        column: x => x.PublicacionputlicationId,
                        principalTable: "Publicaciones",
                        principalColumn: "putlicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    imagenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullPath = table.Column<string>(nullable: true),
                    PublicacionputlicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.imagenId);
                    table.ForeignKey(
                        name: "FK_Imagen_Publicaciones_PublicacionputlicationId",
                        column: x => x.PublicacionputlicationId,
                        principalTable: "Publicaciones",
                        principalColumn: "putlicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_PublicacionputlicationId",
                table: "Comentario",
                column: "PublicacionputlicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_PublicacionputlicationId",
                table: "Imagen",
                column: "PublicacionputlicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UsuariouserId",
                table: "Publicaciones",
                column: "UsuariouserId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuariouserId",
                table: "Usuarios",
                column: "UsuariouserId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_comentarioId",
                table: "Usuarios",
                column: "comentarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Comentario_comentarioId",
                table: "Usuarios",
                column: "comentarioId",
                principalTable: "Comentario",
                principalColumn: "comentarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Publicaciones_PublicacionputlicationId",
                table: "Comentario");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Comentario");
        }
    }
}
