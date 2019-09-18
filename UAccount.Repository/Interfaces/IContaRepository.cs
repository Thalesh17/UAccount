using System.Threading.Tasks;
using UAccount.Domain.Identity;
using UAccount.Domain.Models;

namespace UAccount.Repository.Interfaces
{
    public interface IContaRepository : IUAccountRepository
    {
        Task<Conta> ObterPorId(int ContaId);
        
        Task<Conta[]> ObterTodosPorTituloAsync(string Titulo);
        Task<Conta[]> ObterPorUsuario(int UsuarioId);
    }
}