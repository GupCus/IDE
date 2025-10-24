using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAPI;
using Application.Services;
using Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

// Add Entity Framework Context
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Dependency Injection
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ClockSkew = TimeSpan.Zero
        };
    });

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Políticas para Países
    options.AddPolicy("PaisesLeer", policy => policy.RequireClaim("permission", "paises.leer"));
    options.AddPolicy("PaisesAgregar", policy => policy.RequireClaim("permission", "paises.agregar"));
    options.AddPolicy("PaisesActualizar", policy => policy.RequireClaim("permission", "paises.actualizar"));
    options.AddPolicy("PaisesEliminar", policy => policy.RequireClaim("permission", "paises.eliminar"));

    // Políticas para Clientes
    options.AddPolicy("ClientesLeer", policy => policy.RequireClaim("permission", "clientes.leer"));
    options.AddPolicy("ClientesAgregar", policy => policy.RequireClaim("permission", "clientes.agregar"));
    options.AddPolicy("ClientesActualizar", policy => policy.RequireClaim("permission", "clientes.actualizar"));
    options.AddPolicy("ClientesEliminar", policy => policy.RequireClaim("permission", "clientes.eliminar"));

    // Políticas para Pedidos
    options.AddPolicy("PedidosLeer", policy => policy.RequireClaim("permission", "pedidos.leer"));
    options.AddPolicy("PedidosAgregar", policy => policy.RequireClaim("permission", "pedidos.agregar"));
    options.AddPolicy("PedidosActualizar", policy => policy.RequireClaim("permission", "pedidos.actualizar"));
    options.AddPolicy("PedidosEliminar", policy => policy.RequireClaim("permission", "pedidos.eliminar"));

    // Políticas para Usuarios
    options.AddPolicy("UsuariosLeer", policy => policy.RequireClaim("permission", "usuarios.leer"));
    options.AddPolicy("UsuariosAgregar", policy => policy.RequireClaim("permission", "usuarios.agregar"));
    options.AddPolicy("UsuariosActualizar", policy => policy.RequireClaim("permission", "usuarios.actualizar"));
    options.AddPolicy("UsuariosEliminar", policy => policy.RequireClaim("permission", "usuarios.eliminar"));

    // Políticas para Productos
    options.AddPolicy("ProductosLeer", policy => policy.RequireClaim("permission", "productos.leer"));

    // Fallback: Requerir autenticación para endpoints no especificados
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add CORS for Blazor WebAssembly + React Native
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy =>
        {
            policy.AllowAnyOrigin() // TODO: En producción especificar orígenes exactos por seguridad
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowBlazorWasm");

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAuthEndpoints(); // Auth endpoints (no requieren autenticación)
app.MapUsuarioEndpoints();
app.MapClienteEndpoints();
app.MapPaisEndpoints(); 
app.MapProductoEndpoints();
app.MapPedidoEndpoints();

app.Run();

// Make Program class accessible for integration tests
public partial class Program { }
