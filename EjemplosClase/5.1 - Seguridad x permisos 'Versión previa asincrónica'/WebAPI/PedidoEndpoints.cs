using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PedidoEndpoints
    {
        public static void MapPedidoEndpoints(this WebApplication app)
        {
            app.MapGet("/pedidos/{id}", async (int id) =>
            {
                PedidoService pedidoService = new PedidoService();

                PedidoDTO? dto = await pedidoService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPedido")
            .Produces<PedidoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/pedidos", async () =>
            {
                PedidoService pedidoService = new PedidoService();

                var dtos = await pedidoService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllPedidos")
            .Produces<List<PedidoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/pedidos", async (PedidoDTO dto) =>
            {
                try
                {
                    PedidoService pedidoService = new PedidoService();

                    PedidoDTO pedidoDTO = await pedidoService.AddAsync(dto);

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
            .WithOpenApi();

            app.MapPut("/pedidos", async (PedidoDTO dto) =>
            {
                try
                {
                    PedidoService pedidoService = new PedidoService();

                    var found = await pedidoService.UpdateAsync(dto);

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
            .WithOpenApi();

            app.MapDelete("/pedidos/{id}", async (int id) =>
            {
                PedidoService pedidoService = new PedidoService();

                var deleted = await pedidoService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePedido")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}