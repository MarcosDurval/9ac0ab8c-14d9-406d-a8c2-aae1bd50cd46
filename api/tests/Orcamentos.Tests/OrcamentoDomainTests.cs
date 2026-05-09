using Orcamentos.Domain;

namespace Orcamentos.Tests;

public sealed class OrcamentoDomainTests
{
    [Fact]
    public void Calculates_total_correctly()
    {
        var budget = new Orcamento(
            clienteId: 10,
            veiculoId: 25,
            [
                new OrcamentoItem("Troca de óleo", 1, 120m),
                new OrcamentoItem("Filtro de óleo", 1, 45m)
            ]);

        Assert.Equal(165m, budget.Total);
    }

    [Fact]
    public void Rejects_invalid_clienteId()
    {
        var exception = Assert.Throws<DomainException>(() =>
            new Orcamento(0, 25, [new OrcamentoItem("Troca de óleo", 1, 120m)]));

        Assert.Contains("cliente", exception.Message);
    }

    [Fact]
    public void Rejects_invalid_veiculoId()
    {
        var exception = Assert.Throws<DomainException>(() =>
            new Orcamento(10, 0, [new OrcamentoItem("Troca de óleo", 1, 120m)]));

        Assert.Contains("veículo", exception.Message);
    }

    [Fact]
    public void Rejects_budget_without_items()
    {
        var exception = Assert.Throws<DomainException>(() => new Orcamento(10, 25, []));

        Assert.Contains("pelo menos um item", exception.Message);
    }

    [Fact]
    public void Rejects_item_without_descricao()
    {
        var exception = Assert.Throws<DomainException>(() => new OrcamentoItem("", 1, 120m));

        Assert.Contains("descrição", exception.Message);
    }

    [Fact]
    public void Rejects_quantidade_less_than_or_equal_to_zero()
    {
        var exception = Assert.Throws<DomainException>(() => new OrcamentoItem("Troca de óleo", 0, 120m));

        Assert.Contains("quantidade", exception.Message);
    }

    [Fact]
    public void Rejects_valorUnitario_less_than_or_equal_to_zero()
    {
        var exception = Assert.Throws<DomainException>(() => new OrcamentoItem("Troca de óleo", 1, 0m));

        Assert.Contains("valor unitário", exception.Message);
    }
}
