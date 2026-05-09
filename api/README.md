# API de Orçamentos

API .NET 10 para cadastro de orçamentos de oficina mecânica com DDD em camadas, Minimal API, EF Core InMemory e Swagger.

## Executar

```bash
dotnet restore
dotnet run --project src/OrcamentosApi.csproj
```

Swagger:

```text
http://localhost:5000/swagger
```

## Executar com Docker Compose

Na raiz do repositório:

```bash
docker compose up --build
```

Swagger:

```text
http://localhost:5002/swagger
```

Para parar:

```bash
docker compose down --rmi all --remove-orphans
```

## Testar

```bash
dotnet build
dotnet test
```

## Endpoint

`POST /orcamentos`

Request:

```json
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
```

Response `201 Created`:

```json
{
  "id": 1,
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00,
      "subtotal": 120.00
    },
    {
      "descricao": "Filtro de óleo",
      "quantidade": 1,
      "valorUnitario": 45.00,
      "subtotal": 45.00
    }
  ],
  "total": 165.00
}
```

Erros de validação retornam `400 Bad Request` com mensagens por campo.

## Uso de IA

Foi utilizada IA para acelerar o desenvolvimento desta API.
