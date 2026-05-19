using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public abstract class Livro : Produto
    {
        protected int id;
        public int Id
        {


            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("ERRO: Id não pode ser negativo!!");
                }
                id = value;
            }

        }


        protected string autor = string.Empty;
        public string Autor
        {

            get { return autor; }

            set
            {

                if (!string.IsNullOrWhiteSpace(value))
                {
                    autor = value;
                }
                else
                {
                    throw new ArgumentException("ERRO: Autor não pode ser vazio!!");
                }
            }


        }

        public Livro(int id, string nome, double preco, string autor, int quantidade) : base(nome, preco, quantidade)
        {
            Id = id;
            Autor = autor;
        }


        public abstract double CalculoPrecoUnitario();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not Livro) return false;

            var other = obj as Livro;
            if (other == null) return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
