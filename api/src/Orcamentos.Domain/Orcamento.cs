namespace Orcamentos.Domain;

public sealed class Orcamento
{
    private readonly List<OrcamentoItem> _itens = [];

    private Orcamento()
    {
    }

    public Orcamento(int clienteId, int veiculoId, IEnumerable<OrcamentoItem> itens)
    {
        SetClienteId(clienteId);
        SetVeiculoId(veiculoId);
        SetItens(itens);
    }

    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public int VeiculoId { get; private set; }
    public IReadOnlyCollection<OrcamentoItem> Itens => _itens.AsReadOnly();
    public decimal Total => _itens.Sum(item => item.Subtotal);

    private void SetClienteId(int clienteId)
    {
        if (clienteId <= 0)
        {
            throw new DomainException("O cliente informado é inválido.");
        }

        ClienteId = clienteId;
    }

    private void SetVeiculoId(int veiculoId)
    {
        if (veiculoId <= 0)
        {
            throw new DomainException("O veículo informado é inválido.");
        }

        VeiculoId = veiculoId;
    }

    private void SetItens(IEnumerable<OrcamentoItem> itens)
    {
        var validItems = itens?.ToList() ?? [];
        if (validItems.Count == 0)
        {
            throw new DomainException("O orçamento deve possuir pelo menos um item.");
        }

        _itens.Clear();
        _itens.AddRange(validItems);
    }
}
