using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using SimpleECommerce.Models.User.Authorization;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace SimpleECommerce.InfraStructure
{
    public static class DbInitializer
    {
        public static void Seed(ECommerceDbContext context)
        {
            context.Database.Migrate();

            CategorySeed(context);
            ProductSeed(context);
            PermissionSeed(context);
            RoleSeed(context);
            RolePermissionSeed(context);
            UserSeed(context);
            UserRoleSeed(context);
        }

        private static void CategorySeed(ECommerceDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/category.json", Encoding.UTF8);
            List<CategoryModel> categories = JsonSerializer.Deserialize<List<CategoryModel>>(json)!;
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ProductSeed(ECommerceDbContext context)
        {
            if (context.Products.Any()) 
            {
                return;
            }

            string json = File.ReadAllText("seed/product.json", Encoding.UTF8);
            List<ProductModel> products = JsonSerializer.Deserialize<List<ProductModel>>(json)!;
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void PermissionSeed(ECommerceDbContext context)
        {
            if (context.Permissions.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/permission.json", Encoding.UTF8);
            List<PermissionModel> permissions = JsonSerializer.Deserialize<List<PermissionModel>>(json)!;
            context.Permissions.AddRange(permissions);
            context.SaveChanges();
        }

        private static void RoleSeed(ECommerceDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/role.json", Encoding.UTF8);
            List<RoleModel> roles = JsonSerializer.Deserialize<List<RoleModel>>(json)!;
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private static void RolePermissionSeed(ECommerceDbContext context)
        {
            if (context.RolePermissions.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/rolePermission.json", Encoding.UTF8);
            List<RolePermissionModel> rolePermission = JsonSerializer.Deserialize<List<RolePermissionModel>>(json)!;
            context.RolePermissions.AddRange(rolePermission);
            context.SaveChanges();
        }

        private static void UserSeed(ECommerceDbContext context)
        {
            if (context.Users.Any()) 
            {
                return;
            }

            string json = File.ReadAllText("seed/user.json", Encoding.UTF8);
            List<UserModel> users = JsonSerializer.Deserialize<List<UserModel>>(json)!;

            PasswordHasher<UserModel> hasher = new PasswordHasher<UserModel>();
            foreach (var user in users)
            { 
                string plainPassword = user.Password;
                user.Password = hasher.HashPassword(user, plainPassword);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void UserRoleSeed(ECommerceDbContext context)
        {
            if (context.UserRoles.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/userRole.json", Encoding.UTF8);
            List<UserRoleModel> userRole = JsonSerializer.Deserialize<List<UserRoleModel>>(json)!;
            context.UserRoles.AddRange(userRole);
            context.SaveChanges();
        }
    }
}
