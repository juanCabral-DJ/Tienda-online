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
    public class RepositoryProducto : BaseRepository<productos>, IRepositoryProductos
    {
        private readonly HSWContext _Context;
        private readonly ILoggerBase<productos> _Logger;

        public RepositoryProducto(HSWContext context, IConfiguration configuration, ILoggerBase<productos> logger) : base(context)
        {
            _Context = context;
            _Logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<productos, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retrieving productos entities");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving productos entities", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retrieving productos entities", e);
                result = OperationResult.Failure("An error occurred while retrieving productos entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving producto entity");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving producto entity", entity.Data);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving producto entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async override Task<OperationResult> Removeasync(productos entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("producto entity cannot be null");
                }

                productos? productoExist = await _Context.productos.FindAsync(entity.Id_producto);

                if (productoExist is null)
                {
                    _Logger.LogError("producto entity not found");
                    return OperationResult.Failure("producto entity not found.");
                }

                _Logger.LogInformation("deleting producto entity");


                result = await base.Removeasync(productoExist);

                _Logger.LogInformation("Producto eliminado con éxito.", entity.Id_producto);

                result = OperationResult.Success("Producto entity deleted successfully.", productoExist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(productos entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding producto entity: ${@entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to add a null producto entity.");
                    return OperationResult.Failure("Producto entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the producto type: {ex.Message}");
                _Logger.LogError("An error occurred while adding the producto");
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(productos entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null producto entity.");
                    return OperationResult.Failure("producto entity cannot be null");
                }

                _Logger.LogInformation("updating producto entity: ${@Entity}", entity);

                productos? addressupdate = await _Context.productos.FindAsync(entity.Id_producto);

                if (addressupdate is null)
                    return OperationResult.Failure("producto entity not found.");


                result = await base.Updateasync(addressupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the producto type: {ex.Message}");
                _Logger.LogError("An error occurred while updating the producto type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async override Task<OperationResult> Findasync(Expression<Func<productos, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retornando el producto que cumpla con la condicion");
                var users = await base.GetAllasync(filter);

                result = OperationResult.Success("Retornando el producto que cumpla con la condicion", users.Data);
            }
            catch (Exception e)
            {
                _Logger.LogError("Error retornando el producto que cumpla con la condicion", e);
                result = OperationResult.Failure("A ocurrido un error Retornando el producto que cumpla con la condicion.");
            }

            return result;
        } 

        public async Task<OperationResult> GetProductosPorCategoria(productos producto)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retrieving producto entity");

                if (producto.Id_categoria <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await _Context.productos.FindAsync(producto.Id_categoria);

                result = OperationResult.Success("Retrieving producto entity", producto.Id_categoria);

            }
            catch (Exception ex)
            {
                _Logger.LogError("Error retrieving producto entity", ex);
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {producto.Id_categoria}: {ex.Message}");
            }
            return result;
        }
    }
}
