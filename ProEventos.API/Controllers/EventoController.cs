using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[]
        {
            new Evento()
            {
                EventoId = 1,
                Tema = "Angular 11 e .Net 5",
                Lote = "1° Lote",
                Local = "Belo Horizonte",
                QtdPessoas = 100,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemURL = "foto.png"
            },
            new Evento()
            {
                EventoId = 2,
                Tema = "Angular e suas novidades",
                Lote = "1° Lote",
                Local = "Belo Horizonte",
                QtdPessoas = 100,
                DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
                ImagemURL = "foto.png"

            }
        };


        public EventoController()
        {

        }


        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }


        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }
        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return "Exemplo de Put";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "Exemplo de Delete";
        }

    }
}
