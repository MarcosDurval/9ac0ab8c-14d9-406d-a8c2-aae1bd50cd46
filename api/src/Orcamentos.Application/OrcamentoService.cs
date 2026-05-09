using Orcamentos.Domain;

namespace Orcamentos.Application;

public sealed class OrcamentoService(IOrcamentoRepository repository)
{
    public async Task<Orcamento> CreateAsync(
        int clienteId,
        int veiculoId,
        IEnumerable<NewOrcamentoItem> itens,
        CancellationToken cancellationToken = default)
    {
        var items = itens.Select(item =>
            new OrcamentoItem(item.Descricao, item.Quantidade, item.ValorUnitario));

        var budget = new Orcamento(clienteId, veiculoId, items);
        return await repository.AddAsync(budget, cancellationToken);
    }
}

public sealed record NewOrcamentoItem(
    string Descricao,
    int Quantidade,
    decimal ValorUnitario);
