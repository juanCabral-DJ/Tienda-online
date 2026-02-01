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
    public class RepositoryImagenes_Producto : BaseRepository<imagenes_producto>
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<imagenes_producto> _Logger;

        public RepositoryImagenes_Producto(HSWContext context, IConfiguration configuration, ILoggerBase<imagenes_producto> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<imagenes_producto, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving imagen producto entities");
                var img = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving imagen producto entities", img.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving imagen producto entities", e);
                result = OperationResult.Failure("An error occurred while retrieving imagen producto entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving imagen producto entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving imagen producto entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving imagen producto entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> Removeasync(imagenes_producto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("imagen producto entity cannot be null");
                }

                imagenes_producto? imgExist = await _Context.imagenes_producto.FindAsync(entity.Id_imagen);

                if (imgExist is null)
                {
                    _Logger.LogError("imagen producto entity not found");
                    return OperationResult.Failure("imagen producto entity not found.");
                }

                _Logger.LogInformation("deleting imagen producto entity");


                result = await base.Removeasync(imgExist);

                _Logger.LogInformation("imagen producto eliminado con éxito.", entity.Id_imagen);

                result = OperationResult.Success("imagen producto entity deleted successfully.", imgExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(imagenes_producto entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding imagen producto  entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null imagen producto  entity.");
                    return OperationResult.Failure("imagen producto  entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the imagen producto  type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the direccion");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(imagenes_producto entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null imagen producto  entity.");
                    return OperationResult.Failure("direccion entity cannot be null");
                }

                _Logger.LogInformation("updating imagen producto  entity: ${@Entity}", entity);

                imagenes_producto imgupdate = await _Context.imagenes_producto.FindAsync(entity.Id_imagen);

                if (imgupdate is null)
                    return OperationResult.Failure("imagen producto entity not found.");


                result = await base.Updateasync(imgupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the imagen producto  type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the imagen producto  type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> Findasync(Expression<Func<imagenes_producto, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando la imagen producto que cumpla con la condicion");
                var img = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando la imagen producto que cumpla con la condicion", img.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando la imagen producto que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando la imagen producto que cumpla con la condicion.");
            }

            return result;
        }
    }
}
