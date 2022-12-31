using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersist(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes)
        {
           IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lotes)
                .Include(rs => rs.RedeSocial);

                if(includePalestrantes)
                {
                    query = query
                    .Include(evt => evt.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
                }

                query = query
                    .OrderBy(o => o.Id)
                    .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
                
                return await query
                    .AsNoTracking()
                    .ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lotes)
                .Include(rs => rs.RedeSocial);

                if(includePalestrantes)
                {
                    query = query
                    .Include(evt => evt.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
                }

                return await query
                    .AsNoTracking()
                    .OrderBy(o => o.Id)
                    .ToArrayAsync();
        }
        
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lotes)
                .Include(rs => rs.RedeSocial);

                if(includePalestrantes)
                {
                    query = query
                    .Include(evt => evt.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
                }

                return await query
                    .Where(e => e.Id == eventoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }
    }
}