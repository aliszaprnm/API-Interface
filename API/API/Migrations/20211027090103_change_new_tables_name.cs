using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class change_new_tables_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Roles_Roles_RoleId",
                table: "Account_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_NIK",
                table: "Account_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Tb_M_Role");

            migrationBuilder.RenameTable(
                name: "Account_Roles",
                newName: "Tb_T_AccountRole");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Roles_RoleId",
                table: "Tb_T_AccountRole",
                newName: "IX_Tb_T_AccountRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_Role",
                table: "Tb_M_Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_AccountRole",
                table: "Tb_T_AccountRole",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_M_Role_RoleId",
                table: "Tb_T_AccountRole",
                column: "RoleId",
                principalTable: "Tb_M_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_NIK",
                table: "Tb_T_AccountRole",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_M_Role_RoleId",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_NIK",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_AccountRole",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_Role",
                table: "Tb_M_Role");

            migrationBuilder.RenameTable(
                name: "Tb_T_AccountRole",
                newName: "Account_Roles");

            migrationBuilder.RenameTable(
                name: "Tb_M_Role",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_T_AccountRole_RoleId",
                table: "Account_Roles",
                newName: "IX_Account_Roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account_Roles",
                table: "Account_Roles",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Roles_Roles_RoleId",
                table: "Account_Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Roles_Tb_T_Account_NIK",
                table: "Account_Roles",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
