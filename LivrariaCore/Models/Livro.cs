using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public abstract class Livro : Produto
    {
      
        protected Livro()
        {
        }

        public Livro(int id, string nome, double preco, string autor, int quantidade) : base(nome, preco, quantidade)
        {

            //OBJETO SÓ EXISTE SE TODOS OS DADOS FOREM VÁLIDOS
            if (id < 0)
            {
                throw new ArgumentException("ERRO: ID NÃO PODE SER NEGATIVO");
            }

            if(string.IsNullOrEmpty(autor))
            { 
                throw new ArgumentException("ERRO: AUTOR NÃO PODE SER VAZIO");
            }

            Id = id;
            Autor = autor;
        }

        public int Id { get; set; }
        public string Autor { get; set; } = string.Empty;
        

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
