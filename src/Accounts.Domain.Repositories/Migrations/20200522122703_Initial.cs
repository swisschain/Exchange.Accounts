using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Accounts.Domain.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 36, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wallet",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 36, nullable: false),
                    Type = table.Column<string>(maxLength: 16, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wallet_account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_BrokerId",
                schema: "accounts",
                table: "account",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_account_Name",
                schema: "accounts",
                table: "account",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_AccountId",
                schema: "accounts",
                table: "wallet",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_BrokerId",
                schema: "accounts",
                table: "wallet",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_Name",
                schema: "accounts",
                table: "wallet",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_Type",
                schema: "accounts",
                table: "wallet",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wallet",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "account",
                schema: "accounts");
        }
    }
}
