using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaCore.Models
{
    public enum Status
    {
        PAGAMENTO_PENDENTE,
        PAGAMENTO_APROVADO,
        PAGAMENTO_RECUSADO,
        PROCESSANDO_PEDIDO,
        EM_TRANSITO,
        CANCELADO,
        ENTREGUE
    }
}
