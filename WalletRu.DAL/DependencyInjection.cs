using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using WalletRu.Application.Common.Options;
using WalletRu.Application.Interfaces;
using WalletRu.DAL.Data;
using WalletRu.DAL.Repositories;

namespace WalletRu.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        AddDbInitializer(services);
        AddNpgsqlConnection(services);
        AddRepositories(services);
        AddUnitOfWork(services);
        return services;
    }

    private static void AddDbInitializer(IServiceCollection services)
    {
        services.AddScoped<DbInitializer>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddNpgsqlConnection(IServiceCollection services)
    {
        services.AddScoped<NpgsqlConnection>(sp =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DbOptions>>().Value.ConnectionString;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        });
    }
}