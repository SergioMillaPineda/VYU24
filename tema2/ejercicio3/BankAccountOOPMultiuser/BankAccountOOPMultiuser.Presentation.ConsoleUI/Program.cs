using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Impl;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountOOPMultiuser.Presentation.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<LoginMenu>()
                .AddSingleton<MainMenu>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<ICredentialsService, CredentialsService>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IMovementsRepository, MovementsRepository>()
                .BuildServiceProvider();

            LoginMenu? loginMenu = serviceProvider.GetService<LoginMenu>();
            loginMenu?.Execute();
        }
    }
}
