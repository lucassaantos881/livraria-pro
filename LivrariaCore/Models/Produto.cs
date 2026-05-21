using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public abstract class Produto
    {

        //O EFCORE PRECISA DE CONSTRUTOR VAZIO PARA RECRIAR OBJETOS DO BANCO DE DADOS, POR ISSO ELE É PROTEGIDO PARA QUE NINGUÉM FORA DA CLASSE POSSA USÁ-LO
        protected Produto()
        {
        }

        protected Produto(string nome, double preco, int quantidade)
        {
            //OBJETO SÓ EXISTE SE TODOS OS DADOS FOREM VÁLIDOS
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("ERRO: NOME NÃO PODE SER VAZIO");
            }

            if(preco < 0)
            {
                throw new ArgumentException("ERRO: PREÇO NÃO PODE SER NEGATIVO");
            }

            if (quantidade < 0)
            {
                throw new ArgumentException("ERRO: QUANTIDADE NÃO PODE SER NEGATIVA");
            }
            

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        //GET QUALQUER UM PODE LER, AGORA O SET APENAS A CLASSE OU CLASSES FILHAS PODEM ALTERAR
        public string Nome { get; protected set; } = string.Empty;
        public double Preco { get; protected set; }

        public int Quantidade { get; protected set; }

    }
}
