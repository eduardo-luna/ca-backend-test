using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Billing_and_BillingLines_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOnly",
                table: "Billing",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "BillingLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "BillingLines");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Billing",
                newName: "DateOnly");
        }
    }
}
