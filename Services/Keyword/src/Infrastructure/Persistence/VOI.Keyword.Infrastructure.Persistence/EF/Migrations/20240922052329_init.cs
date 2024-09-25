using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VOI.Keyword.Infrastructure.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Voi_Keyword");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Voi_Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                schema: "Voi_Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                    table.UniqueConstraint("AK_Keywords_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "OutboxEvents",
                schema: "Voi_Keyword",
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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Code",
                schema: "Voi_Keyword",
                table: "Categories",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_Code",
                schema: "Voi_Keyword",
                table: "Keywords",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Voi_Keyword");

            migrationBuilder.DropTable(
                name: "Keywords",
                schema: "Voi_Keyword");

            migrationBuilder.DropTable(
                name: "OutboxEvents",
                schema: "Voi_Keyword");
        }
    }
}
