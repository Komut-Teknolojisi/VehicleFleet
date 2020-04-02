using System;
using System.Linq;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Domain.interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        T GetById(Guid id);

        bool Add(T entity);

        bool Update(T entity);

        bool Delete(Guid id);
    }
}