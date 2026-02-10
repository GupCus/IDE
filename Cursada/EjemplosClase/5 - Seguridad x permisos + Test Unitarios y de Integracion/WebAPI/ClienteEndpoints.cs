using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ClienteEndpoints
    {
        public static void MapClienteEndpoints(this WebApplication app)
        {
            app.MapGet("/clientes/{id}", (int id, IClienteService clienteService) =>
            {
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
            .WithOpenApi()
            .RequireAuthorization("ClientesLeer");

            app.MapGet("/clientes", (IClienteService clienteService) =>
            {
                var dtos = clienteService.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllClientes")
            .Produces<List<ClienteDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("ClientesLeer");

            app.MapPost("/clientes", (ClienteDTO dto, IClienteService clienteService) =>
            {
                try
                {
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
            .WithOpenApi()
            .RequireAuthorization("ClientesAgregar");

            app.MapPut("/clientes", (ClienteDTO dto, IClienteService clienteService) =>
            {
                try
                {
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
            .WithOpenApi()
            .RequireAuthorization("ClientesActualizar");

            app.MapDelete("/clientes/{id}", (int id, IClienteService clienteService) =>
            {
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
            .WithOpenApi()
            .RequireAuthorization("ClientesEliminar");

            app.MapGet("/clientes/criteria", (string texto, IClienteService clienteService) =>
            {
                try
                {
                    var criteria = new ClienteCriteriaDTO { Texto = texto };
                    var clientes = clienteService.GetByCriteria(criteria);
                    return Results.Ok(clientes);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetClientesByCriteria")
            .WithOpenApi()
            .RequireAuthorization("ClientesLeer");
        }
    }
}