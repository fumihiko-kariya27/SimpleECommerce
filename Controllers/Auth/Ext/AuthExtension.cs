using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Controllers.Auth.Ext
{
    public static class AuthExtension
    {
        public static IServiceCollection AddPermissionPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("ViewProduct", policy =>
                {
                    policy.RequireClaim("Permission", "ViewProduct");
                })
                .AddPolicy("RegisterNewProduct", policy =>
                {
                    policy.RequireClaim("Permission", "RegisterNewProduct");
                })
                .AddPolicy("UpdateProduct", policy =>
                {
                    policy.RequireClaim("Permission", "UpdateProduct");
                })
                .AddPolicy("DeleteProduct", policy =>
                {
                    policy.RequireClaim("Permission", "DeleteProduct");
                })
                .AddPolicy("NewOrder", policy =>
                {
                    policy.RequireClaim("Permission", "NewOrder");
                })
                .AddPolicy("UpdateOrder", policy =>
                {
                    policy.RequireClaim("Permission", "UpdateOrder");
                })
                .AddPolicy("CancelOrder", policy =>
                {
                    policy.RequireClaim("Permission", "CancelOrder");
                });

            return services;
        }

        public static IServiceCollection AddCookeiAythentication(this IServiceCollection services)
        {
            services.AddAuthentication("AuthCookie").AddCookie(
                "AuthCookie", options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                    options.AccessDeniedPath = "/auth/denied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(3);
                }
            );

            return services;
        }

        public static DomainUser CetCurrentCustomer(this HttpContext context)
        {
            if (context.Items.TryGetValue("user", out var value) && value is DomainUser user)
            {
                return user;
            }

            throw new InvalidOperationException("user is not set HttpContext Items");
        }
    }
}
