using System.Threading.Tasks;

namespace Cade.BookStore.Data
{
    public interface IBookStoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
