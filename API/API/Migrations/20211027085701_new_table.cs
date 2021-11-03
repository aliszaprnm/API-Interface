using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class new_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_AccountNIK",
                table: "Account_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Account_Roles_AccountNIK",
                table: "Account_Roles");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "Account_Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_NIK",
                table: "Account_Roles",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_NIK",
                table: "Account_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles");

            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "Account_Roles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Roles_AccountNIK",
                table: "Account_Roles",
                column: "AccountNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_AccountNIK",
                table: "Account_Roles",
                column: "AccountNIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
