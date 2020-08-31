using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventshuffle.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateOptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateOptions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    DateOptionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_DateOptions_DateOptionId",
                        column: x => x.DateOptionId,
                        principalTable: "DateOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateOptions_EventId",
                table: "DateOptions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_DateOptionId",
                table: "Votes",
                column: "DateOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_EventId",
                table: "Votes",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "DateOptions");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
