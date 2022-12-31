using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(rs => rs.RedesSociais);
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            return await query
                .AsNoTracking()
                .OrderBy(o => o.Id)
                .ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(rs => rs.RedesSociais);
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            return await query
                .AsNoTracking()
                .Where(p => p.Nome.ToLower().Contains(nome))
                .OrderBy(o => o.Id)
                .ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(rs => rs.RedesSociais);
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            return await query
                .AsNoTracking()
                .Where(p => p.Id == palestranteId)
                .OrderBy(o => o.Id)
                .FirstOrDefaultAsync();
        }
    }
}