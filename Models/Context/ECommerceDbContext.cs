using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.User;
using SimpleECommerce.Models.User.Authorization;

namespace SimpleECommerce.Models.Context
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<ProductImageModel> ProductImages { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<RoleModel> Roles { get; set; }

        public DbSet<UserRoleModel> UserRoles { get; set; }

        public DbSet<PermissionModel> Permissions { get; set; }

        public DbSet<RolePermissionModel> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<RoleModel>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<PermissionModel>().Property(e => e.Id).ValueGeneratedNever();
        }
    }
}
