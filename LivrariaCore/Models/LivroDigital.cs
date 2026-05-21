using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public class LivroDigital : Livro
    {

        

        public LivroDigital(int id, string nome, double preco, string autor, string formato, int quantidade) : base(id, nome, preco, autor, quantidade)
        {
            Formato = formato;
        }

        public string Formato { get; private set; } = string.Empty;
        public override double CalculoPrecoUnitario()
        {

            
            return Preco - (Preco * 0.15);
        }

    }
}
