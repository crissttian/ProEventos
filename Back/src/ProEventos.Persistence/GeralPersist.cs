using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class GeralPersist<T> : IGeralPersist<T> where T: class
    {
        private readonly ProEventosContext _context;
        private DbSet<T> _table = null;

        public GeralPersist(ProEventosContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void DeleteReange(T[] entityArray)
        {
            _table.RemoveRange(entityArray);
        }

        //SaveChanges
        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}