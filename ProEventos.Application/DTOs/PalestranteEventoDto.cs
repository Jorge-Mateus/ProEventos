using ProEventos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOs
{
    public class PalestranteEventoDto
    {
        public int PalestranteId { get; set; }
        public PalestranteDto Palestrante { get; set; }
        public int EventoId { get; set; }
        public EventoDto Evento {get; set; }
    }
}
