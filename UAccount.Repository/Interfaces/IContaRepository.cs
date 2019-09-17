using System.Threading.Tasks;
using UAccount.Domain.Identity;
using UAccount.Domain.Models;

namespace UAccount.Repository.Interfaces
{
    public interface IContaRepository : IUAccountRepository
    {
        Task<Conta> ObterPorId(int ContaId);

        Task<User> ObterPorUsuario(int UsuarioId);

        Task<Conta[]> ObterTodosPorTituloAsync(string Titulo);

    }
}