using LivrariaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.DTO_s
{
    public class PedidoDTO
    {

        public int IdPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string TelefoneCliente { get; set; } = string.Empty;
        public Status StatusPedido { get;  set; }
        public List<int> LivroIds { get; set; } = new List<int>();

    }
}
