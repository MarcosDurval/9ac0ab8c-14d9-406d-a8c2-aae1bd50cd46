using Orcamentos.Application;
using Orcamentos.Domain;
using Orcamentos.Infrastructure;
using Orcamentos.Presentation;
using Orcamentos.Presentation.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.RequestBodyFilter<CreateOrcamentoRequestExampleFilter>();
});
builder.Services.AddScoped<OrcamentoService>();
builder.Services.AddInfrastructure("OrcamentosDb");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (BadHttpRequestException exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new { erro = exception.Message });
    }
    catch (DomainException exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new { erro = exception.Message });
    }
});

app.MapOrcamentosEndpoints();

app.Run();

public partial class Program;
