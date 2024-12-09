using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances_Control_App_API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanType",
                table: "FinancialPlanCategory",
                newName: "PlanCategoryType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanCategoryType",
                table: "FinancialPlanCategory",
                newName: "PlanType");
        }
    }
}
