using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace MVCaptcha.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Captchas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    captcha_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    captcha_value = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    image_url = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    level = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captchas", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    datetime_started = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    datetime_ended = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    difficulty = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    score = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.session_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captchas");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
