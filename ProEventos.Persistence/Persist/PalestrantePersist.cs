using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Persist
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking().OrderBy(pe => pe.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
              .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.User.PrimeiroNome.ToLower().Contains(nome.ToLower()) || p.User.UltimoNome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos)
        {

            IQueryable<Palestrante> query = _context.Palestrantes
              .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
