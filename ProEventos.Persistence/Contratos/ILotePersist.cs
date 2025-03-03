using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}