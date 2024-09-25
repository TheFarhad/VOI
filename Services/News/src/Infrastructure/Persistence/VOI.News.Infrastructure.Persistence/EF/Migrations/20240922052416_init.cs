using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VOI.News.Infrastructure.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Voi_News");

            migrationBuilder.CreateTable(
                name: "News",
                schema: "Voi_News",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.UniqueConstraint("AK_News_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "OutboxEvents",
                schema: "Voi_News",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    OwnerAggregate = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OwnerAggregateType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EventName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EventTypeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(MAX)", unicode: false, maxLength: 300, nullable: false),
                    UserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TraceId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SpanId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsProccessd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                schema: "Voi_News",
                columns: table => new
                {
                    NewsId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => new { x.NewsId, x.Id });
                    table.ForeignKey(
                        name: "FK_Keywords_News_NewsId",
                        column: x => x.NewsId,
                        principalSchema: "Voi_News",
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_Code",
                schema: "Voi_News",
                table: "News",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keywords",
                schema: "Voi_News");

            migrationBuilder.DropTable(
                name: "OutboxEvents",
                schema: "Voi_News");

            migrationBuilder.DropTable(
                name: "News",
                schema: "Voi_News");
        }
    }
}
