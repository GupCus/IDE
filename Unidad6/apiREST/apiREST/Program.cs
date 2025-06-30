using System;

namespace apiREST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", () => 
            Results.Redirect("/swagger/index.html"));

            app.Run();
        }
    }
}
