using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ShoeStore_Manager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfitMargin",
                table: "Products",
                newName: "SellingPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "Products",
                newName: "ProfitMargin");
        }
    }
}
