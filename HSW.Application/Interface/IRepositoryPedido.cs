using HSW.Domain.Base;
using HSW.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Application.Interface
{
    public interface IRepositoryPedido
    {
        public Task<OperationResult> GetPedidosPorCliente(pedido pedido);
    }
}
