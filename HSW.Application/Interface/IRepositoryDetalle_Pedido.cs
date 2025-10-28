using HSW.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Application.Interface
{
    public interface IRepositoryDetalle_Pedido
    {
        public Task<OperationResult> GetDetallesPorPedido(int pedidoId);

    }
}
