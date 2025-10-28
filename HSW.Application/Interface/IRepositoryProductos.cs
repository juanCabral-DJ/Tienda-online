using HSW.Domain.Base;
using HSW.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Application.Interface
{
    public interface IRepositoryProductos
    {
        public Task<OperationResult> GetProductosPorCategoria(productos producto);
    }
}
