using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PedidoEndpoints
    {
        public static void MapPedidoEndpoints(this WebApplication app)
        {
            app.MapGet("/pedidos/{id}", (int id) =>
            {
                PedidoService pedidoService = new PedidoService();

                PedidoDTO? dto = pedidoService.Get(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPedido")
            .Produces<PedidoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("PedidosLeer");

            app.MapGet("/pedidos", () =>
            {
                PedidoService pedidoService = new PedidoService();

                var dtos = pedidoService.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllPedidos")
            .Produces<List<PedidoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("PedidosLeer");

            app.MapPost("/pedidos", (PedidoDTO dto) =>
            {
                try
                {
                    PedidoService pedidoService = new PedidoService();

                    PedidoDTO pedidoDTO = pedidoService.Add(dto);

                    return Results.Created($"/pedidos/{pedidoDTO.Id}", pedidoDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPedido")
            .Produces<PedidoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("PedidosAgregar");

            app.MapPut("/pedidos", (PedidoDTO dto) =>
            {
                try
                {
                    PedidoService pedidoService = new PedidoService();

                    var found = pedidoService.Update(dto);

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
            .WithName("UpdatePedido")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("PedidosActualizar");

            app.MapDelete("/pedidos/{id}", (int id) =>
            {
                PedidoService pedidoService = new PedidoService();

                var deleted = pedidoService.Delete(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePedido")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("PedidosEliminar");
        }
    }
}