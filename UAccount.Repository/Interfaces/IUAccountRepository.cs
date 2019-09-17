using System.Threading.Tasks;

namespace UAccount.Repository.Interfaces
{
    public interface IUAccountRepository
    {
         //GERAL
         void Add<T>(T Entity) where T : class;
         void Update<T>(T Entity) where T : class;
         void Delete<T>(T Entity) where T : class;

         Task<bool> SaveChangesAsync();

    }
}