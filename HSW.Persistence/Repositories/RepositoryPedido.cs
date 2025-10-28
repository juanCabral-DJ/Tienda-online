using HSW.Application.Interface;
using HSW.Domain.Base;
using HSW.Domain.Entities;
using HSW.Persistence.Base;
using HSW.Persistence.Context;
using Microsoft.Extensions.Configuration;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Repositories
{
    public class RepositoryPedido : BaseRepository<pedido>, IRepositoryPedido
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<pedido> _Logger;

        public RepositoryPedido(HSWContext context, IConfiguration configuration, ILoggerBase<pedido> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<pedido, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving pedidos entities");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving pedidos entities", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving pedidos entities", e);
                result = OperationResult.Failure("An error occurred while retrieving pedidos entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving pedido entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving pedido entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving pedido entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }
        public async override Task<OperationResult> Removeasync(pedido entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("pedido entity cannot be null");
                }

                pedido? pedidoExist = await _Context.pedido.FindAsync(entity.Id_pedido);

                if (pedidoExist is null)
                {
                    _Logger.LogError("pedido entity not found");
                    return OperationResult.Failure("pedido entity not found.");
                }

                _Logger.LogInformation("deleting producto entity");


                result = await base.Removeasync(pedidoExist);

                _Logger.LogInformation("pedido eliminado con éxito.", entity.Id_pedido);

                result = OperationResult.Success("pedido entity deleted successfully.", pedidoExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(pedido entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding pedido entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null pedido entity.");
                    return OperationResult.Failure("pedido entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the pedido type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the pedido");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(pedido entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null pedido entity.");
                    return OperationResult.Failure("pedido entity cannot be null");
                }

                _Logger.LogInformation("updating pedido entity: ${@Entity}", entity);

                pedido? pedidoupdate = await _Context.pedido.FindAsync(entity.Id_pedido);

                if (pedidoupdate is null)
                    return OperationResult.Failure("pedido entity not found.");


                result = await base.Updateasync(pedidoupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the pedido type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the pedido type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }
        public async override Task<OperationResult> Findasync(Expression<Func<pedido, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el pedido que cumpla con la condicion");
                var pedido = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando el pedido que cumpla con la condicion", pedido.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el pedido que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el pedido que cumpla con la condicion.");
            }

            return result;
        }
        public async Task<OperationResult> GetPedidosPorCliente(pedido pedido)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el pedido que cumpla con la condicion");
                var users = await _Context.pedido.FindAsync(pedido.Id_usuario);

                result = OperationResult.Success("Retornando el pedido que cumpla con la condicion", pedido.Id_usuario);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el pedido que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el pedido que cumpla con la condicion.");
            }

            return result;
        }
    }
}
