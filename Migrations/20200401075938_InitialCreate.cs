using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverySave.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    pid = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    region = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    is_picked = table.Column<string>(nullable: true),
                    is_delivered = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
