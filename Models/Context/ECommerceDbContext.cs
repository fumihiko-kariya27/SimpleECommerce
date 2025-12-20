using Microsoft.EntityFrameworkCore;

namespace SimpleECommerce.Models.Context
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        { 
        
        }
    }
}
