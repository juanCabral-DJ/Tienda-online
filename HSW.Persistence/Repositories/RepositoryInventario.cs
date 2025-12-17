using HSW.Application.Interface;
using HSW.Domain.Base;
using HSW.Domain.Entities;
using HSW.Persistence.Base;
using HSW.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Repositories
{
    public class RepositoryInventario : BaseRepository<inventario>, IRepositoryInventario
    {
        public readonly HSWContext _Context;
        public readonly ILoggerBase<inventario> _Logger;

        public RepositoryInventario(HSWContext context, ILoggerBase<inventario> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async Task<OperationResult> GetByProductoId(int productoId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving producto entity");

                if (productoId <= 0)
                {
                    result =  OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await _Context.inventario.FindAsync(productoId);

                result = OperationResult.Success("Retrieving producto entity", productoId);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving producto entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {productoId}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<inventario, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving inventarioa entities");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving inventarios entities", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving inventarios entities", e);
                result = OperationResult.Failure("An error occurred while retrieving los inventarios entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving inventario entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo y mayor a 0");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving inventario entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving inventario entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }
        public async override Task<OperationResult> Removeasync(inventario entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("inventario entity cannot be null");
                }

                inventario? inventarioExist = await _Context.inventario.FindAsync(entity.Id_inventario);

                if (inventarioExist is null)
                {
                    _Logger.LogError("inventario entity not found");
                    return OperationResult.Failure("inventario entity not found.");
                }

                _Logger.LogInformation("deleting inventario entity");


                result = await base.Removeasync(inventarioExist);

                _Logger.LogInformation("pedido eliminado con éxito.", entity.Id_inventario);

                result = OperationResult.Success("inventario entity deleted successfully.", inventarioExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(inventario entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding inventario entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null inventario entity.");
                    return OperationResult.Failure("inventario entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the inventario type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the inventario");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(inventario entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null inventario entity.");
                    return OperationResult.Failure("inventario entity cannot be null");
                }

                _Logger.LogInformation("updating inventario entity: ${@Entity}", entity);

                inventario? inventarioupdate = await _Context.inventario.FindAsync(entity.Id_inventario);

                if (inventarioupdate is null)
                    return OperationResult.Failure("inventario entity not found.");


                result = await base.Updateasync(inventarioupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the inventario type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the inventario type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }
        public async override Task<OperationResult> Findasync(Expression<Func<inventario, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el inventario que cumpla con la condicion");
                var pedido = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando el inventario que cumpla con la condicion", pedido.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el inventario que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el inventario que cumpla con la condicion.");
            }

            return result;
        }
    }
}
