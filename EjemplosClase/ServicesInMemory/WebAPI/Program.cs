using Domain.Services;
using Domain.Model;

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

    Cliente cliente = clienteService.Get(id);

    if (cliente == null)
    {
        return Results.NotFound();
    }

    var dto = new DTOs.Cliente
    {
        Id = cliente.Id,
        Nombre = cliente.Nombre,
        Apellido = cliente.Apellido,
        Email = cliente.Email,
        FechaAlta = cliente.FechaAlta
    };

    return Results.Ok(dto);
})
.WithName("GetCliente")
.Produces<DTOs.Cliente>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/clientes", () =>
{
    ClienteService clienteService = new ClienteService();

    var clientes = clienteService.GetAll();

    var dtos = clientes.Select(cliente => new DTOs.Cliente
    {
        Id = cliente.Id,
        Nombre = cliente.Nombre,
        Apellido = cliente.Apellido,
        Email = cliente.Email,
        FechaAlta = cliente.FechaAlta
    }).ToList();

    return Results.Ok(dtos);
})
.WithName("GetAllClientes")
.Produces<List<DTOs.Cliente>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapPost("/clientes", (DTOs.Cliente dto) =>
{
    try
    {
        ClienteService clienteService = new ClienteService();

        Cliente cliente = new Cliente(dto.Id, dto.Nombre, dto.Apellido, dto.Email, dto.FechaAlta);

        clienteService.Add(cliente);

        var dtoResultado = new DTOs.Cliente
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Email = cliente.Email,
            FechaAlta = cliente.FechaAlta
        };

        return Results.Created($"/clientes/{dtoResultado.Id}", dtoResultado);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddCliente")
.Produces<DTOs.Cliente>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapPut("/clientes", (DTOs.Cliente dto) =>
{
    try
    {
        ClienteService clienteService = new ClienteService();

        Cliente cliente = new Cliente(dto.Id, dto.Nombre, dto.Apellido, dto.Email, dto.FechaAlta);

        var found = clienteService.Update(cliente);

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
