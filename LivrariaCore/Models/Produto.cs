using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public abstract class Produto
    {

        protected string nome = string.Empty;
        public string Nome
        {
            get { return nome; }

            set
            {

                if (!string.IsNullOrWhiteSpace(value))
                {
                    nome = value;
                }
                else
                {
                    throw new ArgumentException("ERRO: Autor não pode ser vazio!!");
                }
            }


        }

        protected double preco;
        public double Preco
        {
            get { return preco; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("ERRO: Preco nao pode ser negativo");
                }
                preco = value;
            }

        }

        protected int quantidade;

        public int Quantidade
        {
            get { return quantidade; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("ERRO: Preco nao pode ser negativo");
                }
                quantidade = value;
            }

        }

        public Produto(string nome, double preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }


    }
}
