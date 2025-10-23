using HSW.Application.Base;
using HSW.Domain.Base;
using HSW.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly HSWContext _context;
        private DbSet<TEntity> Entity { get; set; }
        public BaseRepository(HSWContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }
        public async Task<OperationResult> Createasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                result = OperationResult.Success("Entidad creada con éxito.", entity);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ha ocurrido un error al guardar los datos", ex);
            }
            return result;
        }

        public async Task<OperationResult> Findasync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var entities = await Entity.Where(filter).ToListAsync();

                return OperationResult.Success("Entities retrieved successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving all entities: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetAllasync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var entities = await Entity.Where(filter).ToListAsync();

                return OperationResult.Success("Entities retrieved successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving all entities: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity != null)
                {
                    result = OperationResult.Success("Entity retrieved successfully.", entity);
                }
                else
                {
                    result = OperationResult.Failure($"Entity with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult> Removeasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                Entity.Remove(entity);
                await _context.SaveChangesAsync();
                result = OperationResult.Success("Entidad eliminada con éxito.", entity);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ha ocurrido un error al eliminar los datos", ex);
            }
            return result;
        }

        public async Task<OperationResult> Updateasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                Entity.Update(entity);
                await _context.SaveChangesAsync();
                result = OperationResult.Success("Entidad actualizada con éxito.", entity);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ha ocurrido un error al actualizar los datos", ex);
            }
            return result;
        }
    }
}
