using HSW.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Application.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> Createasync(TEntity entity);
        Task<OperationResult> Updateasync(TEntity entity);
        Task<OperationResult> Removeasync(TEntity entity);
        Task<OperationResult> Findasync(Expression<Func<TEntity, bool>> filter); 

    }
}
