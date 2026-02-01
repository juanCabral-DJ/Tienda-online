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
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Repositories
{
    public class RepositoryUsuario : BaseRepository<usuario>
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<usuario> _Logger;

        public RepositoryUsuario(HSWContext context, IConfiguration configuration, ILoggerBase<usuario> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<usuario, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving usuarios entities");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving usuarios entities", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving usuarios entities", e);
                result = OperationResult.Failure("An error occurred while retrieving usuarios entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving usuario entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving usuario entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving usuario entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> Removeasync(usuario entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("usuario entity cannot be null");
                }

                usuario? usuarioExist = await _Context.usuario.FindAsync(entity.Id_usuario);

                if (usuarioExist is null)
                {
                    _Logger.LogError("usuario entity not found");
                    return OperationResult.Failure("usuario entity not found.");
                }

                _Logger.LogInformation("deleting usuario entity");

                 
                result = await base.Removeasync(usuarioExist);

                _Logger.LogInformation("usuario eliminado con éxito.", entity.Id_usuario);

                result = OperationResult.Success("Usuario entity deleted successfully.", usuarioExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(usuario entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding usuario entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null usuario entity.");
                    return OperationResult.Failure("usuario entity cannot be null.");
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
        public async override Task<OperationResult> Updateasync(usuario entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null usuario entity.");
                    return OperationResult.Failure("usuario entity cannot be null");
                }

                _Logger.LogInformation("updating usuario entity: ${@Entity}", entity);

                usuario addressupdate = await _Context.usuario.FindAsync(entity.Id_usuario);

                if (addressupdate is null)
                    return OperationResult.Failure("usuario entity not found.");
                 

                result = await base.Updateasync(addressupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the usuario type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the usuario type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> Findasync(Expression<Func<usuario, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el usuario que cumpla con la condicion");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando el usuario que cumpla con la condicion", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el usuario que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el usuario que cumpla con la condicion.");
            }

            return result;
        }
    }
}
