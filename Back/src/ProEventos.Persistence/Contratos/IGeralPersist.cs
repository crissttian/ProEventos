using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersist<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteReange(T[] entity);

        Task<bool> SaveChangeAsync();
    }
}