using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ganti_nama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "Tb_T_University");

            migrationBuilder.RenameTable(
                name: "Profilings",
                newName: "Tb_T_Profiling");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Tb_M_Employee");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Tb_M_Education");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Tb_T_Account");

            migrationBuilder.RenameIndex(
                name: "IX_Profilings_EducationId",
                table: "Tb_T_Profiling",
                newName: "IX_Tb_T_Profiling_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UniversityId",
                table: "Tb_M_Education",
                newName: "IX_Tb_M_Education_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_University",
                table: "Tb_T_University",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_Profiling",
                table: "Tb_T_Profiling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_Employee",
                table: "Tb_M_Employee",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_Education",
                table: "Tb_M_Education",
                column: "EducationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_Account",
                table: "Tb_T_Account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_M_Education_Tb_T_University_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId",
                principalTable: "Tb_T_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Account_Tb_M_Employee_NIK",
                table: "Tb_T_Account",
                column: "NIK",
                principalTable: "Tb_M_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId",
                principalTable: "Tb_M_Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                table: "Tb_T_Profiling",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_T_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Account_Tb_M_Employee_NIK",
                table: "Tb_T_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_University",
                table: "Tb_T_University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_Profiling",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_Account",
                table: "Tb_T_Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_Employee",
                table: "Tb_M_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_Education",
                table: "Tb_M_Education");

            migrationBuilder.RenameTable(
                name: "Tb_T_University",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "Tb_T_Profiling",
                newName: "Profilings");

            migrationBuilder.RenameTable(
                name: "Tb_T_Account",
                newName: "Accounts");

            migrationBuilder.RenameTable(
                name: "Tb_M_Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Tb_M_Education",
                newName: "Educations");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_T_Profiling_EducationId",
                table: "Profilings",
                newName: "IX_Profilings_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_M_Education_UniversityId",
                table: "Educations",
                newName: "IX_Educations_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
