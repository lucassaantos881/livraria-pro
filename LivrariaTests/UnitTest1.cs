using LivrariaCore.Models;

namespace LivrariaTests
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(50, 65)] //preco 50 -> espera 65
        [InlineData(100, 115)] //preco 100 -> preco 115
        [InlineData(0, 15)] //preco 0 -> espera 15
        public void LivroFisico_CalculoPrecoUnitario_DeveAplicarFreteDe15Reais(double preco, double esperado)
        {
            //Arrange - prepara o cenário
            var livro = new LivroFisico(1, "Harry Potter", preco, "J.K Rowling", "DURA", 4);


            //Act - Executa o que quer testar
            var resultado = livro.CalculoPrecoUnitario();

            //Assert - Verifica se o resultado é esperado
            Assert.Equal(esperado, resultado);

        }

        [Fact]
        public void LivroDigital_CalculoPrecoUnitario_DeveAplicarDescontoDe15Porcento() {

            var livroDigital = new LivroDigital(2, "Five Nights at Freddy's The Fourth Closet", 100, "Scott Cawthon", "PNG", 2);

            var resultadoDigital = livroDigital.CalculoPrecoUnitario();

            Assert.Equal(85, resultadoDigital);
        
        }

        [Fact]
        public void LivroFisico_DeveRetornarExceçãoCasoPrecoNegativo()
        {
            Assert.Throws<ArgumentException>(() =>
                  new LivroFisico(1, "LivroNegativado", -10, "Autor", "DURA", 1));
        }

        [Fact]
        public void Pedido_CalcularTotal_DeveRetornarPrecoCorretoEntreCalculoDoisObjetos()
        {

            var pedido = new Pedido();

            var pedidoFisico = new ItemPedido(1, 2, 5, 35);
            var pedidoDigital = new ItemPedido(2, 4, 2, 28);

            pedido.ItemPedido.Add(pedidoFisico) ;
            pedido.ItemPedido.Add(pedidoDigital);

            var resultadoPedido = pedido.CalcularTotal();

            Assert.Equal(231, resultadoPedido);

        }
    }
}
