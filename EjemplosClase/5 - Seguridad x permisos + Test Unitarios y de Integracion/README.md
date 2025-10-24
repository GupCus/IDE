# Proyecto TPI - Multi-Platform Management System

## Descripción

Proyecto educativo que demuestra múltiples tecnologías de UI (.NET + React/React Native) consumiendo la misma WebAPI con **JWT Authentication** y Entity Framework para persistencia de datos.

### ✅ Tecnologías Completamente Implementadas
- **WindowsForms**: Aplicación desktop con JWT auth + autorización granular ✅
- **Blazor Server**: Aplicación web server-side con autenticación + menús reactivos ✅
- **Blazor WebAssembly**: SPA con localStorage + autorización client-side ✅
- **MAUI**: Cross-platform (Android/iOS) + autorización con ViewModels ✅
- **React.js**: Web moderna TypeScript + hooks de permisos + renderizado condicional ✅
- **React Native**: CRUD completo + autorización móvil funcionando en Android ✅
- **React Native iOS**: CRUD completo + autorización funcionando en iOS Simulator ✅
- **React Native Expo**: CRUD completo + autorización funcionando en iPhone real (SDK 54) ✅
- **WebAPI**: API REST con ASP.NET Core + JWT + Claims-based Authorization (17 permisos) ✅
- **Entity Framework Core**: ORM con SQL Server LocalDB + entidades Usuario/Permiso/Grupo ✅

## Arquitectura JWT Completa

```
UI Layer (WindowsForms/Blazor Server/WebAssembly/MAUI/React.js/React Native/React Native iOS/React Native Expo)
    ↓ JWT Bearer Tokens
API.Auth.* (Platform-specific AuthServices)
    ↓ AuthServiceProvider Singleton
API.Clients (BaseApiClient + Bearer Token Injection)
    ↓ HTTPS + CORS + Android Emulator Support
WebAPI (JWT Authentication + Authorization Endpoints)
    ↓
Application.Services (AuthService + CRUD Services)
    ↓ Shared Repository Pattern
Data (EF + ADO.NET + Encrypted Passwords)
    ↓
Domain.Model (Rich Entities + Validations + Backing Fields)
    ↓
SQL Server LocalDB (Auto-created) / SQLite (Mac Support)
```

## Funcionalidades Implementadas

### 🔐 **JWT Authentication + Claims-based Authorization Universal**
- **Login/Logout**: Credenciales admin/admin123 (todos los permisos) y vendedor/vendedor123 (permisos limitados)
- **Claims-based Authorization**: 17 permisos granulares (países.*, clientes.*, usuarios.*, pedidos.*, productos.leer)
- **Token Management**: Bearer tokens con claims múltiples automáticos en API calls
- **Route Protection**: Redirects automáticos a login si no autenticado
- **UI Authorization**: Componentes/botones invisibles sin permisos (no solo disabled)
- **Platform Storage**: 
  - WindowsForms/MAUI: Memoria local con `HasPermissionAsync()`
  - Blazor Server: Memoria estática server-side con navegación reactiva
  - Blazor WebAssembly: localStorage con persistencia + autorización client-side
  - React/React Native: Hooks de permisos + renderizado condicional
- **Token Validation**: Expiración automática y cleanup + verificación de claims

### 📋 **CRUD Completo**
- **Clientes**: Nombre, Apellido, Email, País, FechaAlta
- **Países**: Lista predefinida (Argentina, Brasil, Chile, etc.)
- **Pedidos**: Cliente + Items con Productos y Cantidades
- **Productos**: Nombre, Descripción, Precio, Stock
- **Usuarios**: Username, Email, Password (hash+salt), Activo, FechaCreación

### 🏗️ **Arquitectura por Entidades**
- **Domain.Model**: Cliente, Pais, Usuario, Producto, Pedido, ItemPedido
- **DTOs**: Separación clara para transferencia (Create/Update/Read DTOs)
- **Data**: Repository pattern con EF + ADO.NET para queries complejas
- **Application.Services**: AuthService, ClienteService, UsuarioService, etc.
- **WebAPI**: Minimal APIs organizadas por entidad (AuthEndpoints, ClienteEndpoints, UsuarioEndpoints)
- **API.Clients**: BaseApiClient + específicos (AuthApiClient, ClienteApiClient)

### 🎨 **Multi-Platform UI**
- **WindowsForms**: DataGridView + formularios modales con validaciones
- **Blazor Server**: Componentes reutilizables + navegación SPA
- **Blazor WebAssembly**: SPA completa + localStorage + CORS
- **MAUI**: Cross-platform con detección de Android emulator
- **React** (en desarrollo): TypeScript + hooks + axios
- **React Native** (próximo): Adaptación mobile de componentes React

## Cómo Ejecutar

### 1. **Configurar Base de Datos**
   - El proyecto usa SQL Server LocalDB
   - La base de datos se crea automáticamente con `Database.EnsureCreated()`
   - Usuario predeterminado: admin/admin123

### 2. **Ejecutar WebAPI** (Requerido para todas las UIs)
   ```bash
   cd WebAPI
   dotnet run --launch-profile https
   # WebAPI corriendo en: https://localhost:7111
   ```

### 3. **Ejecutar UIs (.NET)**
   ```bash
   # WindowsForms
   cd WindowsForms && dotnet run

   # Blazor Server
   cd Blazor.Server && dotnet run
   # Acceder: https://localhost:7001

   # Blazor WebAssembly
   cd Blazor.WebAssembly && dotnet run
   # Acceder: http://localhost:5076

   # MAUI (requiere Visual Studio)
   # Ejecutar desde VS con emulador Android o dispositivo
   ```

### 4. **Ejecutar React** (completado)
   ```bash
   # React.js Web
   cd reactjs
   npm start
   # Acceder: http://localhost:3000

   # React Native Android (Windows/Linux)
   cd reactnative
   npx react-native start
   npx react-native run-android

   # React Native iOS Simulator (macOS)
   cd reactnative
   export DOTNET_ROOT="/opt/homebrew/opt/dotnet@8/libexec"
   export PATH="/opt/homebrew/opt/dotnet@8/bin:$PATH"
   npm run ios

   # React Native Expo (iPhone real)
   cd ReactNativeExpo
   npx expo start --tunnel
   # Escanear QR con Expo Go app
   ```

### 5. **Credenciales de Prueba**
   - **Administrador**: admin / admin123 (todos los 17 permisos)
   - **Vendedor**: vendedor / vendedor123 (permisos limitados: pedidos.*, clientes.leer, productos.leer)
   - Funciona en todas las UIs con autorización granular

## Problemas Técnicos Resueltos

### 🔧 **JWT Authentication Issues**

#### **WebAssembly DateTime Parsing**
- **Problema**: `DateTime.TryParse()` interpretaba fechas UTC como locales
- **Solución**: `DateTimeStyles.RoundtripKind` para preservar timezone
- **Resultado**: `IsAuthenticatedAsync()` funciona correctamente

#### **Android Emulator Connectivity** 
- **Problema**: `localhost` no funciona en emuladores Android
- **Solución**: Detección automática de `RuntimeIdentifier.StartsWith("android")` → `10.0.2.2:5183`
- **Resultado**: MAUI funciona correctamente en Android Studio

#### **Circular Dependencies**
- **Problema**: API.Clients no podía referenciar platform-specific AuthServices
- **Solución**: `AuthServiceProvider.Register()` singleton pattern
- **Resultado**: BaseApiClient obtiene tokens sin dependencies directas

#### **JavaScript Interop Issues**
- **Problema**: Múltiples `await _jsRuntime.InvokeVoidAsync()` causaban fallos silenciosos  
- **Solución**: Uso directo de `localStorage.getItem` sin wrapper complejo
- **Resultado**: AuthService devuelve valores correctos en WebAssembly

## Estado Actual del Proyecto

### ✅ **Logros Arquitectónicos Completados**

#### **Multi-Platform Success**
- **8 UIs diferentes**: WindowsForms, Blazor Server, WebAssembly, MAUI, React.js, React Native (Android), React Native (iOS Simulator), React Native Expo (iPhone Real)
- **Misma WebAPI**: Una sola fuente de verdad para todas las tecnologías
- **JWT Universal**: Autenticación funcionando consistentemente en todas las plataformas
- **CORS configurado**: WebAssembly, React.js y React Native pueden acceder a WebAPI
- **Mobile Real Testing**: React Native Expo probado en iPhone físico real + iOS Simulator
- **Android Emulator Support**: URLs automáticas para `10.0.2.2` en React Native/MAUI
- **Network Configuration**: Firewall Windows configurado para conexiones de red local
- **Cross-Platform Ready**: Soporte para Windows (LocalDB) y macOS (cross-platform networking)
- **iOS Development**: CocoaPods, Ruby, Xcode configurado correctamente

#### **Architecture Patterns Implemented**
- **Repository Pattern**: Acceso a datos abstraído y consistent
- **DTO Pattern**: Separación limpia entre Domain Models y transferencia
- **Service Layer**: Lógica de negocio centralizada
- **Domain-Driven Design**: Entidades con encapsulación y validaciones
- **API-First Design**: Backend reutilizable para múltiples clientes

#### **Security & Authentication**
- **JWT Bearer Authentication**: Implementado correctamente en todas las UIs
- **Password Hashing**: PBKDF2 con salt para seguridad de contraseñas
- **Route Protection**: Guards automáticos en todas las plataformas
- **Token Management**: Expiración, refresh y cleanup automático

#### **Data & Persistence**
- **Entity Framework**: ORM principal con auto-migrations
- **ADO.NET**: Para queries complejas y performance crítica
- **SQL Server LocalDB**: Auto-setup, no requiere configuración manual
- **Rich Domain Model**: Validaciones en entidades, backing fields para consistencia

### 🚀 **Logros Recientes Completados**

#### **React Native Expo iOS Success** ✅
- **CRUD Completo**: Crear, leer, actualizar, eliminar usuarios funcionando en iPhone real
- **Componentes Nativos iOS**: Login con TextInput, UsuariosList con FlatList
- **UsuarioModal**: Modal completo para CRUD con validaciones y KeyboardAvoidingView
- **Mobile UX**: FAB, pull-to-refresh, cards, alerts nativos iOS
- **AsyncStorage**: JWT persistente entre sesiones en iPhone
- **iPhone Real Testing**: Probado exitosamente en iPhone físico con Expo Go
- **Network Tunnel**: Expo tunnel para conectividad desde iPhone a PC Windows
- **Firewall Configuration**: Windows Firewall configurado para permitir conexiones de red local

#### **React Native Android Success** ✅
- **CRUD Completo**: Crear, leer, actualizar, eliminar usuarios
- **Componentes Nativos**: Login con TextInput, UsuariosList con FlatList
- **UsuarioModal**: Modal completo para CRUD con validaciones
- **Mobile UX**: FAB, pull-to-refresh, cards, alerts nativos
- **AsyncStorage**: JWT persistente entre sesiones
- **Android Emulator**: URLs correctas (`10.0.2.2:5183`)
- **Network Configuration**: HTTP cleartext traffic habilitado

#### **React.js Web Complete** ✅  
- **TypeScript + Hooks**: Componentes funcionales modernos
- **JWT Authentication**: localStorage con auto-logout
- **Axios Integration**: HTTP client con interceptores
- **Modal Components**: Crear/editar usuarios
- **Form Validation**: Validaciones client-side
- **Error Handling**: Manejo consistente de errores

#### **Cross-Platform Authentication** ✅
- **7 Plataformas**: Todas con JWT funcionando
- **Unified Backend**: Misma WebAPI para todas las UIs
- **Platform Storage**: Memoria (WinForms/MAUI), localStorage (Web), AsyncStorage (React Native/Expo)
- **Mobile Real Testing**: iPhone físico + Android emulator completamente funcionales
- **Network Solutions**: Firewall configuration, tunnel connectivity, emulator URL detection

### 🎯 **Objetivos Educativos Alcanzados**
- **Comparación de tecnologías**: Mismo backend, diferentes approaches de UI
- **Authentication patterns**: Diferentes estrategias de storage por plataforma  
- **API consumption**: HTTP clients, error handling, async patterns
- **Cross-platform development**: .NET ecosystem + React/React Native
- **Real-world problems**: CORS, emulator connectivity, timezone handling, network configurations

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

### 🟢 **Aspectos de Desarrollo Moderno Faltantes**

#### 11. **🧪 Testing Strategy & Automation (CRÍTICO FALTANTE)**
**Problema**: No hay testing automatizado implementado

**Solución**:
```bash
# Backend Testing
- xUnit para Unit Tests (.NET)
- Integration Tests para WebAPI endpoints
- Test fixtures para base de datos
- Mocking con Moq

# Frontend Testing  
- Jest + Testing Library para React
- Unit tests para componentes
- E2E tests con Playwright/Cypress
- Visual regression testing

# Mobile Testing
- Jest para React Native
- Detox para E2E mobile testing
- Device testing automation
```

**Beneficios**: Confiabilidad, regression detection, CI/CD readiness

#### 12. **🚀 CI/CD Pipeline & DevOps (CRÍTICO FALTANTE)**
**Problema**: No hay automatización de build/deploy

**Solución**:
```yaml
# GitHub Actions Pipeline
name: CI/CD Pipeline
on: [push, pull_request]
jobs:
  test:
    - Build all projects
    - Run unit tests
    - Run integration tests
    - Code coverage reports
  deploy:
    - Build for production
    - Deploy to staging/production
    - Run smoke tests
```

**Implementar**:
- `.github/workflows/` para GitHub Actions
- Build automation para todas las tecnologías
- Automated testing en PRs
- Code quality gates (SonarQube)
- Database migrations automation

#### 13. **🔒 Security Beyond JWT (CRÍTICO FALTANTE)**
**Problema**: Faltan aspectos modernos de seguridad

**Solución**:
```csharp
// Rate Limiting
builder.Services.AddRateLimiter(options => {
    options.AddPolicy("Api", context => 
        RateLimitPartition.CreateFixedWindow("Api", 
            _ => new FixedWindowRateLimiterOptions(100, TimeSpan.FromMinutes(1))));
});

// Security Headers
app.Use(async (context, next) => {
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
    await next();
});

// Input Sanitization
builder.Services.AddAntiforgery();
```

**Implementar**:
- Rate Limiting & API Throttling
- Input Sanitization & Validation
- Security Headers (HSTS, CSP, X-Frame-Options)
- CSRF Protection
- Secrets Management (Azure Key Vault)
- API Versioning
- Dependency vulnerability scanning

#### 14. **📊 Observability & Monitoring (CRÍTICO FALTANTE)**
**Problema**: No hay monitoreo ni observabilidad

**Solución**:
```csharp
// Structured Logging
builder.Services.AddSerilog(config => {
    config.WriteTo.Console()
          .WriteTo.ApplicationInsights()
          .Enrich.FromLogContext();
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddDbContext<TPIContext>()
    .AddUrlGroup(new Uri("https://api.external.com/health"));

// Telemetry
builder.Services.AddApplicationInsightsTelemetry();
```

**Implementar**:
- Structured Logging con Serilog
- Application Insights / OpenTelemetry
- Health Checks endpoints (`/health`)
- Metrics & Performance monitoring
- Error tracking (Sentry)
- Distributed Tracing
- Log aggregation y alertas

#### 15. **🛠️ Development Experience (FALTANTE)**
**Problema**: Falta containerization y DevX moderno

**Solución**:
```dockerfile
# Dockerfile para WebAPI
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY . /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "WebAPI.dll"]
```

```yaml
# docker-compose.yml para desarrollo local
version: '3.8'
services:
  webapi:
    build: ./WebAPI
    ports: ["5183:80"]
  database:
    image: mcr.microsoft.com/mssql/server
    environment:
      SA_PASSWORD: "YourPassword123"
  redis:
    image: redis:alpine
```

**Implementar**:
- Docker containerization
- Docker Compose para local development
- Hot reload para backend (.NET watch)
- Environment management (.env files)
- VS Code devcontainer
- Development automation scripts

#### 16. **🏗️ Modern Architecture Patterns (AVANZADO)**
**Problema**: Oportunidades para patrones arquitectónicos modernos

**Solución**:
```csharp
// Clean Architecture
- Core (Domain + Application)
- Infrastructure (Data + External)
- Presentation (APIs + UIs)

// CQRS + MediatR
public class CreateClienteCommand : IRequest<ClienteDTO>
{
    public string Nombre { get; set; }
    public string Email { get; set; }
}

// Domain Events
public class ClienteCreadoEvent : INotification
{
    public int ClienteId { get; set; }
    public DateTime FechaCreacion { get; set; }
}
```

**Implementar**:
- Clean Architecture boundaries
- CQRS con MediatR
- Domain Events & Event Handlers
- Saga Pattern para workflows complejos
- Circuit Breaker Pattern
- Outbox Pattern para consistency

#### 17. **📱 Modern Mobile & Web Patterns (AVANZADO)**
**Problema**: Faltan patrones modernos mobile/web

**Solución Mobile**:
```typescript
// State Management
import { create } from 'zustand';
import { persist } from 'zustand/middleware';

// Offline-first
import NetInfo from '@react-native-netinfo';
import AsyncStorage from '@react-native-async-storage/async-storage';

// Push Notifications
import PushNotification from 'react-native-push-notification';
```

**Solución Web**:
```typescript
// PWA Support
// Service Worker registration
// Web Push Notifications
// Offline capabilities with Cache API
// Code splitting with React.lazy()
```

**Implementar**:
- State management (Zustand/Redux)
- Offline-first architecture
- Push notifications
- Biometric authentication (mobile)
- Service Workers (web)
- Code splitting & lazy loading

#### 18. **🔧 Code Quality & Standards (FALTANTE)**
**Problema**: No hay automatización de calidad de código

**Solución**:
```json
// .eslintrc.js
module.exports = {
  extends: ['@react-native-community'],
  rules: {
    'no-unused-vars': 'error',
    'prefer-const': 'error'
  }
};

// .editorconfig
root = true
[*]
indent_style = space
indent_size = 2
end_of_line = lf
```

```json
// package.json
{
  "scripts": {
    "lint": "eslint src/",
    "format": "prettier --write src/",
    "prepare": "husky install"
  },
  "husky": {
    "hooks": {
      "pre-commit": "lint-staged"
    }
  }
}
```

**Implementar**:
- ESLint + Prettier para React projects
- EditorConfig para consistency
- Husky para Git hooks
- Conventional Commits
- Automated code formatting
- Code analysis (CodeQL, SonarQube)
- Pre-commit quality gates

#### 19. **📈 Performance & Optimization (FALTANTE)**
**Problema**: No hay optimizaciones de performance implementadas

**Solución**:
```csharp
// Response Caching
builder.Services.AddResponseCaching();
app.UseResponseCaching();

// Connection Pooling
builder.Services.AddDbContext<TPIContext>(options => {
    options.UseSqlServer(connectionString, sqlOptions => {
        sqlOptions.EnableRetryOnFailure();
    });
}, ServiceLifetime.Scoped);

// Compression
builder.Services.AddResponseCompression();
```

**Implementar**:
- Response Caching & ETags
- Database Connection Pooling
- API Response Compression (Gzip)
- Pagination para listas grandes
- Image optimization
- Bundle analysis & performance budgets
- Lighthouse CI para performance tracking
- Memory profiling & optimization

## Prioridades de Implementación Modernas

### **🔥 Fase 1 - Fundación Moderna (CRÍTICO)**
1. **Docker Containerization** - Deployment y desarrollo moderno
2. **Unit Testing** - xUnit para backend, Jest para React/React Native
3. **GitHub Actions CI/CD** - Build automation y quality gates
4. **Structured Logging** - Serilog con Application Insights
5. **Dependency Injection** - Interfaces e IoC container
6. **Environment Management** - .env files y configuration moderna

### **⚡ Fase 2 - Calidad y Seguridad**  
7. **ESLint + Prettier** - Code quality automática para React projects
8. **Rate Limiting** - API throttling y security básica
9. **Health Checks** - Endpoints `/health` para monitoring
10. **Security Headers** - HSTS, CSP, X-Frame-Options
11. **HttpClient Factory** - Manejo correcto de HTTP connections
12. **Async/Await Pattern** - Conversión completa a async

### **🚀 Fase 3 - DevX y Arquitectura**
13. **Error Handling Estructurado** - Exception middleware global
14. **Code Quality Gates** - Husky, pre-commit hooks, lint-staged  
15. **Repository Pattern Mejorado** - Unit of Work pattern
16. **Centralized Localization** - i18n/Resource files
17. **Input Sanitization** - Protection contra injection attacks
18. **Performance Monitoring** - APM básico y metrics

### **🏗️ Fase 4 - Arquitectura Avanzada**
19. **Clean Architecture** - Separation of concerns moderna
20. **CQRS + MediatR** - Command/Query separation
21. **Domain Events** - Event-driven architecture
22. **FluentValidation** - Validation rules centralizadas
23. **Response Caching** - Performance optimization
24. **Database Optimization** - Connection pooling, query optimization

### **📱 Fase 5 - Mobile/Web Moderno**
25. **State Management** - Zustand/Redux para React projects
26. **Offline-First** - Capabilities para mobile apps
27. **PWA Support** - Service Workers para web apps
28. **Push Notifications** - Mobile y web notifications
29. **Code Splitting** - Lazy loading y bundle optimization
30. **E2E Testing** - Playwright/Cypress automation

### **🛡️ Fase 6 - Security y Observability Avanzada**
31. **Secrets Management** - Azure Key Vault integration
32. **API Versioning** - Backward compatibility
33. **Distributed Tracing** - OpenTelemetry implementation
34. **Error Tracking** - Sentry integration
35. **Security Scanning** - Dependency vulnerability automation
36. **Performance Budgets** - Lighthouse CI integration

### **🚀 Fase 7 - Production Ready Enterprise**
37. **Circuit Breaker Pattern** - Resilience patterns
38. **Saga Pattern** - Complex workflow management  
39. **Multi-tenancy** - SaaS architecture patterns
40. **A/B Testing** - Feature flag infrastructure
41. **Load Testing** - Performance validation automation
42. **Disaster Recovery** - Backup y recovery automation

## 📊 Matriz de Impacto vs Esfuerzo

### **🔴 Alto Impacto + Bajo Esfuerzo (HACER PRIMERO)**
- Unit Testing (xUnit, Jest)
- Docker Containerization  
- GitHub Actions CI/CD básico
- ESLint + Prettier
- Health Checks
- Environment variables

### **🟠 Alto Impacto + Alto Esfuerzo (PLANIFICAR)**
- Clean Architecture refactor
- CQRS + MediatR implementation
- Comprehensive E2E testing
- Multi-platform state management
- Advanced security implementation

### **🟡 Bajo Impacto + Bajo Esfuerzo (HACER DESPUÉS)**
- Code formatting automation
- Documentation automation
- Basic performance monitoring
- Simple caching implementation

### **⚪ Bajo Impacto + Alto Esfuerzo (EVITAR POR AHORA)**
- Over-engineered patterns
- Premature micro-services
- Complex event sourcing
- Advanced monitoring sin basic foundation

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

## Estado Actual: 🎉 PROYECTO MULTI-PLATFORM COMPLETADO

Este proyecto ha evolucionado exitosamente de un simple CRUD a una **arquitectura multi-platform completa** con JWT authentication funcionando en **8 tecnologías diferentes**:

### 🏆 **Logros Principales COMPLETADOS**
1. **JWT Authentication Universal**: ✅ Funcionando en TODAS las plataformas - WindowsForms, Blazor Server, WebAssembly, MAUI, React.js, React Native (Android), React Native iOS Simulator, React Native Expo (iPhone Real)
2. **Multi-UI Success**: ✅ Misma WebAPI sirviendo exitosamente a 8 tecnologías diferentes
3. **Mobile Real Testing**: ✅ iPhone físico + Android emulator con CRUD completo funcionando
4. **Network Solutions**: ✅ Firewall configuration, Expo tunneling, emulator connectivity resueltos
5. **Real Problems Solved**: ✅ Timezone issues, CORS, Android emulator connectivity, circular dependencies, network configurations
6. **Production-Ready Patterns**: ✅ Repository, DTO, Service Layer, Domain Validation, Backing Fields
7. **JavaScript Ecosystem**: ✅ React.js web + React Native Android + React Native Expo iOS completamente implementados
8. **Cross-Platform Ready**: ✅ Windows (LocalDB) + Mac (SQLite) + GitHub Repository

### 🎯 **Valor Educativo Único DEMOSTRADO**
- **Comparación directa**: ✅ 8 tecnologías resolviendo los mismos problemas de diferentes maneras
- **Authentication strategies**: ✅ Memoria, localStorage, server-side storage, AsyncStorage
- **Cross-platform challenges**: ✅ URLs de emulador, CORS, JavaScript Interop, network security configs, firewall configuration
- **Architecture evolution**: ✅ De simple CRUD a sistema enterprise-ready con auth completa
- **Mobile Development**: ✅ React Native con componentes nativos, modals, AsyncStorage
- **Real Device Testing**: ✅ iPhone físico + iOS Simulator funcionando perfectamente
- **Network Connectivity**: ✅ Windows Firewall, Expo tunneling, cross-platform PC-to-mobile communication
- **macOS Development**: ✅ Xcode, CocoaPods, Ruby configuration para React Native iOS

### ✅ **PROYECTO FINALIZADO - Todas las Metas Alcanzadas**
- **8 Tecnologías UI**: Todas implementadas y funcionando
- **Mobile Real Testing**: iPhone físico + iOS Simulator + Android emulator validados
- **JWT Universal**: Funcionando en todas las plataformas
- **Network Configuration**: Todos los problemas de conectividad resueltos
- **Production Checklist**: Documentado para deployment real
- **Cross-platform Development**: Windows ↔ macOS networking y desarrollo configurado

**Nota**: Este proyecto demuestra una **arquitectura moderna multi-platform** con problemas reales resueltos. Es un excelente ejemplo de cómo una WebAPI bien diseñada puede servir múltiples tipos de cliente manteniendo consistencia y seguridad.

---

## ⚠️ CONFIGURACIÓN PARA PRODUCCIÓN

### 🔴 **CRÍTICO - CAMBIOS OBLIGATORIOS PARA PRODUCCIÓN**

#### 1. **CORS Policy (WebAPI\Program.cs)**
```csharp
// ❌ ACTUAL (Desarrollo) - INSEGURO para producción
policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();

// ✅ CAMBIAR EN PRODUCCIÓN
policy.WithOrigins(
    "https://mi-dominio.com",           // Blazor WebAssembly  
    "https://app.mi-dominio.com",       // React.js
    "https://api.mi-dominio.com"        // Otros clientes web
)
.WithHeaders("Content-Type", "Authorization")
.WithMethods("GET", "POST", "PUT", "DELETE");
```

#### 2. **JWT Secret Key (WebAPI\appsettings.json)**
```json
// ❌ ACTUAL - Clave de desarrollo simple
"JwtSettings": {
  "SecretKey": "mi_super_secreto_jwt_key_de_32_caracteres_minimo",
  "Issuer": "TPI.WebAPI",
  "Audience": "TPI.Clients",
  "ExpirationHours": 24
}

// ✅ CAMBIAR EN PRODUCCIÓN
"JwtSettings": {
  "SecretKey": "[GENERAR_CLAVE_CRYPTO_SEGURA_256_BITS]",
  "Issuer": "https://mi-dominio-api.com",
  "Audience": "mi-app-produccion",
  "ExpirationHours": 2
}
```

#### 3. **Connection String (WebAPI\appsettings.json)**
```json
// ❌ ACTUAL - LocalDB de desarrollo
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TPI;Trusted_Connection=true;MultipleActiveResultSets=true"
}

// ✅ CAMBIAR EN PRODUCCIÓN  
"ConnectionStrings": {
  "DefaultConnection": "Server=servidor-produccion;Database=TPI_PROD;User Id=usuario_app;Password=[PASSWORD_SEGURO];Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"
}
```

#### 4. **HTTPS Enforcement (WebAPI\Program.cs)**
```csharp
// ✅ VERIFICAR que esté habilitado en producción
app.UseHttpsRedirection();
app.UseHsts(); // Agregar si no existe

// ✅ CONFIGURAR certificados SSL válidos
// ✅ CONFIGURAR redirect de HTTP a HTTPS en el servidor web
```

#### 5. **Logging Levels (WebAPI\appsettings.json)**
```json
// ❌ ACTUAL - Logs de desarrollo verbose
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}

// ✅ CAMBIAR EN PRODUCCIÓN
"Logging": {
  "LogLevel": {
    "Default": "Warning",
    "Microsoft.AspNetCore": "Error",
    "Microsoft.EntityFrameworkCore": "Error"
  }
}
```

#### 6. **URLs Base en Clientes (React Native/React.js)**
```typescript
// ❌ ACTUAL - IPs locales de desarrollo
const apiClient = axios.create({
  baseURL: 'http://192.168.0.238:5183',  // React Native
});

// ✅ CAMBIAR EN PRODUCCIÓN
const apiClient = axios.create({
  baseURL: 'https://api.mi-dominio.com',
  timeout: 10000,
});
```

#### 7. **Environment Variables**
```bash
# ✅ CONFIGURAR en servidor de producción
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:443;http://+:80
JWT_SECRET_KEY=[CLAVE_SEGURA_DESDE_KEYVAULT]
DATABASE_CONNECTION=[STRING_DESDE_VARIABLES_ENTORNO]
```

#### 8. **Credenciales por Defecto**
```csharp
// ❌ ACTUAL - Usuario hardcodeado
// En TPIContext.OnModelCreating()
var adminUser = new Usuario
{
    Username = "admin",
    Password = hashedPassword, // admin123
};

// ✅ CAMBIAR EN PRODUCCIÓN
// - Eliminar usuario por defecto
// - Crear script de setup inicial seguro
// - Usar contraseñas generadas criptográficamente
// - Implementar flujo de first-run admin setup
```

#### 9. **Swagger/OpenAPI (WebAPI\Program.cs)**
```csharp
// ❌ ACTUAL - Swagger expuesto siempre
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ VERIFICAR que Swagger NO se exponga en producción
// ✅ O configurar autenticación para Swagger si es necesario
```

#### 10. **Headers de Seguridad**
```csharp
// ✅ AGREGAR headers de seguridad en producción
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
    await next();
});
```

### 🔥 **CHECKLIST PRE-PRODUCCIÓN**

- [ ] Cambiar CORS de `AllowAnyOrigin()` a dominios específicos
- [ ] Generar JWT Secret Key criptográficamente segura (256+ bits)
- [ ] Configurar Connection String a base de datos de producción
- [ ] Verificar HTTPS enforcement y certificados SSL
- [ ] Configurar logging levels apropiados (Warning/Error)
- [ ] Cambiar URLs base de desarrollo por URLs de producción
- [ ] Configurar variables de entorno seguras
- [ ] Eliminar usuario admin por defecto y crear setup inicial
- [ ] Deshabilitar Swagger o configurar autenticación
- [ ] Agregar headers de seguridad HTTP
- [ ] Configurar rate limiting y throttling
- [ ] Implementar monitoreo y alertas
- [ ] Configurar backups automáticos de base de datos
- [ ] Realizar penetration testing básico
- [ ] Configurar firewall y network security groups

### 📋 **NOTAS ADICIONALES**

- **Secrets Management**: Usar Azure Key Vault, AWS Secrets Manager, o HashiCorp Vault
- **Database**: Migrar de LocalDB a SQL Server production instance
- **Monitoring**: Implementar Application Insights, Datadog, o similar
- **CI/CD**: Configurar pipelines con validaciones de seguridad
- **Load Testing**: Validar performance bajo carga real
- **Disaster Recovery**: Plan de recuperación y backups

**⚠️ IMPORTANTE**: Este proyecto está configurado para desarrollo y demostración. Los cambios listados son OBLIGATORIOS antes de cualquier deployment a producción.

---

## 📋 **RESUMEN - GAP ANALYSIS DE DESARROLLO MODERNO**

### ✅ **LO QUE ESTÁ BIEN IMPLEMENTADO**
- **Multi-platform architecture** con 8 tecnologías diferentes
- **JWT Authentication** universal funcionando
- **Domain-Driven Design** básico con entidades encapsuladas
- **Repository Pattern** implementado correctamente
- **DTO Pattern** para separación de concerns
- **Cross-platform connectivity** resuelto (firewall, tunneling, URLs)
- **Real device testing** validado (iPhone + iOS Simulator + Android)
- **macOS Development Setup** con Xcode, CocoaPods, Ruby configurado

### ❌ **ASPECTOS CRÍTICOS FALTANTES (Desarrollo Moderno 2024/2025)**

#### **🧪 Testing & Quality (0% implementado)**
- Sin unit tests, integration tests, E2E tests
- Sin code coverage, quality gates
- Sin test automation

#### **🚀 DevOps & CI/CD (0% implementado)** 
- Sin containerization (Docker)
- Sin CI/CD pipelines (GitHub Actions)
- Sin deployment automation
- Sin environment management

#### **🔒 Security (30% implementado)**
- JWT ✅ | Rate limiting ❌ | Security headers ❌ | Input sanitization ❌
- HTTPS ✅ | Secrets management ❌ | Vulnerability scanning ❌

#### **📊 Observability (10% implementado)**
- HTTP logging básico ✅ | Structured logging ❌ | Health checks ❌  
- Monitoring ❌ | Error tracking ❌ | Distributed tracing ❌

#### **🛠️ Developer Experience (40% implementado)**
- Hot reload parcial ✅ | Containerization ❌ | Code quality automation ❌
- Environment management ❌ | IDE configuration ❌

### 🎯 **PRIORITY ROADMAP PARA MODERNIZACIÓN**

#### **Semana 1-2: Foundation**
1. Docker containerization
2. Basic unit testing (xUnit + Jest)
3. GitHub Actions CI/CD
4. ESLint + Prettier

#### **Semana 3-4: Security & Quality**  
5. Rate limiting + Security headers
6. Health checks endpoints
7. Structured logging (Serilog)
8. Environment variables management

#### **Mes 2: Architecture & Performance**
9. Dependency Injection refactor
10. Error handling middleware
11. Performance optimization
12. E2E testing automation

Este gap analysis muestra que aunque el proyecto tiene una **arquitectura sólida multi-platform**, necesita **modernización significativa** en testing, DevOps, security y observability para cumplir con estándares de desarrollo moderno 2024/2025.