using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ModalAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationStatus",
                table: "AspNetUsers");
        }
    }
}
