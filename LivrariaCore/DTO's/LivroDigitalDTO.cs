using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.DTO_s
{
    public class LivroDigitalDTO
    {

        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public double Preco { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Formato { get; set; } = string.Empty;
        public int Quantidade { get; set; }

    }
}
