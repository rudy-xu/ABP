using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyProject.EntityFrameworkCore
{
    public static class MyProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyProjectDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySQL(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyProjectDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySQL(connection);
        }
    }
}
