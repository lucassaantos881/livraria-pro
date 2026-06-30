using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Pedidos_PedidoId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_PedidoId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Livros");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    LivroId = table.Column<int>(type: "INTEGER", nullable: false),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeLivros = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => new { x.LivroId, x.PedidoId });
                    table.ForeignKey(
                        name: "FK_ItemPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.AlterColumn<int>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Livros",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_PedidoId",
                table: "Livros",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Pedidos_PedidoId",
                table: "Livros",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
