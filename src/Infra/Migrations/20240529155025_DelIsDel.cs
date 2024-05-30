using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class DelIsDel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDel",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsDel",
                table: "RoleAction");

            migrationBuilder.DropColumn(
                name: "IsDel",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsDel",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "IsDel",
                table: "AppUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDel",
                table: "UserRole",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "IsDel",
                table: "RoleAction",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDel",
                table: "Role",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "IsDel",
                table: "Division",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "IsDel",
                table: "AppUser",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "((1))");
        }
    }
}
