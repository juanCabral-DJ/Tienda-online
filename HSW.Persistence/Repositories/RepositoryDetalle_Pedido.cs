using HSW.Application.Interface;
using HSW.Domain.Base;
using HSW.Domain.Entities;
using HSW.Persistence.Base;
using HSW.Persistence.Context;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Repositories
{
    public class RepositoryDetalle_Pedido : BaseRepository<detalle_pedido>, IRepositoryDetalle_Pedido
    {
        public readonly HSWContext _Context;
        public readonly ILoggerBase<detalle_pedido> _Logger;
        public RepositoryDetalle_Pedido(HSWContext context, ILoggerBase<detalle_pedido> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async Task<OperationResult> GetDetallesPorPedido(int pedidoId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving detalle de pedido by pedido id entity");

                if (pedidoId <= 0)
                {
                    result =  OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await _Context.detalle_pedido.FindAsync(pedidoId);

                result = OperationResult.Success("Retrieving detalle de pedido by pedido id entity", pedidoId);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving producto entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {pedidoId}: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<detalle_pedido, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving detalles de pedidos entities");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving detalles de pedidos entities", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving detalles de pedidos entities", e);
                result = OperationResult.Failure("An error occurred while retrieving  los detalles de pedidos entities.");
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
                    return OperationResult.Failure("El id tiene que ser positivo y mayor a 0");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving detalles de pedido entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving pedido entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }
        public async override Task<OperationResult> Removeasync(detalle_pedido entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("detalles de pedido entity cannot be null");
                }

                detalle_pedido? pedidoExist = await _Context.detalle_pedido.FindAsync(entity.Id_detalle);

                if (pedidoExist is null)
                {
                    _Logger.LogError("detalle de pedido entity not found");
                    return OperationResult.Failure("detalle de pedido entity not found.");
                }

                _Logger.LogInformation("deleting detalle de pedido entity");


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
        public async override Task<OperationResult> Createasync(detalle_pedido entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding detalles de pedido entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null detalles de pedido entity.");
                    return OperationResult.Failure("detalle de pedido entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the detalle de pedido type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the detalle de pedido");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(detalle_pedido entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null detalle de pedido entity.");
                    return OperationResult.Failure("detalle de pedido entity cannot be null");
                }

                _Logger.LogInformation("updating detalle de pedido entity: ${@Entity}", entity);

                detalle_pedido? pedidoupdate = await _Context.detalle_pedido.FindAsync(entity.Id_pedido);

                if (pedidoupdate is null)
                    return OperationResult.Failure(" detalle de pedido entity not found.");


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
        public async override Task<OperationResult> Findasync(Expression<Func<detalle_pedido, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el detalle de pedido que cumpla con la condicion");
                var pedido = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando el detalle de pedido que cumpla con la condicion", pedido.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el detalle de pedido que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el detalle de pedido que cumpla con la condicion.");
            }

            return result;
        }
    }
}
