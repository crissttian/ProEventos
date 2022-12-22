using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/evento")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context){
            _context = context;
        }

        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            return await Task.Run(() => {
                return Ok("The Event API is running!!!");
            });
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
           return await Task.Run(() => {
                return Ok(_context.Eventos);
           });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var eventos = _context.Eventos;
            var evento = await eventos.FirstOrDefaultAsync(e => e.EventoId == id);
            
            if(evento == null) return NotFound();

            return Ok(evento);   
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return await Task.Run(() => {

                return Ok("Post Ok");
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            return await Task.Run(() => {
                return Ok($"Put Id {id}");
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Task.Run(() => {
                return Ok($"Delete Id {id}");
            });
        }

        private async Task<IEnumerable<Evento>> ListarEventos()
        {
            return await Task.Run(() => {

                return new Evento[]{
                    new Evento(){
                        EventoId = 1,
                        Tema = "Angular 11 + .NET Core 5",
                        Local = "Belo Horizonte",
                        Lote = "1° Lote",
                        QtdPessoas = 250,
                        DataEvento = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"),
                        ImagemUrl = "img/foto.png"
                    },
                    new Evento{
                        EventoId = 2,
                        Tema = "Angular - Novidades + .NET Core 6",
                        Local = "São Paulo",
                        Lote = "2° Lote",
                        QtdPessoas = 350,
                        DataEvento = DateTime.Now.AddDays(25).ToString("dd/MM/yyyy"),
                        ImagemUrl = "img/foto.png"
                    },
                };
            });
        }
    }
}
