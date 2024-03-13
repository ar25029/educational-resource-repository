using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileHandling.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Err_ImageTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standard = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Err_ImageTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Err_PdfTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard = table.Column<int>(type: "int", nullable: false),
                    ResourcePdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Err_PdfTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Err_VideoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard = table.Column<int>(type: "int", nullable: false),
                    ResourceVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Err_VideoTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Err_ImageTable");

            migrationBuilder.DropTable(
                name: "Err_PdfTable");

            migrationBuilder.DropTable(
                name: "Err_VideoTable");
        }
    }
}
