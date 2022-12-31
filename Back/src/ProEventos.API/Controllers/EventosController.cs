using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService){
            _eventoService = eventoService;
        }

        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            return await Task.Run(() => {
                return Ok("The Event API is running!!!");
            });
        }

        [HttpGet("{incluirPalestrantes:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAll([FromRoute]bool incluirPalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(incluirPalestrantes);
                if(eventos == null || !eventos.Any())  return NotFound();

                return Ok(eventos);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar recuperar eventos. Erro:{ex.Message}");
            }
            
        }

        [HttpGet("{id:int}/{incluirPalestrantes:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int id, bool incluirPalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetEventoByIdAsync(id, incluirPalestrantes);
                if(eventos == null) return NotFound();
            
                return Ok(eventos);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar recuperar eventos. Erro:{ex.Message}");
            } 
        }

        [HttpGet("{tema:alpha}/{incluirPalestrantes:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTema([FromRoute]string tema, bool incluirPalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByTemaAsync(tema, incluirPalestrantes);
                if(eventos == null) return NotFound(); 

                return Ok(eventos); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar recuperar eventos. Erro:{ex.Message}");
            } 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEvento([FromBody]Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao adicionar evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar adicionar eventos. Erro:{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]Evento model)
        {
            try
            {
                if(id != model.Id) return BadRequest("Os Ids informados não coincidem");
                
                var evento = await _eventoService.UpdateEvento(id, model);
                if(evento == null) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar atualizar o evento. Erro:{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var evento = await _eventoService.DeleteEvento(id);
                if(!evento) return BadRequest("Evento não deletado");

                 return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar excluir o evento. Erro:{ex.Message}");
            }
        }
    }
}
