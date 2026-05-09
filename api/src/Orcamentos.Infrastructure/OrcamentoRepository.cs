using Orcamentos.Application;
using Orcamentos.Domain;

namespace Orcamentos.Infrastructure;

public sealed class OrcamentoRepository(AppDbContext dbContext) : IOrcamentoRepository
{
    public async Task<Orcamento> AddAsync(Orcamento budget, CancellationToken cancellationToken)
    {
        dbContext.Orcamentos.Add(budget);
        await dbContext.SaveChangesAsync(cancellationToken);
        return budget;
    }
}
