using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectHK3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    PasswordHash = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    ContactPhone = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    RegisteredOn = table.Column<DateOnly>(type: "DATE", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    ContactEmail = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    ContactPhone = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDetails_AdminLogin_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportType = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    GeneratedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportLogs_AdminLogin_GeneratedBy",
                        column: x => x.GeneratedBy,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    PasswordHash = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Department = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    HireDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpRegister_CompanyDetails_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PolicyName = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    CoverageAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "DATE", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policies_CompanyDetails_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    TableName = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrail_AdminLogin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditTrail_EmpRegister_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    RefreshToken = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthSession_AdminLogin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthSession_EmpRegister_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    HashedToken = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    TokenType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_AdminLogin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_EmpRegister_EmpId",
                        column: x => x.EmpId,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoliciesOnEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliciesOnEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliciesOnEmployees_EmpRegister_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliciesOnEmployees_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyRequestDetails_EmpRegister_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmpRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyRequestDetails_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyTotalDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    PolicySummary = table.Column<string>(type: "TEXT", nullable: true),
                    Benefits = table.Column<string>(type: "TEXT", nullable: true),
                    Exclutions = table.Column<string>(type: "TEXT", nullable: true),
                    TermsConditions = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTotalDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyTotalDescription_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyApprovalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyApprovalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyApprovalDetails_AdminLogin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyApprovalDetails_PolicyRequestDetails_RequestId",
                        column: x => x.RequestId,
                        principalTable: "PolicyRequestDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "TransactionLedgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "System"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLedgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionLedgers_PolicyApprovalDetails_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "PolicyApprovalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrail_AdminId",
                table: "AuditTrail",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrail_EmployeeId",
                table: "AuditTrail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthSession_AdminId",
                table: "AuthSession",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthSession_EmployeeId",
                table: "AuthSession",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_CreateBy",
                table: "CompanyDetails",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_EmpRegister_CompanyId",
                table: "EmpRegister",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_AdminId",
                table: "PasswordResetTokens",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_EmpId",
                table: "PasswordResetTokens",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CompanyId",
                table: "Policies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliciesOnEmployees_EmployeeId",
                table: "PoliciesOnEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliciesOnEmployees_PolicyId",
                table: "PoliciesOnEmployees",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyApprovalDetails_AdminId",
                table: "PolicyApprovalDetails",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyApprovalDetails_RequestId",
                table: "PolicyApprovalDetails",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDetails_EmployeeId",
                table: "PolicyRequestDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDetails_PolicyId",
                table: "PolicyRequestDetails",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDocuments_RequestId",
                table: "PolicyRequestDocuments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequestDocuments_UploadedBy",
                table: "PolicyRequestDocuments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTotalDescription_PolicyId",
                table: "PolicyTotalDescription",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportLogs_GeneratedBy",
                table: "ReportLogs",
                column: "GeneratedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLedgers_ApprovalId",
                table: "TransactionLedgers",
                column: "ApprovalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "AuthSession");

            migrationBuilder.DropTable(
                name: "HospitalInfo");

            migrationBuilder.DropTable(
                name: "NotificationLog");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "PoliciesOnEmployees");

            migrationBuilder.DropTable(
                name: "PolicyRequestDocuments");

            migrationBuilder.DropTable(
                name: "PolicyTotalDescription");

            migrationBuilder.DropTable(
                name: "ReportLogs");

            migrationBuilder.DropTable(
                name: "TransactionLedgers");

            migrationBuilder.DropTable(
                name: "PolicyApprovalDetails");

            migrationBuilder.DropTable(
                name: "PolicyRequestDetails");

            migrationBuilder.DropTable(
                name: "EmpRegister");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "CompanyDetails");

            migrationBuilder.DropTable(
                name: "AdminLogin");
        }
    }
}
