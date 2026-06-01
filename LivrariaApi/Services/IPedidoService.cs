using LivrariaCore.Models;
using LivrariaCore.DTO_s;
namespace LivrariaApi.Services

{
    public interface IPedidoService
    {

        void AdicionarPedido(PedidoDTO pedidoDto);
        void DespacharPedido(int id);
        void CancelarPedido(int id);
        Pedido? AnalisarPedido(int id);
        double CalcularPedido(int id);
    }
}
