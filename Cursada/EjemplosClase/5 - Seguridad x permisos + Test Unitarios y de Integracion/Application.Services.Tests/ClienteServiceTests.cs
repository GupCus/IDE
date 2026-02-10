using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Moq;

namespace Application.Services.Tests
{
    public class ClienteServiceTests
    {
        [Fact]
        public void Get_ClienteExiste_RetornaClienteDTO()
        {
            // Arrange
            var cliente = new Cliente(1, "Juan", "Perez", "juan@test.com", 1, DateTime.Now);
            var pais = new Pais(1, "Argentina");
            cliente.SetPais(pais);

            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.Get(1)).Returns(cliente);

            var service = new ClienteService(mockRepository.Object);

            // Act
            var result = service.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Juan", result.Nombre);
            Assert.Equal("Perez", result.Apellido);
            Assert.Equal("juan@test.com", result.Email);
            Assert.Equal(1, result.PaisId);
            Assert.Equal("Argentina", result.PaisNombre);
        }

        [Fact]
        public void Get_ClienteNoExiste_RetornaNull()
        {
            // Arrange
            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.Get(999)).Returns((Cliente?)null);

            var service = new ClienteService(mockRepository.Object);

            // Act
            var result = service.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_ClienteValido_RetornaClienteDTOConId()
        {
            // Arrange
            var dto = new ClienteDTO 
            { 
                Nombre = "Ana", 
                Apellido = "Garcia", 
                Email = "ana@test.com", 
                PaisId = 1 
            };

            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.EmailExists(It.Is<string>(email => email == dto.Email), It.IsAny<int?>())).Returns(false);
            mockRepository.Setup(r => r.Add(It.IsAny<Cliente>()))
                         .Callback<Cliente>(c => c.SetId(100)); // Simular que la BD asigna ID 100

            var service = new ClienteService(mockRepository.Object);

            // Act
            var result = service.Add(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Id);
            Assert.Equal("Ana", result.Nombre);
            Assert.Equal("Garcia", result.Apellido);
            Assert.Equal("ana@test.com", result.Email);
            Assert.Equal(1, result.PaisId);
            Assert.True(result.FechaAlta > DateTime.Now.AddMinutes(-1)); // Fecha reciente
        }

        [Fact]
        public void Add_EmailDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new ClienteDTO 
            { 
                Nombre = "Pedro", 
                Apellido = "Lopez", 
                Email = "pedro@test.com", 
                PaisId = 1 
            };

            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.EmailExists(It.Is<string>(email => email == dto.Email), It.IsAny<int?>())).Returns(true);

            var service = new ClienteService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.Add(dto));
            Assert.Contains("Ya existe un cliente con el Email 'pedro@test.com'", exception.Message);
        }

        [Fact]
        public void Delete_IdExistente_RetornaTrue()
        {
            // Arrange
            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.Delete(1)).Returns(true);

            var service = new ClienteService(mockRepository.Object);

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void Delete_IdNoExistente_RetornaFalse()
        {
            // Arrange
            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.Delete(999)).Returns(false);

            var service = new ClienteService(mockRepository.Object);

            // Act
            var result = service.Delete(999);

            // Assert
            Assert.False(result);
        }
    }
}