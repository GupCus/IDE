using Dominio;
using System;
using System.Reflection;

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
            Results.Redirect("/swagger/"));

            app.MapGet("/alumnos/", () =>
            Results.Ok(Alumno.Lista));

            app.MapGet("/alumnos/{id}", (int id) =>
            Alumno.Lista.Find(a => a.Id == id) is Alumno alumno
            ? Results.Ok(alumno)
            : Results.NotFound());

            app.MapPost("/alumnos", (Alumno alumno) =>
            {
                alumno.Id = Alumno.ObtenerProximoId();
                Alumno.Lista.Add(alumno);
                return Results.Created($"/alumnos/{alumno.Id}", alumno);
            });

            app.MapPut("/alumnos/{id}", (int id, Alumno alumno) =>
            {
                var index = Alumno.Lista.FindIndex(a => a.Id == id);
                if (index == -1)
                {
                    return Results.NotFound();
                }
                else
                {
                    alumno.Id = id;
                    Alumno.Lista[index] = alumno;
                    return Results.NoContent();
                }
            });

            app.MapDelete("/alumnos/{id}", (int id) =>
            {
                if (Alumno.Lista.Find(a => a.Id == id) is Alumno alumno)
                {
                    Alumno.Lista.Remove(alumno);
                    return Results.NoContent();
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.Run();
        }
    }
}
