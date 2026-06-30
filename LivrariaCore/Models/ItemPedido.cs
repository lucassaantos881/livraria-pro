using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LivrariaCore.Models
{
    public class ItemPedido
    {

        public ItemPedido(){

        }

        public int LivroId { get; set; }
        public int QuantidadeLivros { get; set; }

        public double PrecoUnitario { get; set; }

        public int PedidoId { get; set; }

        public ItemPedido(int livroId, int pedidoId, int quantidadeInserida, double precoUnitario)
        {
            if (livroId <= 0)
            {
                throw new ArgumentException("ERRO: ID DO LIVRO PRECISA SER MAIOR QUE 0");
            }

            if (pedidoId <= 0)
            {
                throw new ArgumentException("ERRO: ID DO PEDIDO PRECISA SER MAIOR QUE 0");
            }

            if (quantidadeInserida <= 0)
            {
                throw new ArgumentException("ERRO: QUANTIDADE DO PEDIDO PRECISA SER MAIOR QUE 0");

            }

            if (precoUnitario <= 0)
            {
                throw new ArgumentException("ERRO: PREÇO DO PEDIDO PRECISA SER MAIOR QUE 0");

            }


            LivroId = livroId;
            PedidoId = pedidoId;
            QuantidadeLivros = quantidadeInserida;
            PrecoUnitario = precoUnitario;

        }

    }
}
