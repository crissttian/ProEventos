using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist<Evento> _geralPersist;
        public readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist<Evento> geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;
            
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersist.Add(model);
                if(await _geralPersist.SaveChangeAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                
                if(evento == null) return null;
                if(eventoId != model.Id) return null;

                _geralPersist.Update(model);                
                
                if(await _geralPersist.SaveChangeAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                
                if(evento == null) throw new Exception($"O evento para delete não foi encontrado. O Id informado foi {eventoId}");
                
                _geralPersist.Delete(evento);                
                
                return await _geralPersist.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventosByTemaAsync(tema, includePalestrantes);
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}