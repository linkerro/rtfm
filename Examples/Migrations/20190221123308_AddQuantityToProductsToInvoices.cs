using Microsoft.EntityFrameworkCore.Migrations;

namespace Examples.Migrations
{
    public partial class AddQuantityToProductsToInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductsToInvoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductsToInvoices");
        }
    }
}
