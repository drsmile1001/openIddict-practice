using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpenIddictPractice.Server.Entities;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIddictPractice.Server
{
    internal class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("console") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "console",
                    ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                    DisplayName = "My client application",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials
                    }
                });
            }

            if (!context.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userAdmin = new ApplicationUser { UserName = "admin" };
                var createAdminResult = await userManager.CreateAsync(userAdmin, "password");
                if (!createAdminResult.Succeeded)
                    Serilog.Log.Warning("建立 Admin 失敗", createAdminResult.Errors);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}