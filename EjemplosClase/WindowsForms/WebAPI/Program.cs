using Application.Services;
using Domain.Model;
using DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //Falta configurar de manera correcta        
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

    


app.MapGet("/clientes/{id}", (int id) =>
{
    ClienteService clienteService = new ClienteService();

    ClienteDTO dto = clienteService.Get(id);

    if (dto == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(dto);
})
.WithName("GetCliente")
.Produces<ClienteDTO>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/clientes", () =>
{
    ClienteService clienteService = new ClienteService();

    var dtos = clienteService.GetAll();

    return Results.Ok(dtos);
})
.WithName("GetAllClientes")
.Produces<List<ClienteDTO>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapPost("/clientes", (ClienteDTO dto) =>
{
    try
    {
        ClienteService clienteService = new ClienteService();

        ClienteDTO clienteDTO = clienteService.Add(dto);

        return Results.Created($"/clientes/{clienteDTO.Id}", clienteDTO);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddCliente")
.Produces<ClienteDTO>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapPut("/clientes", (ClienteDTO dto) =>
{
    try
    {
        ClienteService clienteService = new ClienteService();

        var found = clienteService.Update(dto);

        if (!found)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateCliente")
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapDelete("/clientes/{id}", (int id) =>
{
    ClienteService clienteService = new ClienteService();

    var deleted = clienteService.Delete(id);

    if (!deleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
        
})
.WithName("DeleteCliente")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.Run();
