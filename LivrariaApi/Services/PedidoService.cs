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

            //Verifica se o livro existe no banco de dados
            var livros = _context.Livros //olha na tabela de livros
                .Where(x => pedidoDto.LivroIds //filtra somente os livros desejados
                .Contains(x.Id)) //cujo Id esteja presente na lista de Ids do pedido
                .ToList(); //executa a consulta e retorna os resultados como uma lista

            pedido.Livros = livros; //atribui a lista de livros ao pedido

            pedido.StatusPedido = Status.PAGAMENTO_PENDENTE;
            _context.Add(pedido);
            _context.SaveChanges();
        }

        public Pedido? AnalisarPedido(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "ERRO: ID DO PEDIDO A SER ANALISADO DEVE SER UM NÚMERO POSITIVO");
            }

            var pedidoLocalizado = _context.Pedidos
                                    .Include(p => p.Livros)
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
                                   .Include(p => p.Livros) //carrega os livros relacionados
                                   .FirstOrDefault(p => p.Id == id);

            if (pedidoCancelado == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");
            }

            pedidoCancelado.Livros.Clear(); //remove relacionamento (pois o pedido contém os livros contidos na outra tabela)
            _context.Remove(pedidoCancelado);
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
            pedidoDespachado.StatusPedido = Status.PROCESSANDO_PEDIDO;
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
                .Include(p => p.Livros) //Carrega os livros relacionados ao pedido
                .FirstOrDefault(p => p.Id == id); //Filtra pelo Id
            

            if (pedido == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: PEDIDO NÃO ENCONTRADO");

            }

            return pedido?.CalcularTotal() ?? 0;
        }
        


    }
}
