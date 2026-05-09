using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Orcamentos.Infrastructure;
using Orcamentos.Presentation.Dtos;

namespace Orcamentos.Tests;

public sealed class OrcamentosEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OrcamentosEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddInfrastructure($"OrcamentosTests-{Guid.NewGuid()}");
            });
        });
    }

    [Fact]
    public async Task Creates_valid_budget_with_201_created()
    {
        var client = _factory.CreateClient();
        var request = new CreateOrcamentoRequest(
            10,
            25,
            [
                new CreateOrcamentoItemRequest("Troca de óleo", 1, 120m),
                new CreateOrcamentoItemRequest("Filtro de óleo", 1, 45m)
            ]);

        var response = await client.PostAsJsonAsync("/orcamentos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<OrcamentoResponse>();

        Assert.NotNull(body);
        Assert.True(body.Id > 0);
        Assert.Equal(10, body.ClienteId);
        Assert.Equal(25, body.VeiculoId);
        Assert.Equal(165m, body.Total);
        Assert.Collection(
            body.Itens,
            item => Assert.Equal(120m, item.Subtotal),
            item => Assert.Equal(45m, item.Subtotal));
    }

    public static TheoryData<string, string> InvalidRequests => new()
    {
        {
            """
            {
              "clienteId": 0,
              "veiculoId": 25,
              "itens": [{ "descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00 }]
            }
            """,
            "ClienteId"
        },
        {
            """
            {
              "clienteId": 10,
              "veiculoId": 0,
              "itens": [{ "descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00 }]
            }
            """,
            "VeiculoId"
        },
        {
            """
            {
              "clienteId": 10,
              "veiculoId": 25,
              "itens": []
            }
            """,
            "Itens"
        },
        {
            """
            {
              "clienteId": 10,
              "veiculoId": 25,
              "itens": [{ "descricao": "", "quantidade": 1, "valorUnitario": 120.00 }]
            }
            """,
            "Descricao"
        },
        {
            """
            {
              "clienteId": 10,
              "veiculoId": 25,
              "itens": [{ "descricao": "Troca de óleo", "quantidade": 0, "valorUnitario": 120.00 }]
            }
            """,
            "Quantidade"
        },
        {
            """
            {
              "clienteId": 10,
              "veiculoId": 25,
              "itens": [{ "descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 0 }]
            }
            """,
            "ValorUnitario"
        }
    };

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public async Task Rejects_invalid_request_with_400_bad_request(string json, string field)
    {
        var client = _factory.CreateClient();
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/orcamentos", content);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(field, body);
    }

    [Fact]
    public async Task Rejects_malformed_json_with_400_bad_request()
    {
        var client = _factory.CreateClient();
        using var content = new StringContent(
            """
            {
              "clienteId": x,
              "veiculoId": 25,
              "itens": []
            }
            """,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync("/orcamentos", content);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Failed to read parameter", body);
    }
}
