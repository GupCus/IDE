using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PaisEndpoints
    {
        public static void MapPaisEndpoints(this WebApplication app)
        {
            app.MapGet("/paises", async () =>
            {
                PaisService paisService = new PaisService();
                var dtos = await paisService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPaises")
            .Produces<List<PaisDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();
        }
    }
}