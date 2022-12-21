using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/evento")]
    public class EventoController : ControllerBase
    {
        public EventoController(){}
       

        [HttpGet("health")]
        public async Task<IActionResult> CheckHealth()
        {
            return await Task.Run(() => {
                return Ok("The Event API is running!!!");
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return Ok(await ListarEventos());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var eventos = await ListarEventos();
            var evento = eventos.Where(e => e.EventoId == id).FirstOrDefault();
            if(evento != null)  return Ok(evento);  

            return NotFound(); 
                  
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
