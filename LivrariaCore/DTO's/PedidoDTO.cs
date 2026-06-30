using LivrariaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LivrariaCore.DTO_s
{
    public class PedidoDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "O ID do pedido tem que ser maior que 0")]
        public int IdPedido { get; set; }

        //Serve como uma etiqueta, para dizer que o campo é obrigatório, e caso não seja preenchido, a mensagem de erro será exibida
        [Required (ErrorMessage = "O nome do cliente é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome do cliente deve ter pelo menos 3 caracteres")]
        public string NomeCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone do cliente é obrigatório")]
        public string TelefoneCliente { get; set; } = string.Empty;
        public List<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();

    }
}
