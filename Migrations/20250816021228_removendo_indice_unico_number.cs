using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProvaPub.Migrations
{
    /// <inheritdoc />
    public partial class removendo_indice_unico_number : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Numbers_Number",
                table: "Numbers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Numbers_Number",
                table: "Numbers",
                column: "Number",
                unique: true);
        }
    }
}
