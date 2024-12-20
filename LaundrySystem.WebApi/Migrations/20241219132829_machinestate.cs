using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaundrySystem.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class machinestate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Laveries_LaverieId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_LaverieId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "IsRunning",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "LaverieId",
                table: "Machines",
                newName: "State");

            migrationBuilder.AddColumn<int>(
                name: "LaundryId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_LaundryId",
                table: "Machines",
                column: "LaundryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Laveries_LaundryId",
                table: "Machines",
                column: "LaundryId",
                principalTable: "Laveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Laveries_LaundryId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_LaundryId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LaundryId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Machines",
                newName: "LaverieId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRunning",
                table: "Machines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_LaverieId",
                table: "Machines",
                column: "LaverieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Laveries_LaverieId",
                table: "Machines",
                column: "LaverieId",
                principalTable: "Laveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
