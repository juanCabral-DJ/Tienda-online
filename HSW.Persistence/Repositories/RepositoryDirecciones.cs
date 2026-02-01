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
    public class RepositoryDirecciones : BaseRepository<direcciones>
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<direcciones> _Logger;

        public RepositoryDirecciones(HSWContext context, IConfiguration configuration, ILoggerBase<direcciones> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<direcciones, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving direcciones entities");
                var cat = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving direcciones entities", cat.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving direcciones entities", e);
                result = OperationResult.Failure("An error occurred while retrieving direcciones entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving direcciones entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving direcciones entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving direcciones entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> Removeasync(direcciones entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("direcciones entity cannot be null");
                }

                direcciones? direccionExist = await _Context.direcciones.FindAsync(entity.Id_direccion);

                if (direccionExist is null)
                {
                    _Logger.LogError("direcciones entity not found");
                    return OperationResult.Failure("direcciones entity not found.");
                }

                _Logger.LogInformation("deleting direcciones entity");


                result = await base.Removeasync(direccionExist);

                _Logger.LogInformation("direccion eliminado con éxito.", entity.Id_direccion);

                result = OperationResult.Success("direccion entity deleted successfully.", direccionExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(direcciones entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding direccion entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null direccion entity.");
                    return OperationResult.Failure("direccion entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the direccion type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the direccion");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(direcciones entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null direccion entity.");
                    return OperationResult.Failure("direccion entity cannot be null");
                }

                _Logger.LogInformation("updating direccion entity: ${@Entity}", entity);

                direcciones addressupdate = await _Context.direcciones.FindAsync(entity.Id_direccion);

                if (addressupdate is null)
                    return OperationResult.Failure("direccion entity not found.");


                result = await base.Updateasync(addressupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the direccion type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the direccion type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> Findasync(Expression<Func<direcciones, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando la direccion que cumpla con la condicion");
                var direccion = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando la direccion que cumpla con la condicion", direccion.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando la direccion que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando la direccion que cumpla con la condicion.");
            }

            return result;
        }
    }
}
