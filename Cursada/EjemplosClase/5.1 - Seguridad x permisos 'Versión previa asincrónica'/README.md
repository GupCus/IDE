# Proyecto TPI - Cliente Management System

## Descripción

Proyecto educativo que demuestra múltiples tecnologías de UI (.NET) consumiendo la misma WebAPI con Entity Framework para persistencia de datos.

### Tecnologías Implementadas
- **WindowsForms**: Aplicación desktop tradicional
- **Blazor Server**: Aplicación web con renderizado en servidor
- **Blazor WebAssembly**: SPA con ejecución en cliente
- **WebAPI**: API REST con ASP.NET Core
- **Entity Framework Core**: ORM con SQL Server LocalDB

## Arquitectura Actual

```
UI Layer (WindowsForms/Blazor Server/Blazor WebAssembly)
    ↓
API.Clients (HTTP Client Wrapper)
    ↓
WebAPI (REST Endpoints)
    ↓
Application.Services (Business Logic)
    ↓
Data (Repository Pattern)
    ↓
Domain.Model (Entity)
```

## Estructura de Proyectos

- **Domain.Model**: Entidades de dominio (Cliente)
- **DTOs**: Objetos de transferencia de datos
- **Data**: Acceso a datos con Entity Framework (ClienteRepository, ClienteContext)
- **Application.Services**: Lógica de negocio (ClienteService)
- **WebAPI**: Controladores REST
- **API.Clients**: Cliente HTTP para consumir la API
- **WindowsForms**: Interfaz desktop
- **Blazor.Server**: Interfaz web server-side
- **Blazor.WebAssembly**: Interfaz web client-side

## Cómo Ejecutar

1. **Configurar Base de Datos**:
   - El proyecto usa SQL Server LocalDB
   - La base de datos se crea automáticamente con `Database.EnsureCreated()`

2. **Ejecutar WebAPI**:
   ```bash
   cd WindowsForms/WebAPI
   dotnet run
   ```

3. **Ejecutar Aplicaciones**:
   - **WindowsForms**: Ejecutar desde Visual Studio o `dotnet run`
   - **Blazor Server**: `cd Blazor.Server && dotnet run`
   - **Blazor WebAssembly**: `cd Blazor.WebAssembly && dotnet run`

## Mejoras Arquitectónicas Recomendadas

### 🔴 **Críticas (Para Implementar Pronto)**

#### 1. **Dependency Injection y IoC Container**
**Problema**: Service Locator anti-pattern (`new ClienteRepository()`, `new ClienteService()`)

**Solución**:
```csharp
// Crear interfaces
public interface IClienteRepository
{
    Task AddAsync(Cliente cliente);
    Task<bool> DeleteAsync(int id);
    Task<Cliente?> GetAsync(int id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<bool> UpdateAsync(Cliente cliente);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
}

public interface IClienteService
{
    Task<ClienteDTO> AddAsync(CreateClienteRequest request);
    Task<bool> DeleteAsync(int id);
    Task<ClienteDTO?> GetAsync(int id);
    Task<IEnumerable<ClienteDTO>> GetAllAsync();
    Task<bool> UpdateAsync(UpdateClienteRequest request);
}

// Configurar en Program.cs
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
```

#### 2. **Async/Await Pattern**
**Problema**: Métodos síncronos en acceso a datos

**Solución**: Convertir todos los métodos de datos a async
```csharp
public async Task AddAsync(Cliente cliente)
{
    using var context = CreateContext();
    context.Clientes.Add(cliente);
    await context.SaveChangesAsync();
}
```

#### 3. **HttpClient Management**
**Problema**: HttpClient estático puede causar socket exhaustion

**Solución**: Usar IHttpClientFactory
```csharp
// En Program.cs
builder.Services.AddHttpClient<IClienteApiClient, ClienteApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});
```

#### 4. **Centralización de Textos e Internacionalización**
**Problema**: Textos hardcodeados dispersos en múltiples archivos sin soporte para múltiples idiomas

**Problemas actuales**:
- Mensajes de validación: "El Nombre es requerido" en cada formulario
- Confirmaciones: "¿Está seguro que desea eliminar..." duplicadas
- Headers de UI: "Clientes", "Agregar", "Editar" hardcodeados
- Mensajes de éxito/error sin centralizar
- No hay soporte para múltiples idiomas

**Solución Opción 1**: Resource Files (.resx) - Estándar .NET
- Crear archivo `Resources/Messages.resx` (español por defecto)
- Crear archivo `Resources/Messages.en.resx` (inglés)
- Crear archivo `Resources/Validation.resx` para mensajes de validación
- Usar `ResourceManager` para acceder a textos localizados

**Solución Opción 2**: JSON + Servicio de Localización
- Crear archivos `Localization/es.json` y `Localization/en.json`
- Implementar `ILocalizationService` para cargar textos
- Registrar servicio en DI container
- Inyectar servicio donde se necesiten textos

**Beneficios educativos**:
- Enseña internacionalización (i18n)
- Separation of Concerns para textos
- Resource management
- Dependency Injection para servicios de localización
- Mantenibilidad y escalabilidad

#### 5. **Error Handling Estructurado**
**Problema**: Excepciones genéricas, no hay logging

**Solución**: Crear excepciones específicas y middleware global para manejo consistente de errores.

#### 6. **SQL Hardcodeado en Repositorios**
**Problema**: Consultas SQL como strings literales en el código (ej. método `GetByCriteria`)

**Solución**: 
- **Archivos de recursos**: Crear archivos `.sql` en carpeta `Scripts/` o `Resources/`
- **Resource files**: Usar archivos `.resx` para consultas SQL
- **Embedded resources**: Archivos SQL como recursos embebidos
- **Query builders**: Librerías como Dapper.Contrib o micro-ORMs

**Beneficios educativos**:
- Separation of concerns entre lógica y consultas
- Facilita mantenimiento de SQL complejo
- Permite versionado independiente de esquemas
- Introduce conceptos de gestión de recursos

### 🟡 **Importantes (Para Considerar)**

#### 7. **Repository Pattern con Unit of Work**
**Problema**: Context creado por operación

**Solución**: Shared context y Unit of Work pattern
```csharp
public interface IUnitOfWork : IDisposable
{
    IClienteRepository Clientes { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}
```

#### 7. **Domain-Driven Design Patterns**
**Problema**: Oportunidades de mejora en el modelo de dominio

**Situación actual**: El dominio tiene buena encapsulación y validaciones, pero podría beneficiarse de patrones DDD más avanzados.

**Mejoras propuestas**:
- **Value Objects**: Convertir Email en Value Object en lugar de string primitivo
- **Factory Methods**: Implementar `Cliente.Create()` para creación controlada
- **Domain Events**: Agregar eventos como `ClienteCreado`, `EmailCambiado`
- **Métodos de comportamiento**: Agregar lógica de negocio como `CambiarEmail()`, `EsClienteActivo()`

**Beneficios educativos**: Enseña patrones DDD avanzados manteniendo la base sólida actual.

#### 8. **CQRS Pattern**
**Problema**: Operaciones read/write mezcladas

**Solución**: Separar Commands y Queries usando MediatR
```csharp
public class CreateClienteCommand : IRequest<ClienteDTO>
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }  
    public string Email { get; set; }
}
```

#### 9. **Separation of Concerns en UI (WindowsForms)**
**Problema**: `ClienteDetalle` mezcla responsabilidades de UI, validación y lógica de negocio

**Problema actual**: El formulario de detalle llama directamente a la API y maneja múltiples responsabilidades, violando el principio de responsabilidad única.

**Comparación con Blazor**: En los proyectos Blazor, el patrón está mejor implementado:
- `ClienteModal` solo maneja UI y notifica eventos al padre
- `Clientes.razor` centraliza toda la lógica de negocio y llamadas a API

**Solución propuesta**: Implementar el mismo patrón en WindowsForms donde `ClientesLista` maneje toda la lógica de negocio (como `Clientes.razor`) y `ClienteDetalle` solo se encargue de UI y validación de formulario.

**Beneficios educativos**:
- Enseña Separation of Concerns
- Muestra Single Responsibility Principle
- Mantiene consistencia arquitectónica entre tecnologías UI
- Mejora testabilidad separando lógica de UI
- Introduce conceptos de event-driven design

#### 10. **Validation Layer**
**Problema**: Validación inconsistente

**Solución**: Implementar una capa de validación consistente usando atributos de validación en DTOs o librerías como FluentValidation para centralizar reglas de negocio.

### 🟢 **Opcionales (Para Proyectos Avanzados)**

#### 11. **Testing Structure**
- Unit Tests para Domain
- Integration Tests para Repository  
- API Tests para WebAPI

#### 12. **Logging y Monitoring**
- Structured logging con Serilog
- Health checks
- OpenTelemetry para observabilidad

#### 13. **Security**
- Authentication/Authorization
- HTTPS enforcement
- Input validation sanitization

#### 14. **Performance**
- Response caching
- Database connection pooling
- Pagination para listas grandes

## Prioridades de Implementación

### **Fase 1 (Fundación)**
1. Centralización de textos e internacionalización
2. Interfaces y Dependency Injection
3. Async/Await pattern
4. Error handling básico
5. HttpClient factory

### **Fase 2 (Arquitectura)**
1. Repository pattern mejorado
2. Unit of Work
3. Logging estructurado

### **Fase 3 (Dominio)**
1. Value Objects
2. Rich Domain Model
3. Domain Services

### **Fase 4 (Avanzado)**
1. CQRS con MediatR
2. FluentValidation
3. AutoMapper

### **Fase 5 (Producción)**
1. Testing completo
2. Security
3. Performance optimization
4. Monitoring

## Objetivos Educativos

### **Conceptos Enseñados**
- **Arquitectura en Capas**: Separación clara de responsabilidades
- **API-First Design**: Reutilización de lógica entre múltiples UIs
- **Repository Pattern**: Abstracción del acceso a datos
- **DTO Pattern**: Separación entre modelos de dominio y transferencia
- **Entity Framework**: ORM moderno para .NET

### **Buenas Prácticas Demostradas**
- Clean Code principles
- SOLID principles (cuando se implementen las mejoras)
- Domain-Driven Design basics
- RESTful API design
- Cross-platform UI development

## Notas para Estudiantes

Este proyecto progresivamente introduce conceptos de arquitectura de software moderna. Cada mejora tiene un propósito educativo específico:

- **DI**: Enseña inversión de control y testabilidad
- **Async/Await**: Performance y escalabilidad
- **Repository Pattern**: Abstracción y separation of concerns
- **Error Handling**: Robustez y experiencia de usuario
- **CQRS**: Escalabilidad y complejidad de dominio

## Contribuciones

Para contribuir a este proyecto educativo:
1. Fork el repositorio
2. Crea una rama para tu feature
3. Documenta el objetivo educativo de tu cambio
4. Asegúrate de que el código sea claro y educativo
5. Crea un Pull Request

---

**Nota**: Este es un proyecto educativo diseñado para enseñar arquitectura de software moderna en .NET. El énfasis está en la claridad y progresión educativa más que en la optimización prematura.