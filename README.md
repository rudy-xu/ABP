# ABP
## DDD(Domain-Driven design)
* 展现层(Presentation layer)
* 应用层(Application layer)
* 领域层(Domain layer)
* 基础设施层(Infrastructure layer)

## Program structure
* xxx.Application
* xxx.Application.Contracts
* Migration:
    * xxx.DbMigrator: 运行此程序，其会创建数据库和表格并植入`user`和`password`数据(即`admin`和`1q2w3E*`)
    * xxx.EntityFrameworkCore.DbMigrations：使用`update-database`命令，在需要的时候创建空的数据库和表格并应用在待处理的`migrations`  
    **<font color="red">建议使用`.DbMigrator`工具，当不需要seed the database可以使用`update-database`，但是使用`.DbMigrator`可以迁移`schema`并seed the database</font>**
* Domain layer(领域层)：
    * xxx.Domain：包含实体(entities)，领域服务(domain services)，其他核心领域对象
    * xxx.Domain.Shared：包含一些常量(constants)，枚举(enums)，领域其他相关的公共对象
* xxx.EntityFrameworkCore
* xxx.HttpApi
* xxx.HttpApi.Client
* Presentation(展现层):
    * `xxx.Web`
        * `https://localhost:<port>`: 页面
        * `https://localhost:<port>/swagger/`: Swagger has a nice interface to test the APIs.

## Others
* `Guid`也称为`Uuid`用于表格的一个主键，有时也专指微软对`uuid`标准的实现，是一种由算法生成的二进制长度为128位的数字标识符。在理想情况下，任何计算机之间都不会生成两个相同的GUID。GUID的优点允许开发人员随时创建新值，而无需从数据库服务器检查值的唯一性。
* **<font color="red">问题</font>**：很多数据库在创建主键时，为了充分发挥数据库的性能，会自动在该列上创建聚集索引。聚集索引确定表中数据的物理顺序，类似于电话簿，按姓氏排列数据。由于聚集索引规定数据在表中的物理存储顺序，因此一个表也只能包含一个聚集索引。它能够快速查找到数据，但是如果插入数据库的主键不在列表的末尾，向表中添加新行时就非常缓慢。例如：

    |ID|Name|
    |--|----|
    |1|Tom|
    |3|Cade|
    |6|Jack|
**在上表中插入一个`ID为7`的数据，非常容易，直接查到末尾即可，但是如果插入`ID为4`的数据，就得先将最后一行的数据下移，再进行插入。又因为`GUID`时随机数据，所以插入数据时会随时涉及到数据的移动问题，从而导致插入速度会减慢，因此出现一种有规则的GUID生成方式，即有序GUID，保证每次GUID都比上一数据大**


## ABP Framework Related
* xxxx.Domain &rarr; 定义`Book`实体
* xxxx.Application.Contract
    * 定义一个`DTO`映射实体到应用
    * 定义`IBookAppService`继承`ICrudAppService`包含增删改查方法
* xxxx.Application
    * xxxxApplicationAutoMapperProfile &rarr; 实现DTO映射
    * 定义类`BookAppService`实现上述两个接口
    ```cs
    public class BookAppService : CrudAppService<Book, ReadBookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, IBookAppService 
    {
        //injects IRepository<Book, Guid> which is the default repository for the Book entity
        //ABP automatically creates default repositories for each aggregate root (or entity)
        public BookAppService(IRepository<Book, Guid> repository): base(repository)
        {

        }
    }
    ```
## ABP Test
* `Xunit` as the main test framework.
    * using Xunit;
    * [Fact]
        ```cs
        //test _bookAppService GetList
        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            //Action
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984");
        }
        ```
    * command
        ```sh
        dotnet test ProjectName
        ```
* `Shoudly` as the assertion library.
    * using Shouldly;
    * ShouldBeGreaterThan(num);
    * ShouldContain(xxx);
    * ShouldNotBe(xxx);

* `NSubstitute` as the mocking library.

## ABP Permisssion
* `ABP Framework`提供了基于`ASP.NET Core`权限基础框架的`权限系统`。
* xxxx.Application.Contract
    * BookStorePermissions //定义许可证名称
        ```cs
        public static class BookStorePermissions
        {
            public const string GroupName = "BookStore";

            public static class Books
            {
                public const string Default = GroupName + ".Books";
                //...
            }
        }
        ```
    * BookStorePermissionDefinitionProvider 定义许可证
    * xxx.Application 
        * BookAppService //利用许可证进行授权
    * xxx.Web
        * BookStoreWebModule &rarr; ConfigureServices //没有权限的情况下访问页面进行重定向
            ```cs
            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Books/Index", BookStorePermissions.Books.Default);
            });
            ```
    * 利用`AuthorizationService.IsGrantedAsync(BookStorePermissions.Books.xxxxx)`进行权限判断，从而进行一些权限范围的划分