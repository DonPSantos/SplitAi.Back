using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class ReInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Couvert = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TableCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonTable",
                columns: table => new
                {
                    PeopleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TablesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTable", x => new { x.PeopleId, x.TablesId });
                    table.ForeignKey(
                        name: "FK_PersonTable_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonTable_Tables_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumption_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionPerson",
                columns: table => new
                {
                    ConsumptionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionPerson", x => new { x.ConsumptionsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ConsumptionPerson_Consumption_ConsumptionsId",
                        column: x => x.ConsumptionsId,
                        principalTable: "Consumption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionPerson_People_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_ItemId",
                table: "Consumption",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionPerson_ParticipantsId",
                table: "ConsumptionPerson",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TableId",
                table: "Items",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTable_TablesId",
                table: "PersonTable",
                column: "TablesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableCode",
                table: "Tables",
                column: "TableCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumptionPerson");

            migrationBuilder.DropTable(
                name: "PersonTable");

            migrationBuilder.DropTable(
                name: "Consumption");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
