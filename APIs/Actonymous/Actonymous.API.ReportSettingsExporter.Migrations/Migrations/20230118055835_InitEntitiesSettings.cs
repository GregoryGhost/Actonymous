using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Actonymous.API.ReportSettingsExporter.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitEntitiesSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractNumber = table.Column<string>(type: "text", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContractFile = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    HeaderFullname = table.Column<string>(type: "text", nullable: false),
                    HeaderPosition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExecutorInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    HeaderFullname = table.Column<string>(type: "text", nullable: false),
                    HeaderPosition = table.Column<string>(type: "text", nullable: false),
                    RatePerHour = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutorInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraCredentials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ServerAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MorpherSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccessToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorpherSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateFilesInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActTemplateFile = table.Column<byte[]>(type: "bytea", nullable: false),
                    TaskTemplateFile = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateFilesInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutorInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ContractInfoId = table.Column<long>(type: "bigint", nullable: false),
                    TemplateFilesInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateSettings_ContractInfo_ContractInfoId",
                        column: x => x.ContractInfoId,
                        principalTable: "ContractInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateSettings_CustomerInfo_CustomerInfoId",
                        column: x => x.CustomerInfoId,
                        principalTable: "CustomerInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateSettings_ExecutorInfo_ExecutorInfoId",
                        column: x => x.ExecutorInfoId,
                        principalTable: "ExecutorInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateSettings_TemplateFilesInfo_TemplateFilesInfoId",
                        column: x => x.TemplateFilesInfoId,
                        principalTable: "TemplateFilesInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportReportSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JiraCredentialsId = table.Column<long>(type: "bigint", nullable: false),
                    TemplateSettingsId = table.Column<long>(type: "bigint", nullable: false),
                    MorpherSettingsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReportSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReportSettings_JiraCredentials_JiraCredentialsId",
                        column: x => x.JiraCredentialsId,
                        principalTable: "JiraCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportReportSettings_MorpherSettings_MorpherSettingsId",
                        column: x => x.MorpherSettingsId,
                        principalTable: "MorpherSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportReportSettings_TemplateSettings_TemplateSettingsId",
                        column: x => x.TemplateSettingsId,
                        principalTable: "TemplateSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportSettings_JiraCredentialsId",
                table: "ExportReportSettings",
                column: "JiraCredentialsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportSettings_MorpherSettingsId",
                table: "ExportReportSettings",
                column: "MorpherSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportSettings_TemplateSettingsId",
                table: "ExportReportSettings",
                column: "TemplateSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_ContractInfoId",
                table: "TemplateSettings",
                column: "ContractInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_CustomerInfoId",
                table: "TemplateSettings",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_ExecutorInfoId",
                table: "TemplateSettings",
                column: "ExecutorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_TemplateFilesInfoId",
                table: "TemplateSettings",
                column: "TemplateFilesInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportReportSettings");

            migrationBuilder.DropTable(
                name: "JiraCredentials");

            migrationBuilder.DropTable(
                name: "MorpherSettings");

            migrationBuilder.DropTable(
                name: "TemplateSettings");

            migrationBuilder.DropTable(
                name: "ContractInfo");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "ExecutorInfo");

            migrationBuilder.DropTable(
                name: "TemplateFilesInfo");
        }
    }
}
