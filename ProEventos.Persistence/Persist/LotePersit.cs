using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Persist
{
    public class LotePersit : ILotePersist
    {
        private readonly ProEventosContext _context;

        public LotePersit(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoId && lote.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}
