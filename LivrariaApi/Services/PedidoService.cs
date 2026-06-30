using LivrariaApi.Data;
using LivrariaCore.DTO_s;
using LivrariaCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LivrariaApi.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly LivrariaContext _context;

        public PedidoService(LivrariaContext context)
        {
            _context = context;
        }

        public void AdicionarPedido(PedidoDTO pedidoDto)
        {
            if(pedidoDto == null)
            {
                throw new ArgumentException(nameof(pedidoDto), "ERRO: PEDIDO NÃO PODE SER NULO");
            }

            var pedido = new Pedido(pedidoDto.IdPedido, pedidoDto.NomeCliente, pedidoDto.TelefoneCliente);

            //extrai apenas os ID da lista de itens
            var ids = pedidoDto.Itens.Select(l => l.LivroId).ToList();

            //Verifica se o livro existe no banco de dados
            var livros = _context.Livros //olha na tabela de livros
                .Where(x => ids.Contains(x.Id))//filtra passando os IDS dos itens, que contenha o Id do respectivo livro.
                .ToList(); //executa a consulta e retorna os resultados como uma lista

            _context.Add(pedido);
            _context.SaveChanges();

            foreach(var iPedido in pedidoDto.Itens)
            {
                var livro = livros.FirstOrDefault(x => x.Id == iPedido.LivroId);

                if (livro == null)
                {
                    throw new ArgumentException($"ERRO: LIVRO COM ID {iPedido.LivroId} NÃO ENCONTRADO");
                }

                if (livro.Quantidade < iPedido.QuantidadeLivros)
                {
                    throw new ArgumentException("ERRO: QUANTIDADE INSUFICIENTE");
                }
                else
                {

                   livro.Quantidade -= iPedido.QuantidadeLivros;
                    var novoItem = new ItemPedido(livro.Id, pedido.Id, iPedido.QuantidadeLivros, livro.CalculoPrecoUnitario());

                    

                    _context.Update(livro);
                    _context.Add(novoItem);
                    

                }


                
            }


            pedido.StatusPedido = Status.PROCESSANDO_PEDIDO;
            _context.SaveChanges();



        }

        public Pedido? AnalisarPedido(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "ERRO: ID DO PEDIDO A SER ANALISADO DEVE SER UM NÚMERO POSITIVO");
            }

            var pedidoLocalizado = _context.Pedidos
                                    .Include(p => p.ItemPedido)
                                    .FirstOrDefault(p => p.Id == id);

            if (pedidoLocalizado == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");
            }

            return pedidoLocalizado;
        }

        public void CancelarPedido(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "ERRO: ID DO PEDIDO A SER CANCELADO DEVE SER UM NÚMERO POSITIVO");
            }

            var pedidoCancelado = _context.Pedidos
                                   .Include(p => p.ItemPedido) //carrega os livros relacionados
                                   .FirstOrDefault(p => p.Id == id);

            if (pedidoCancelado == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");
            }

            foreach (var item in pedidoCancelado.ItemPedido)
            {
                var livro = _context.Livros.FirstOrDefault(l => l.Id == item.LivroId);

                if (livro == null)
                {
                    throw new ArgumentException($"ERRO: LIVRO COM ID {item.LivroId} NÃO ENCONTRADO");
                }

                livro.Quantidade += item.QuantidadeLivros;
                _context.Update(livro);

            }

            // remove cada item antes do pedido
            _context.ItemPedidos.RemoveRange(pedidoCancelado.ItemPedido);
            _context.Pedidos.Remove(pedidoCancelado);
            _context.SaveChanges();

        }

        public void DespacharPedido(int id)
        {

            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "ERRO: ID DO PEDIDO A SER DESPACHADO DEVE SER UM NÚMERO POSITIVO");
            }

            var pedidoDespachado = _context.Pedidos.FirstOrDefault(p => p.Id == id);

            if (pedidoDespachado == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");
            }
            pedidoDespachado.StatusPedido = Status.EM_TRANSITO;
            _context.Update(pedidoDespachado);
            _context.SaveChanges();


        }

        public double CalcularPedido(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "ERRO: ID DO PEDIDO A SER CALCULADO DEVE SER UM NÚMERO POSITIVO");
            }
            
            var pedido = _context.Pedidos
                .Include(p => p.ItemPedido) //Carrega os livros relacionados ao pedido
                .FirstOrDefault(p => p.Id == id); //Filtra pelo Id
            

            if (pedido == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");

            }

            return pedido?.CalcularTotal() ?? 0;
        }
        


    }
}
