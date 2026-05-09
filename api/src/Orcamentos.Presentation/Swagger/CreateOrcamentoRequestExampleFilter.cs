using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Orcamentos.Presentation.Dtos;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orcamentos.Presentation.Swagger;

public sealed class CreateOrcamentoRequestExampleFilter : IRequestBodyFilter
{
    public void Apply(IOpenApiRequestBody requestBody, RequestBodyFilterContext context)
    {
        if (context.BodyParameterDescription?.Type != typeof(CreateOrcamentoRequest))
        {
            return;
        }

        if (requestBody.Content is null ||
            !requestBody.Content.TryGetValue("application/json", out var mediaType))
        {
            return;
        }

        mediaType.Example = JsonNode.Parse(
            """
            {
              "clienteId": 10,
              "veiculoId": 25,
              "itens": [
                {
                  "descricao": "Troca de óleo",
                  "quantidade": 1,
                  "valorUnitario": 120.00
                },
                {
                  "descricao": "Filtro de óleo",
                  "quantidade": 1,
                  "valorUnitario": 45.00
                }
              ]
            }
            """);
    }
}
