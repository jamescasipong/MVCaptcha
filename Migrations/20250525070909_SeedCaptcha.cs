using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCaptcha.Migrations
{
    /// <inheritdoc />
    public partial class SeedCaptcha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Captchas",
                columns: new[] { "id", "captcha_name", "captcha_value", "image_url", "level" },
                values: new object[,]
                {
                    { 1, "easy1.png", "4321", "/image/captcha/easy1.png", "E" },
                    { 2, "easy2.png", "45687", "/image/captcha/easy2.png", "E" },
                    { 3, "easy3.png", "965774123", "/image/captcha/easy3.png", "E" },
                    { 4, "normal1.png", "sPdY", "/image/captcha/normal1.png", "N" },
                    { 5, "normal2.png", "cRse", "/image/captcha/normal2.png", "N" },
                    { 6, "normal3.png", "opMuMI", "/image/captcha/normal3.png", "N" },
                    { 7, "hard1.png", "1ess2", "/image/captcha/hard1.png", "H" },
                    { 8, "hard2.png", "2wP34", "/image/captcha/hard2.png", "H" },
                    { 9, "hard3.png", "Lz00Oda", "/image/captcha/hard3.png", "H" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Captchas",
                keyColumn: "id",
                keyValue: 9);
        }
    }
}
