using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<PageList<Evento>> GetAllEventosAsync(int userId,PageParams pageParams, bool includePalestrantes );
        Task<Evento> GetEventoByIdAsync(int userId, int EventoId, bool includePalestrantes);
    }
}