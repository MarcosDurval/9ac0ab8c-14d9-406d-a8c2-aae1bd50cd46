using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orcamentos.Application;

namespace Orcamentos.Presentation.Dtos;

public sealed record CreateOrcamentoRequest(
    [property: Range(1, int.MaxValue)]
    [property: DefaultValue(10)]
    int ClienteId,

    [property: Range(1, int.MaxValue)]
    [property: DefaultValue(25)]
    int VeiculoId,

    [property: Required]
    [property: MinLength(1)]
    List<CreateOrcamentoItemRequest> Itens)
{
    public IEnumerable<NewOrcamentoItem> GetItems() =>
        Itens.Select(item => new NewOrcamentoItem(
            item.Descricao,
            item.Quantidade,
            item.ValorUnitario));
}

public sealed record CreateOrcamentoItemRequest(
    [property: Required]
    [property: MinLength(1)]
    [property: DefaultValue("Troca de óleo")]
    string Descricao,

    [property: Range(1, int.MaxValue)]
    [property: DefaultValue(1)]
    int Quantidade,

    [property: Range(
        typeof(decimal),
        "0.01",
        "79228162514264337593543950335",
        ParseLimitsInInvariantCulture = true)]
    [property: DefaultValue(typeof(decimal), "120.00")]
    decimal ValorUnitario);
