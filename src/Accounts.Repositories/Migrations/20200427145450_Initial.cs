using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounts.Repositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    broker_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(36)", nullable: false),
                    is_disabled = table.Column<bool>(nullable: false),
                    type = table.Column<string>(type: "varchar(16)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_broker_id",
                schema: "accounts",
                table: "accounts",
                column: "broker_id");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_name",
                schema: "accounts",
                table: "accounts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_is_disabled",
                schema: "accounts",
                table: "accounts",
                column: "is_disabled");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_type",
                schema: "accounts",
                table: "accounts",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_broker_id_name",
                schema: "accounts",
                table: "accounts",
                columns: new[] { "broker_id", "name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts",
                schema: "accounts");
        }
    }
}
