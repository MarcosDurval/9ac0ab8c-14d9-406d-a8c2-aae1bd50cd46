using Orcamentos.Domain;

namespace Orcamentos.Presentation.Dtos;

public sealed record OrcamentoResponse(
    int Id,
    int ClienteId,
    int VeiculoId,
    List<OrcamentoItemResponse> Itens,
    decimal Total)
{
    public static OrcamentoResponse FromDomain(Orcamento budget) =>
        new(
            budget.Id,
            budget.ClienteId,
            budget.VeiculoId,
            budget.Itens.Select(OrcamentoItemResponse.FromDomain).ToList(),
            budget.Total);
}

public sealed record OrcamentoItemResponse(
    string Descricao,
    int Quantidade,
    decimal ValorUnitario,
    decimal Subtotal)
{
    public static OrcamentoItemResponse FromDomain(OrcamentoItem item) =>
        new(item.Descricao, item.Quantidade, item.ValorUnitario, item.Subtotal);
}
