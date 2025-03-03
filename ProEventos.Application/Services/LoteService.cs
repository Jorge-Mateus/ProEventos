using AutoMapper;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contratos;
using ProEventos.Application.DTOs;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Persist;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersit;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersist geralPersist,
                             ILotePersist lotePersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _lotePersit = lotePersist;
            _mapper = mapper;
        }


        public async Task<LoteDto> GetLoteByIdsAsync(int EventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersit.GetLoteByIdsAsync(EventoId, loteId);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersit.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _lotePersit.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);

                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _geralPersist.Update<Lote>(lote);
                    }

                }

                var loteRetorno = await _lotePersit.GetLotesByEventoIdAsync(eventoId);

                return _mapper.Map<LoteDto[]>(loteRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar evento: " + ex.Message);
            }
        }
        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersit.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não foi encontrado.");

                _geralPersist.Delete<Lote>(lote);

                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geralPersist.Add<Lote>(lote);

                await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar evento: " + ex.Message);
            }
        }
    }
}
