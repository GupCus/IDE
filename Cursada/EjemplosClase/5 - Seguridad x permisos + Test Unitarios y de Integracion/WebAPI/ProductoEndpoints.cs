using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ProductoEndpoints
    {
        public static void MapProductoEndpoints(this WebApplication app)
        {
            app.MapGet("/productos/{id}", (int id) =>
            {
                ProductoService productoService = new ProductoService();

                ProductoDTO? dto = productoService.Get(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetProducto")
            .Produces<ProductoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("ProductosLeer");

            app.MapGet("/productos", () =>
            {
                ProductoService productoService = new ProductoService();

                var dtos = productoService.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllProductos")
            .Produces<List<ProductoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("ProductosLeer");
        }
    }
}