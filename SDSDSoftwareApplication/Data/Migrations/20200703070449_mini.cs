using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDSDSoftwareApplication.Data.Migrations
{
    public partial class mini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentsId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentsId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DepartmentsId1",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Departments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentsId",
                table: "Projects",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentsId",
                table: "Projects",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentsId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentsId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId1",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Departments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentsId1",
                table: "Projects",
                column: "DepartmentsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentsId1",
                table: "Projects",
                column: "DepartmentsId1",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
