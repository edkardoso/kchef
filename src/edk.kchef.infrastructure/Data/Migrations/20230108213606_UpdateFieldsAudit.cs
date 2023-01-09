using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edk.Kchef.Infrastructure.Data.Migrations
{
    public partial class UpdateFieldsAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AuditDate",
                table: "Products",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuditUserId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "AuditDate",
                table: "ProductPrice",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuditUserId",
                table: "ProductPrice",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ProductPrice",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AuditUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AuditDate",
                table: "ProductPrice");

            migrationBuilder.DropColumn(
                name: "AuditUserId",
                table: "ProductPrice");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProductPrice");
        }
    }
}
