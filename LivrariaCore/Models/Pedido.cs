using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public class Pedido
    {
        public Pedido()
        {
        }

        public Pedido(int idPedido, string nomeCliente, string telefoneCliente)
        {

            if (idPedido <= 0)
            {
                throw new ArgumentException(nameof(idPedido), "ERRO: ID DO PEDIDO DEVE SER UM NÚMERO POSITIVO");
            }

            if (string.IsNullOrEmpty(nomeCliente))
            {
                throw new ArgumentException(nameof(nomeCliente), "ERRO: NOME DO CLIENTE NÃO PODE SER NULO OU VAZIO");
            }

            if (string.IsNullOrEmpty(telefoneCliente))
            {
                throw new ArgumentException(nameof(telefoneCliente), "ERRO: TELEFONE DO CLIENTE NÃO PODE SER NULO OU VAZIO");
            }

            Id = idPedido;
            DataPedido = DateTime.Now;
            NomeCliente = nomeCliente;
            TelefoneCliente = telefoneCliente;
        }

        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string TelefoneCliente { get; set; } = string.Empty;
        public Status StatusPedido { get; set; }

        public ICollection<Livro> Livros { get; set; } = new List <Livro>();


        public double CalcularTotal()
        {
            return Livros.Sum(l => l.CalculoPrecoUnitario());
        }

    }
}
