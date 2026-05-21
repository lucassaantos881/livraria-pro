using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public class LivroFisico : Livro
    {

        protected LivroFisico() { }

        public LivroFisico(int id, string nome, double preco, string autor, string tipoCapa, int quantidade) : base(id, nome, preco, autor, quantidade)
        {
            TipoCapa = tipoCapa;
        }

        public string TipoCapa { get; private set; } = string.Empty;
        public override double CalculoPrecoUnitario()
        {

            return Preco + 15.00;
        }

    }
}
