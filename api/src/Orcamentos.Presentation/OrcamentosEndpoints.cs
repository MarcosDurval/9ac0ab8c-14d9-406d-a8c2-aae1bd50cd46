using Orcamentos.Application;
using Orcamentos.Presentation.Dtos;

namespace Orcamentos.Presentation;

public static class OrcamentosEndpoints
{
    public static IEndpointRouteBuilder MapOrcamentosEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/orcamentos", CreateOrcamento)
            .WithName("CreateOrcamento")
            .Produces<OrcamentoResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> CreateOrcamento(
        CreateOrcamentoRequest request,
        OrcamentoService service,
        CancellationToken cancellationToken)
    {
        var budget = await service.CreateAsync(
            request.ClienteId,
            request.VeiculoId,
            request.GetItems(),
            cancellationToken);

        return Results.Created($"/orcamentos/{budget.Id}", OrcamentoResponse.FromDomain(budget));
    }
}
