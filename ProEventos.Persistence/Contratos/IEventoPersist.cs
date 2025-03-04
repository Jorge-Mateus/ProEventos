using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes );
        Task<Evento> GetEventoByIdAsync(int userId, int EventoId, bool includePalestrantes);
    }
}