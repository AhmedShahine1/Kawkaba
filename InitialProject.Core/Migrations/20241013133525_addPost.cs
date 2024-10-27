using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kawkaba.Core.Migrations
{
    /// <inheritdoc />
    public partial class addPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                schema: "dbo",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestEmployees",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusRequestEmployee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestEmployees_Users_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestEmployees_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                schema: "dbo",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                schema: "dbo",
                table: "Images",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CompanyId",
                schema: "dbo",
                table: "Posts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestEmployees_CompanyId",
                schema: "dbo",
                table: "RequestEmployees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestEmployees_EmployeeId",
                schema: "dbo",
                table: "RequestEmployees",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                schema: "dbo",
                table: "Images",
                column: "PostId",
                principalSchema: "dbo",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CompanyId",
                schema: "dbo",
                table: "Users",
                column: "CompanyId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                schema: "dbo",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CompanyId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RequestEmployees",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Images_PostId",
                schema: "dbo",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostId",
                schema: "dbo",
                table: "Images");
        }
    }
}
