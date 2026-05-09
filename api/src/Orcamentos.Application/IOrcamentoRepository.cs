using Orcamentos.Domain;

namespace Orcamentos.Application;

public interface IOrcamentoRepository
{
    Task<Orcamento> AddAsync(Orcamento budget, CancellationToken cancellationToken);
}
