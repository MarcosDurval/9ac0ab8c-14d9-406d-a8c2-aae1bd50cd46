namespace Orcamentos.Domain;

public sealed class OrcamentoItem
{
    private OrcamentoItem()
    {
        Descricao = string.Empty;
    }

    public OrcamentoItem(string descricao, int quantidade, decimal valorUnitario)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new DomainException("A descrição do item deve ser preenchida.");
        }

        if (quantidade <= 0)
        {
            throw new DomainException("A quantidade do item deve ser maior que zero.");
        }

        if (valorUnitario <= 0)
        {
            throw new DomainException("O valor unitário do item deve ser maior que zero.");
        }

        Descricao = descricao.Trim();
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal Subtotal => Quantidade * ValorUnitario;
}
