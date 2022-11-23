using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSocial.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 12, nullable: false),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    apellido = table.Column<string>(maxLength: 50, nullable: false),
                    preguntaSecreta = table.Column<string>(nullable: false),
                    respuestaSecreta = table.Column<string>(nullable: false),
                    fechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Amistad",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailSender = table.Column<string>(maxLength: 50, nullable: false),
                    emailReciver = table.Column<string>(maxLength: 50, nullable: false),
                    aceptada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amistad", x => x.id);
                    table.ForeignKey(
                        name: "FK_Amistad_UsuariosReceiver",
                        column: x => x.emailReciver,
                        principalTable: "Usuario",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Amistad_UsuariosSender",
                        column: x => x.emailSender,
                        principalTable: "Usuario",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publicacion",
                columns: table => new
                {
                    publicacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(nullable: false),
                    texto = table.Column<string>(maxLength: 200, nullable: false),
                    userEmail = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacion", x => x.publicacionId);
                    table.ForeignKey(
                        name: "FK_Publicacion_UsuarioCreador",
                        column: x => x.userEmail,
                        principalTable: "Usuario",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    publicacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    texto = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => new { x.publicacionId, x.email });
                    table.ForeignKey(
                        name: "FK_Comentario_Usuario",
                        column: x => x.email,
                        principalTable: "Usuario",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_Publicacion",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    imagenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullPath = table.Column<string>(maxLength: 200, nullable: false),
                    publicacionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.imagenId);
                    table.ForeignKey(
                        name: "FK_Imagen_Publicacion",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    publicacionId = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.publicacionId, x.email });
                    table.ForeignKey(
                        name: "FK_Like_Usuario",
                        column: x => x.email,
                        principalTable: "Usuario",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Publicacion",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amistad_emailReciver",
                table: "Amistad",
                column: "emailReciver");

            migrationBuilder.CreateIndex(
                name: "IX_Amistad_emailSender",
                table: "Amistad",
                column: "emailSender");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_email",
                table: "Comentario",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_publicacionId",
                table: "Imagen",
                column: "publicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_email",
                table: "Like",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacion_userEmail",
                table: "Publicacion",
                column: "userEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amistad");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Publicacion");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
