using Volo.Abp.Modularity;

namespace Cade.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationModule),
        typeof(BookStoreDomainTestModule)
        )]
    public class BookStoreApplicationTestModule : AbpModule
    {

    }
}