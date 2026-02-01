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
    public class RepositoryCategoria : BaseRepository<categoria>
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<categoria> _Logger;

        public RepositoryCategoria(HSWContext context, IConfiguration configuration, ILoggerBase<categoria> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<categoria, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving categoria entities");
                var cat = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving categoria entities", cat.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving categoria entities", e);
                result = OperationResult.Failure("An error occurred while retrieving categoria entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving categoria entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving categoria entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving categoria entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> Removeasync(categoria entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("categoria entity cannot be null");
                }

                categoria? cateogoriaExist = await _Context.categoria.FindAsync(entity.Id_categoria);

                if (cateogoriaExist is null)
                {
                    _Logger.LogError("categoria entity not found");
                    return OperationResult.Failure("categoria entity not found.");
                }

                _Logger.LogInformation("deleting categoria entity");


                result = await base.Removeasync(cateogoriaExist);

                _Logger.LogInformation("categoria eliminado con éxito.", entity.Id_categoria);

                result = OperationResult.Success("categoria entity deleted successfully.", cateogoriaExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(categoria entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding categoria entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null categoria entity.");
                    return OperationResult.Failure("categoria entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the usuario type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the usuario");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(categoria entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null categoria entity.");
                    return OperationResult.Failure("categoria entity cannot be null");
                }

                _Logger.LogInformation("updating usuario entity: ${@Entity}", entity);

                categoria addressupdate = await _Context.categoria.FindAsync(entity.Id_categoria);

                if (addressupdate is null)
                    return OperationResult.Failure("categoria entity not found.");


                result = await base.Updateasync(addressupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the categoria type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the categoria type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> Findasync(Expression<Func<categoria, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando la categoria que cumpla con la condicion");
                var categoria = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando la categoria que cumpla con la condicion", categoria.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando la categoria que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando la categoria que cumpla con la condicion.");
            }

            return result;
        }
    }
}
