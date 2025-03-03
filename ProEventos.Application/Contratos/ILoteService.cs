using ProEventos.Application.DTOs;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface ILoteService
    {
        Task<LoteDto[]>SaveLote(int eventoId, LoteDto[] models);
        Task<bool>DeleteLote(int eventoId, int loteId);
        Task<LoteDto[]>GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto>GetLoteByIdsAsync(int EventoId, int loteId);

    }
}
