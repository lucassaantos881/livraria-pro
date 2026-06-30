using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LivrariaCore.DTO_s
{
    public class ItemPedidoDTO
    {

           [Range(1, int.MaxValue, ErrorMessage = "ID DO PEDIDO tem que ser maior que 0")]
           public int LivroId { get; set; }

           [Range(1, int.MaxValue, ErrorMessage = "QUANTIDADE DO PEDIDO tem que ser maior que 0")]
           public int QuantidadeLivros { get; set; }


    }
}
