using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kawkaba.Core.Migrations
{
    /// <inheritdoc />
    public partial class addcodetoCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyCode",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyCode",
                schema: "dbo",
                table: "Users",
                column: "CompanyCode",
                unique: true,
                filter: "[CompanyCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyCode",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyCode",
                schema: "dbo",
                table: "Users");
        }
    }
}
