using AutoMapper;
using ProEventos.Application.DTOs;
using ProEventos.Domain.Models;

namespace ProEventos.Application.helpers
{
    public class ProEventosProfile : Profile 
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();

        }
    }
}
