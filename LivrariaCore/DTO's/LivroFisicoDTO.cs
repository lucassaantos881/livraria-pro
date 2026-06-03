using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LivrariaCore.DTO_s
{
    public class LivroFisicoDTO
    {

        //Serve como uma etiqueta, para dizer que o campo é obrigatório, e caso não seja preenchido, a mensagem de erro será exibida
        [Range(1, int.MaxValue, ErrorMessage = "O ID do livro tem que ser maior que 0")]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do livro é obrigatório")]
        [MinLength (3, ErrorMessage = "O nome do livro tem que ter pelo menos 3 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "O preço do livro tem que ser maior que 0")]
        public double Preco { get; set; }

        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome do autor tem que ter pelo menos 3 caracteres")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de capa é obrigatório")]
        [MinLength(3, ErrorMessage = "O tipo de capa tem que ter pelo menos 3 caracteres")]
        public string TipoCapa { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de livros tem que ser maior que 0")]
        public int Quantidade { get; set; }

    }
}
