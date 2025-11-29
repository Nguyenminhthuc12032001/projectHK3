using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectHK3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AuthSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "AuthSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PolicyRequestDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    FileURL = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRequestDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyRequestDocuments_EmpRegister_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyRequestDocuments_PolicyRequestDetails_RequestId",
                        column: x => x.RequestId,
                        principalTable: "PolicyRequestDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDocuments_RequestId",
                table: "PolicyRequestDocuments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDocuments_UploadedBy",
                table: "PolicyRequestDocuments",
                column: "UploadedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyRequestDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AuthSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "AuthSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
