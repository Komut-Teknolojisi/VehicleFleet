using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VehicleFleet.Domain.Entities;
using VehicleFleet.Domain.interfaces;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfBaseReporitory<T> : IRepository<T> where T : BaseEntity
    {
        private readonly VehicleFleetContext _context;
        private readonly ILogger<EfBaseReporitory<T>> _logger;

        public EfBaseReporitory(VehicleFleetContext context, ILogger<EfBaseReporitory<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual bool Add(T entity)
        {
            try
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.SaveChanges();

                _logger.LogInformation("entity added", entity.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error", entity);
                return false;
            }
        }

        public virtual bool Delete(Guid id)
        {
            try
            {
                T entity = _context.Set<T>().Find(id);
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                _logger.LogInformation("entity deleted", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error", id);
                return false;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                _logger.LogInformation("entity updated", entity.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", entity);
                return false;
            }
        }
    }
}