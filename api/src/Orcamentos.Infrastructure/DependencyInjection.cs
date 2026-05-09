using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orcamentos.Application;

namespace Orcamentos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName));
        services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();

        return services;
    }
}
