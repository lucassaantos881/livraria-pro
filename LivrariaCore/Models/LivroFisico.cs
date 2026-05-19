using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public class LivroFisico : Livro
    {

        public string TipoCapa { get; set; }

        public LivroFisico(int id, string nome, double preco, string autor, string tipoCapa, int quantidade) : base(id, nome, preco, autor, quantidade)
        {
            TipoCapa = tipoCapa;
        }

        public override double CalculoPrecoUnitario()
        {
            double valorFrete = Preco + 15.00; // Valor fixo para o frete
            return valorFrete;
        }

    }
}
