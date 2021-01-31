using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
         Task<T> InsertAsync(T item);
         Task<T> UpdateAsync(T item);
         Task<Boolean> DeleteAync(Guid id);
         Task<T> SelectAsync(Guid id);
         Task<IEnumerable<T>> GetAllAsync();
         Task<bool> ExistsAsync(Guid id);
    }
}