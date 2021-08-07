using Cade.BookStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Cade.BookStore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreTestModule)
        )]
    public class BookStoreDomainTestModule : AbpModule
    {

    }
}