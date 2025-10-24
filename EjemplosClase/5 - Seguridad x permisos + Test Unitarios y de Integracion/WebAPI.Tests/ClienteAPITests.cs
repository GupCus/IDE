using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using Data;
using DTOs;
using Domain.Model;

namespace WebAPI.Tests
{
    public class ClienteAPITests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly HttpClient client;

        public ClienteAPITests(WebApplicationFactory<Program> factory)
        {
            // Usar un nombre fijo para la BD para compartir entre requests
            var dbName = "ClienteAPITestsDb";
            
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("ConnectionStrings:DefaultConnection", "Server=InMemory");
                
                builder.ConfigureServices(services =>
                {
                    // Remover todos los servicios relacionados con DbContext
                    var dbContextServices = services
                        .Where(s => s.ServiceType.FullName?.Contains("TPIContext") == true ||
                                   s.ServiceType == typeof(DbContextOptions<TPIContext>))
                        .ToList();
                    
                    foreach (var service in dbContextServices)
                    {
                        services.Remove(service);
                    }

                    // Agregar InMemory database para tests CON NOMBRE FIJO
                    services.AddDbContext<TPIContext>(options =>
                        options.UseInMemoryDatabase(dbName));

                    // Deshabilitar autenticación para tests - Sobrescribir TODAS las políticas específicas
                    services.PostConfigure<Microsoft.AspNetCore.Authorization.AuthorizationOptions>(options =>
                    {
                        // Crear una política que siempre autoriza
                        var alwaysAuthorizePolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                            .RequireAssertion(_ => true) // Siempre autorizado
                            .Build();
                        
                        // Sobrescribir todas las políticas específicas de la aplicación
                        options.AddPolicy("ClientesLeer", alwaysAuthorizePolicy);
                        options.AddPolicy("ClientesAgregar", alwaysAuthorizePolicy);
                        options.AddPolicy("ClientesActualizar", alwaysAuthorizePolicy);
                        options.AddPolicy("ClientesEliminar", alwaysAuthorizePolicy);
                        
                        // También sobrescribir políticas por si existen otras entidades
                        options.AddPolicy("PaisesLeer", alwaysAuthorizePolicy);
                        options.AddPolicy("PaisesAgregar", alwaysAuthorizePolicy);
                        options.AddPolicy("PaisesActualizar", alwaysAuthorizePolicy);
                        options.AddPolicy("PaisesEliminar", alwaysAuthorizePolicy);
                        
                        options.AddPolicy("UsuariosLeer", alwaysAuthorizePolicy);
                        options.AddPolicy("UsuariosAgregar", alwaysAuthorizePolicy);
                        options.AddPolicy("UsuariosActualizar", alwaysAuthorizePolicy);
                        options.AddPolicy("UsuariosEliminar", alwaysAuthorizePolicy);
                        
                        options.AddPolicy("PedidosLeer", alwaysAuthorizePolicy);
                        options.AddPolicy("PedidosAgregar", alwaysAuthorizePolicy);
                        options.AddPolicy("PedidosActualizar", alwaysAuthorizePolicy);
                        options.AddPolicy("PedidosEliminar", alwaysAuthorizePolicy);
                        
                        options.AddPolicy("ProductosLeer", alwaysAuthorizePolicy);
                        
                        // Política por defecto también autorizada
                        options.DefaultPolicy = alwaysAuthorizePolicy;
                        options.FallbackPolicy = null; // No fallback policy
                    });
                });
            });

            client = this.factory.CreateClient();
            
            // Inicializar datos de prueba
            using var scope = this.factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
            SeedTestData(context);
        }

        private static void SeedTestData(TPIContext context)
        {
            // Limpiar datos existentes
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Agregar países de prueba
            var argentina = new Pais(0, "Argentina"); // 0 = auto-generated ID
            var brasil = new Pais(0, "Brasil");
            context.Paises.AddRange(argentina, brasil);
            context.SaveChanges();

            // Recargar para obtener IDs generados
            argentina = context.Paises.First(p => p.Nombre == "Argentina");
            brasil = context.Paises.First(p => p.Nombre == "Brasil");

            // Agregar clientes de prueba
            var cliente1 = new Cliente(0, "Juan", "Perez", "juan@test.com", argentina.Id, DateTime.Now.AddDays(-30));
            var cliente2 = new Cliente(0, "Ana", "Garcia", "ana@test.com", brasil.Id, DateTime.Now.AddDays(-15));
            
            cliente1.SetPais(argentina);
            cliente2.SetPais(brasil);
            
            context.Clientes.AddRange(cliente1, cliente2);
            context.SaveChanges();
        }

        [Fact]
        public async Task Get_TodosLosClientes_RetornaListaCompleta()
        {
            // Act
            var response = await client.GetAsync("/clientes");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var clientes = await response.Content.ReadFromJsonAsync<List<ClienteDTO>>();
            
            Assert.NotNull(clientes);
            Assert.Equal(2, clientes.Count);
            
            var juan = clientes.First(c => c.Email == "juan@test.com");
            Assert.Equal("Juan", juan.Nombre);
            Assert.Equal("Perez", juan.Apellido);
            Assert.Equal("Argentina", juan.PaisNombre);
        }

        [Fact]
        public async Task Get_ClientePorId_RetornaClienteCorrect()
        {
            // Act
            var response = await client.GetAsync("/clientes/1");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var cliente = await response.Content.ReadFromJsonAsync<ClienteDTO>();
            
            Assert.NotNull(cliente);
            Assert.Equal(1, cliente.Id);
            Assert.Equal("Juan", cliente.Nombre);
            Assert.Equal("Perez", cliente.Apellido);
            Assert.Equal("juan@test.com", cliente.Email);
            Assert.Equal("Argentina", cliente.PaisNombre);
        }

        [Fact]
        public async Task Get_ClienteNoExiste_Retorna404()
        {
            // Act
            var response = await client.GetAsync("/clientes/999");
            
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ClienteNuevo_CreaClienteCorrectamente()
        {
            // Arrange
            var nuevoCliente = new ClienteDTO
            {
                Nombre = "Carlos",
                Apellido = "Lopez",
                Email = "carlos@test.com",
                PaisId = 1
            };

            // Act
            var response = await client.PostAsJsonAsync("/clientes", nuevoCliente);
            
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            
            var clienteCreado = await response.Content.ReadFromJsonAsync<ClienteDTO>();
            Assert.NotNull(clienteCreado);
            Assert.True(clienteCreado.Id > 0);
            Assert.Equal("Carlos", clienteCreado.Nombre);
            Assert.Equal("Lopez", clienteCreado.Apellido);
            Assert.Equal("carlos@test.com", clienteCreado.Email);
            Assert.True(clienteCreado.FechaAlta > DateTime.Now.AddMinutes(-1));

            // Verificar que se puede obtener el cliente creado
            var getResponse = await client.GetAsync($"/clientes/{clienteCreado.Id}");
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_EmailDuplicado_RetornaBadRequest()
        {
            // Arrange - usar email que ya existe en los datos de prueba
            var clienteDuplicado = new ClienteDTO
            {
                Nombre = "Otro",
                Apellido = "Usuario", 
                Email = "juan@test.com", // Email duplicado
                PaisId = 1
            };

            // Act
            var response = await client.PostAsJsonAsync("/clientes", clienteDuplicado);
            
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            
            var errorContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Ya existe un cliente con el Email", errorContent);
        }

        [Fact]
        public async Task Put_ClienteExistente_ActualizaCorrectamente()
        {
            // Arrange
            var clienteActualizado = new ClienteDTO
            {
                Id = 1,
                Nombre = "Juan Carlos", // Nombre actualizado
                Apellido = "Perez",
                Email = "juan.carlos@test.com", // Email actualizado
                PaisId = 2, // País actualizado
                FechaAlta = DateTime.Now.AddDays(-30)
            };

            // Act
            var response = await client.PutAsJsonAsync("/clientes", clienteActualizado);
            
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);

            // Verificar que la actualización fue exitosa
            var getResponse = await client.GetAsync("/clientes/1");
            var cliente = await getResponse.Content.ReadFromJsonAsync<ClienteDTO>();
            
            Assert.NotNull(cliente);
            Assert.Equal("Juan Carlos", cliente.Nombre);
            Assert.Equal("juan.carlos@test.com", cliente.Email);
            Assert.Equal(2, cliente.PaisId);
        }

        [Fact]
        public async Task Delete_ClienteExistente_EliminaCorrectamente()
        {
            // Act
            var response = await client.DeleteAsync("/clientes/2");
            
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);

            // Verificar que el cliente fue eliminado
            var getResponse = await client.GetAsync("/clientes/2");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        // COMENTADO: Este test no funciona con InMemory database porque GetByCriteria() 
        // usa ADO.NET directo con SqlConnection, no compatible con InMemory provider
        /*
        [Fact]
        public async Task Get_BuscarPorCriteria_RetornaClientesFiltrados()
        {
            // Act - buscar por "Juan"
            var response = await client.GetAsync("/clientes/criteria?texto=Juan");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var clientes = await response.Content.ReadFromJsonAsync<List<ClienteDTO>>();
            
            Assert.NotNull(clientes);
            Assert.Single(clientes);
            Assert.Equal("Juan", clientes[0].Nombre);
            Assert.Equal("juan@test.com", clientes[0].Email);
        }
        */
    }
}