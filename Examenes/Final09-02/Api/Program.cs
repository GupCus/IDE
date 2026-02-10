namespace Barroso.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.Run();
        }
    }
}
