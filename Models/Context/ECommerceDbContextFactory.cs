using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleECommerce.Models.Context
{
    // DbContextが生成できなくなったエラーに対応する一時クラス
    public class ECommerceDbContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();
            optionsBuilder.UseSqlServer("ApplicationDbContext");

            return new ECommerceDbContext(optionsBuilder.Options);
        }
    }

}
