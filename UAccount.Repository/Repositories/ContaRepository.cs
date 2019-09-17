using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UAccount.Domain.Identity;
using UAccount.Domain.Models;
using UAccount.Repository.Data;
using UAccount.Repository.Interfaces;

namespace UAccount.Repository.Repository
{
    public class ContaRepository : IContaRepository
    {
        private readonly UAccountContext _context;

        public ContaRepository(UAccountContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
     
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<Conta> ObterPorId(int ContaId)
        {
            IQueryable<Conta> query = _context.Contas
                    .AsNoTracking()
                    .OrderByDescending(c => c.Vencimento)
                    .Where(c => c.Id == ContaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Conta[]> ObterTodosPorTituloAsync(string Titulo)
        {
             IQueryable<Conta> query = _context.Contas
                        .AsNoTracking()
                        .OrderByDescending(c => c.Vencimento)
                        .Where(c => c.Titulo.ToLower().Contains(Titulo.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<User> ObterPorUsuario(int UsuarioId)
        {
            IQueryable<User> query = _context.Users
                                .Include(c => c.Contas);

            query = query
                    .AsNoTracking()
                    .Where(c => c.Id == UsuarioId);

            return await query.FirstOrDefaultAsync();
        }
    }
}