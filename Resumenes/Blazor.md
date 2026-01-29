# Desarrollo de Aplicaciones Web con Blazor

## Índice

- [Desarrollo de Aplicaciones Web con Blazor](#desarrollo-de-aplicaciones-web-con-blazor)
  - [Índice](#índice)
  - [1. Introducción a Blazor](#1-introducción-a-blazor)
    - [1.1 ¿Por qué utilizar Blazor?](#11-por-qué-utilizar-blazor)
  - [2. Modelos de Hospedaje (Hosting Models)](#2-modelos-de-hospedaje-hosting-models)
    - [2.1 Blazor Server](#21-blazor-server)
    - [2.2 Blazor WebAssembly (WASM)](#22-blazor-webassembly-wasm)
    - [2.3 Comparativa Rápida](#23-comparativa-rápida)
    - [2.4 Blazor WebAssembly: Configuración y Estructura](#24-blazor-webassembly-configuración-y-estructura)
      - [Creación del Proyecto en Visual Studio 2022](#creación-del-proyecto-en-visual-studio-2022)
      - [Estructura de Proyecto Blazor WASM](#estructura-de-proyecto-blazor-wasm)
      - [Program.cs en Blazor WebAssembly](#programcs-en-blazor-webassembly)
      - [Consumo de APIs REST en Blazor WASM](#consumo-de-apis-rest-en-blazor-wasm)
      - [Configuración PWA (Progressive Web App)](#configuración-pwa-progressive-web-app)
  - [3. Arquitectura de una Aplicación Blazor](#3-arquitectura-de-una-aplicación-blazor)
  - [4. Componentes Razor](#4-componentes-razor)
    - [4.1 Estructura Básica: El Contador](#41-estructura-básica-el-contador)
    - [4.2 Ciclo de Vida de Componentes](#42-ciclo-de-vida-de-componentes)
  - [5. Enrutamiento (Routing)](#5-enrutamiento-routing)
    - [5.1 Tipos de Rutas](#51-tipos-de-rutas)
    - [5.2 Parámetros de Ruta](#52-parámetros-de-ruta)
    - [5.3 Restricciones de Tipo en Rutas](#53-restricciones-de-tipo-en-rutas)
  - [6. Navegación (NavMenu)](#6-navegación-navmenu)
    - [6.1 Navegación Programática](#61-navegación-programática)
  - [7. Data Binding (Enlace de Datos)](#7-data-binding-enlace-de-datos)
    - [7.1 One-Way Binding (Unidireccional)](#71-one-way-binding-unidireccional)
    - [7.2 Two-Way Binding (Bidireccional)](#72-two-way-binding-bidireccional)
  - [8. Modelos de Datos y Validación](#8-modelos-de-datos-y-validación)
    - [8.1 Definición de la Entidad (Modelo)](#81-definición-de-la-entidad-modelo)
  - [9. Formularios y Validación (EditForm)](#9-formularios-y-validación-editform)
    - [9.1 Implementación Completa del Formulario](#91-implementación-completa-del-formulario)
    - [9.2 Componentes de Entrada de Blazor](#92-componentes-de-entrada-de-blazor)
    - [9.3 Ejemplo InputSelect](#93-ejemplo-inputselect)
  - [10. Listado de Datos (CRUD - Lectura)](#10-listado-de-datos-crud---lectura)
    - [10.1 Componente de Listado Completo](#101-componente-de-listado-completo)
  - [11. Inyección de Dependencias](#11-inyección-de-dependencias)
    - [11.1 Registro de Servicios (Program.cs)](#111-registro-de-servicios-programcs)
    - [11.2 Uso en Componentes](#112-uso-en-componentes)
  - [12. Interoperabilidad con JavaScript (JS Interop)](#12-interoperabilidad-con-javascript-js-interop)
    - [12.1 Llamar JavaScript desde C#](#121-llamar-javascript-desde-c)
    - [12.2 Función JS Personalizada](#122-función-js-personalizada)
  - [13. Resumen de Directivas Razor](#13-resumen-de-directivas-razor)

---

## 1. Introducción a Blazor

Blazor es un framework de código abierto desarrollado por Microsoft que permite construir aplicaciones web interactivas del lado del cliente (SPA - Single Page Applications) utilizando **C# y HTML** en lugar de JavaScript.

La fórmula fundamental es: **Browser + Razor = Blazor**.

### 1.1 ¿Por qué utilizar Blazor?

* **Código Reutilizable:** Permite compartir lógica y modelos de datos (clases C#) entre el cliente (frontend) y el servidor (backend).
* **Ecosistema .NET:** Acceso total a las librerías de .NET y herramientas de desarrollo como Visual Studio.
* **Curva de aprendizaje:** Si ya conoces C#, no necesitas aprender un ecosistema completamente nuevo de JavaScript (React/Angular/Vue).
* **Rendimiento:** Posibilidad de ejecución nativa en el navegador mediante WebAssembly.

---

## 2. Modelos de Hospedaje (Hosting Models)

Blazor ofrece dos arquitecturas principales para ejecutar la aplicación. La elección depende de los requerimientos de latencia, SEO y capacidad de cómputo.

### 2.1 Blazor Server

La lógica de la aplicación se ejecuta en el servidor (ASP.NET Core).

* **Funcionamiento:** El navegador actúa como una "terminal tonta". Las interacciones del usuario (clics, tecleo) se envían al servidor a través de una conexión **SignalR** (WebSockets). El servidor procesa el evento, renderiza la diferencia en el HTML y la envía de vuelta al navegador para actualizar el DOM.
* **Ventajas:** Carga inicial muy rápida (solo baja un pequeño JS), el código fuente (C#) no se descarga al cliente (seguridad).
* **Desventajas:** Requiere conexión constante; si se cae internet, la app deja de funcionar. Mayor latencia en cada interacción.
* **Uso ideal:** Apps corporativas internas, intranets, apps que no requieren manipular muchos recursos del cliente.

### 2.2 Blazor WebAssembly (WASM)

La aplicación se ejecuta completamente en el navegador del usuario.

* **Funcionamiento:** Se descargan al navegador los archivos HTML, CSS, una versión ligera del runtime de .NET (en formato WebAssembly) y las DLLs de tu aplicación.
* **Tecnología base:** Utiliza **WebAssembly (Wasm)**, un formato de instrucción binaria que permite ejecutar código de lenguajes de bajo nivel (C++, Rust, C#) en el navegador a velocidad casi nativa.
* **Ventajas:** Funciona offline (PWA), no carga al servidor con procesamiento de UI, respuesta inmediata en la interfaz.
* **Desventajas:** La carga inicial es más lenta (debe descargar el runtime y librerías ~2-5MB).
* **Uso ideal:** Aplicaciones públicas de alto tráfico, apps que requieren funcionalidad offline.

### 2.3 Comparativa Rápida

| Característica | Blazor Server | Blazor WebAssembly |
|----------------|---------------|---------------------|
| Ejecución | Servidor | Navegador (cliente) |
| Carga inicial | Rápida | Lenta (descarga runtime) |
| Conexión requerida | Permanente (SignalR) | Solo para APIs |
| Funciona offline | No | Sí (PWA) |
| Carga del servidor | Alta | Baja |
| Seguridad del código | Alta (no se descarga) | Menor (DLLs visibles) |

### 2.4 Blazor WebAssembly: Configuración y Estructura

#### Creación del Proyecto en Visual Studio 2022

1. **Nuevo Proyecto** → Buscar "Blazor WebAssembly Standalone App"
2. Configurar nombre y ubicación
3. Seleccionar framework (.NET 8 recomendado)
4. Opciones importantes:
   - **Progressive Web Application (PWA):** Habilita funcionalidad offline
   - **ASP.NET Core Hosted:** Crea un proyecto servidor que sirve la app WASM

#### Estructura de Proyecto Blazor WASM

```
MiAppBlazor/
├── wwwroot/
│   ├── index.html          # Página host principal
│   ├── css/
│   └── sample-data/
├── Pages/
│   ├── Index.razor
│   ├── Counter.razor
│   └── FetchData.razor
├── Shared/
│   ├── MainLayout.razor
│   └── NavMenu.razor
├── Program.cs              # Punto de entrada
├── _Imports.razor          # Imports globales
└── App.razor               # Componente raíz
```

#### Program.cs en Blazor WebAssembly

```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiAppBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración de HttpClient para llamadas a APIs
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});

// Registro de servicios personalizados
builder.Services.AddScoped<IAlumnoService, AlumnoService>();

await builder.Build().RunAsync();
```

#### Consumo de APIs REST en Blazor WASM

```csharp
@page "/alumnos"
@inject HttpClient Http

<h3>Listado de Alumnos</h3>

@if (alumnos == null)
{
    <p><em>Cargando...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Legajo</th>
                <th>Nombre</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var alumno in alumnos)
            {
                <tr>
                    <td>@alumno.Legajo</td>
                    <td>@alumno.Nombre</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private List<Alumno>? alumnos;

    protected override async Task OnInitializedAsync()
    {
        // Llamada HTTP a la API REST
        alumnos = await Http.GetFromJsonAsync<List<Alumno>>("api/alumnos");
    }

    // Método para enviar datos (POST)
    private async Task GuardarAlumno(Alumno nuevoAlumno)
    {
        var response = await Http.PostAsJsonAsync("api/alumnos", nuevoAlumno);
        if (response.IsSuccessStatusCode)
        {
            // Recargar lista
            alumnos = await Http.GetFromJsonAsync<List<Alumno>>("api/alumnos");
        }
    }
}
```

#### Configuración PWA (Progressive Web App)

Cuando habilitas PWA, se generan archivos adicionales:

* **service-worker.js:** Maneja el cache offline
* **manifest.json:** Define íconos, nombre y comportamiento de la app instalable

```json
// wwwroot/manifest.json
{
  "name": "Mi App Blazor",
  "short_name": "BlazorApp",
  "start_url": "./",
  "display": "standalone",
  "background_color": "#ffffff",
  "theme_color": "#512bd4",
  "icons": [
    {
      "src": "icon-512.png",
      "sizes": "512x512",
      "type": "image/png"
    }
  ]
}
```

---

## 3. Arquitectura de una Aplicación Blazor

La estructura de proyecto en Visual Studio 2022 (Template "Blazor Web App") organiza el código de la siguiente manera:

* **Program.cs:** Punto de entrada. Aquí se configuran los servicios (Inyección de Dependencias) y el pipeline HTTP.
* **App.razor:** El componente raíz que gestiona el enrutamiento general.
* **MainLayout.razor:** Define la plantilla maestra de la UI (barra lateral, encabezado y cuerpo principal `@Body`).
* **wwwroot:** Archivos estáticos (CSS, imágenes, JS de terceros).
* **Pages/Components:** Archivos `.razor` que contienen la UI y la lógica.

---

## 4. Componentes Razor

Un componente en Blazor (`.razor`) combina HTML y C# en un solo archivo.

### 4.1 Estructura Básica: El Contador

Este es el ejemplo canónico que demuestra la interacción básica (evento `onclick`) y la actualización de estado.

```csharp
@page "/contador"

<PageTitle>Contador</PageTitle>

<h1>Contador</h1>

<p>La cuenta actual es: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    // Estado del componente
    private int currentCount = 0;

    // Método que modifica el estado
    private void IncrementCount()
    {
        currentCount++;
        // Al terminar este método, Blazor detecta el cambio y repinta el DOM automáticamente.
    }
}
```

### 4.2 Ciclo de Vida de Componentes

Los componentes Blazor tienen varios métodos de ciclo de vida que se ejecutan en orden:

```csharp
@code {
    // 1. Se ejecuta al establecer los parámetros (inicial y en cada actualización)
    protected override void OnParametersSet()
    {
        // Lógica cuando cambian los parámetros
    }

    // 2. Se ejecuta una vez cuando el componente se inicializa
    protected override void OnInitialized()
    {
        // Inicialización síncrona
    }

    // 3. Versión asíncrona - ideal para cargar datos de BD o APIs
    protected override async Task OnInitializedAsync()
    {
        listaAlumnos = await Contexto.Alumnos.ToListAsync();
    }

    // 4. Se ejecuta después de cada renderizado
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            // Solo en el primer render (ej: inicializar JS interop)
        }
    }

    // 5. Versión asíncrona del anterior
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("initializeChart");
        }
    }
}
```

---

## 5. Enrutamiento (Routing)

El enrutamiento en Blazor se maneja mediante la directiva `@page` al inicio del archivo `.razor`.

### 5.1 Tipos de Rutas

* **Ruta Estática:** `@page "/alumnos"`
* **Ruta Dinámica (Parámetros):** `@page "/alumnos/{AlumnoId:int}"`
* **Múltiples Rutas:** Un componente puede responder a varias rutas

```csharp
@page "/alumnos/{AlumnoId:int}"
@page "/estudiantes/{AlumnoId:int}"
```

### 5.2 Parámetros de Ruta

Para capturar valores de la URL se utiliza el atributo `[Parameter]`:

```csharp
@page "/alumnos/{AlumnoId:int}"

<h3>Detalle del Alumno</h3>
<p>Estás viendo el ID: @AlumnoId</p>

@code {
    // El atributo [Parameter] es OBLIGATORIO para capturar el valor de la URL
    // La propiedad debe ser pública y tener get/set
    // El nombre "AlumnoId" debe coincidir exactamente con el de la ruta {AlumnoId}
    [Parameter]
    public int AlumnoId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        alumno = await _context.Alumnos.FindAsync(AlumnoId);
    }
}
```

### 5.3 Restricciones de Tipo en Rutas

| Restricción | Ejemplo | Coincide |
|-------------|---------|----------|
| `int` | `{id:int}` | 123 |
| `bool` | `{active:bool}` | true, false |
| `datetime` | `{date:datetime}` | 2024-01-15 |
| `guid` | `{id:guid}` | GUID válido |
| `long` | `{id:long}` | Entero largo |

---

## 6. Navegación (NavMenu)

Para que el usuario pueda acceder a las páginas, debemos modificar el componente `NavMenu.razor` (ubicado en la carpeta `Shared` o `Layout`).

El componente `NavLink` nativo de Blazor añade automáticamente la clase CSS `active` cuando la ruta coincide:

```html
<div class="nav-item px-3">
    <NavLink class="nav-link" href="alumnos">
        <span class="oi oi-people" aria-hidden="true"></span> Alumnos
    </NavLink>
</div>

<div class="nav-item px-3">
    <NavLink class="nav-link" href="alumnos/nuevo">
        <span class="oi oi-plus" aria-hidden="true"></span> Nuevo Alumno
    </NavLink>
</div>
```

### 6.1 Navegación Programática

```csharp
@inject NavigationManager Navigation

<button @onclick="IrADetalle">Ver Detalle</button>

@code {
    private void IrADetalle()
    {
        Navigation.NavigateTo("/alumnos/1");
    }

    private void IrADetalleConRecarga()
    {
        Navigation.NavigateTo("/alumnos/1", forceLoad: true);
    }
}
```

---

## 7. Data Binding (Enlace de Datos)

Es la sincronización entre la variable en C# y la visualización en HTML.

### 7.1 One-Way Binding (Unidireccional)

Del código a la vista. Si la variable cambia, la vista se actualiza:

```html
<p>Contador: @currentCount</p>
<p>Nombre: @alumno.Nombre</p>
```

### 7.2 Two-Way Binding (Bidireccional)

Esencial para formularios. Sincroniza la vista y el código mutuamente usando `@bind`:

```html
<input @bind="nombreUsuario" />
<p>Hola, @nombreUsuario</p>

<!-- Especificar evento de actualización -->
<input @bind="nombreUsuario" @bind:event="oninput" />

<!-- Binding con formato -->
<input @bind="fechaNacimiento" @bind:format="yyyy-MM-dd" />
```

---

## 8. Modelos de Datos y Validación

### 8.1 Definición de la Entidad (Modelo)

```csharp
using System.ComponentModel.DataAnnotations;

public class Alumno
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string Apellido { get; set; }
    
    [Required]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "El legajo debe tener 5 dígitos")]
    public string Legajo { get; set; }
    
    [EmailAddress(ErrorMessage = "Email no válido")]
    public string Email { get; set; }
    
    public string Direccion { get; set; }
}
```

---

## 9. Formularios y Validación (EditForm)

Blazor simplifica la gestión de formularios. En lugar de usar `<form>` HTML estándar, usamos `<EditForm>` que maneja el ciclo de validación.

### 9.1 Implementación Completa del Formulario

```csharp
@page "/alumnos/nuevo"
@page "/alumnos/editar/{AlumnoId:int}"

<h3>@(AlumnoId == 0 ? "Nuevo Alumno" : "Editar Alumno")</h3>

<EditForm Model="@alumno" OnValidSubmit="@GuardarAlumno" OnInvalidSubmit="@ManejarErrores">
    
    <DataAnnotationsValidator />
    <ValidationSummary />

    <div class="form-group mb-3">
        <label for="nombre">Nombre:</label>
        <InputText id="nombre" class="form-control" @bind-Value="alumno.Nombre" />
        <ValidationMessage For="@(() => alumno.Nombre)" />
    </div>

    <div class="form-group mb-3">
        <label for="apellido">Apellido:</label>
        <InputText id="apellido" class="form-control" @bind-Value="alumno.Apellido" />
        <ValidationMessage For="@(() => alumno.Apellido)" />
    </div>

    <div class="form-group mb-3">
        <label for="legajo">Legajo:</label>
        <InputText id="legajo" class="form-control" @bind-Value="alumno.Legajo" />
        <ValidationMessage For="@(() => alumno.Legajo)" />
    </div>

    <div class="form-group mb-3">
        <label for="email">Email:</label>
        <InputText id="email" class="form-control" @bind-Value="alumno.Email" />
        <ValidationMessage For="@(() => alumno.Email)" />
    </div>

    <button type="submit" class="btn btn-success">Guardar</button>
    <a href="/alumnos" class="btn btn-secondary">Cancelar</a>

</EditForm>

@code {
    [Parameter]
    public int AlumnoId { get; set; }

    private Alumno alumno = new Alumno();

    protected override async Task OnInitializedAsync()
    {
        if (AlumnoId > 0)
        {
            // Modo edición: cargar datos existentes
            alumno = await _context.Alumnos.FindAsync(AlumnoId);
        }
    }

    private async Task GuardarAlumno()
    {
        if (AlumnoId == 0)
        {
            _context.Alumnos.Add(alumno);
        }
        else
        {
            _context.Alumnos.Update(alumno);
        }
        await _context.SaveChangesAsync();
        Navigation.NavigateTo("/alumnos");
    }

    private void ManejarErrores()
    {
        Console.WriteLine("El formulario tiene errores de validación");
    }
}
```

### 9.2 Componentes de Entrada de Blazor

| Componente | Tipo de Dato | Uso |
|------------|--------------|-----|
| `InputText` | `string` | Texto simple |
| `InputTextArea` | `string` | Texto multilínea |
| `InputNumber<T>` | `int`, `decimal`, `double` | Números |
| `InputDate<T>` | `DateTime`, `DateOnly` | Fechas |
| `InputCheckbox` | `bool` | Casilla de verificación |
| `InputSelect<T>` | `enum`, `string`, `int` | Lista desplegable |

### 9.3 Ejemplo InputSelect

```csharp
<div class="form-group mb-3">
    <label>Carrera:</label>
    <InputSelect @bind-Value="alumno.CarreraId" class="form-control">
        <option value="">-- Seleccionar --</option>
        @foreach (var carrera in carreras)
        {
            <option value="@carrera.Id">@carrera.Nombre</option>
        }
    </InputSelect>
</div>
```

---

## 10. Listado de Datos (CRUD - Lectura)

### 10.1 Componente de Listado Completo

```csharp
@page "/alumnos"
@using MiProyecto.Models
@inject ApplicationDbContext _context
@inject NavigationManager Navigation

<h3>Listado de Alumnos</h3>

<a href="/alumnos/nuevo" class="btn btn-primary mb-3">
    <span class="oi oi-plus"></span> Nuevo Alumno
</a>

@if (alumnos == null)
{
    <p><em>Cargando...</em></p>
}
else if (!alumnos.Any())
{
    <div class="alert alert-info">No hay alumnos registrados.</div>
}
else
{
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Legajo</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Email</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var alumno in alumnos)
            {
                <tr>
                    <td>@alumno.Legajo</td>
                    <td>@alumno.Nombre</td>
                    <td>@alumno.Apellido</td>
                    <td>@alumno.Email</td>
                    <td>
                        <a href="@($"/alumnos/editar/{alumno.Id}")" class="btn btn-sm btn-info">
                            Editar
                        </a>
                        <button class="btn btn-sm btn-danger" @onclick="() => EliminarAlumno(alumno.Id)">
                            Eliminar
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private List<Alumno>? alumnos;

    protected override async Task OnInitializedAsync()
    {
        await CargarAlumnos();
    }

    private async Task CargarAlumnos()
    {
        alumnos = await _context.Alumnos.ToListAsync();
    }

    private async Task EliminarAlumno(int id)
    {
        var alumno = await _context.Alumnos.FindAsync(id);
        if (alumno != null)
        {
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            await CargarAlumnos(); // Recargar lista
        }
    }
}
```

---

## 11. Inyección de Dependencias

Blazor utiliza el sistema de inyección de dependencias de .NET para gestionar servicios.

### 11.1 Registro de Servicios (Program.cs)

```csharp
var builder = WebApplication.CreateBuilder(args);

// Servicios de Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios personalizados
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddSingleton<IConfiguracion, Configuracion>();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();
```

### 11.2 Uso en Componentes

```csharp
@inject ApplicationDbContext _context
@inject NavigationManager Navigation
@inject IAlumnoService AlumnoService
@inject IJSRuntime JSRuntime

@code {
    protected override async Task OnInitializedAsync()
    {
        // Usar los servicios inyectados
        var alumnos = await AlumnoService.ObtenerTodosAsync();
    }
}
```

---

## 12. Interoperabilidad con JavaScript (JS Interop)

Cuando necesitas usar librerías JavaScript o acceder a APIs del navegador:

### 12.1 Llamar JavaScript desde C#

```csharp
@inject IJSRuntime JSRuntime

<button @onclick="MostrarAlerta">Mostrar Alerta</button>
<button @onclick="ObtenerAnchoPantalla">Obtener Ancho</button>

@code {
    private async Task MostrarAlerta()
    {
        await JSRuntime.InvokeVoidAsync("alert", "¡Hola desde Blazor!");
    }

    private async Task ObtenerAnchoPantalla()
    {
        var ancho = await JSRuntime.InvokeAsync<int>("eval", "window.innerWidth");
        Console.WriteLine($"Ancho: {ancho}px");
    }
}
```

### 12.2 Función JS Personalizada

```html
<!-- wwwroot/index.html o _Host.cshtml -->
<script>
    window.mostrarConfirmacion = (mensaje) => {
        return confirm(mensaje);
    };
    
    window.guardarEnLocalStorage = (clave, valor) => {
        localStorage.setItem(clave, valor);
    };
</script>
```

```csharp
@code {
    private async Task<bool> Confirmar(string mensaje)
    {
        return await JSRuntime.InvokeAsync<bool>("mostrarConfirmacion", mensaje);
    }

    private async Task EliminarConConfirmacion(int id)
    {
        if (await Confirmar("¿Está seguro de eliminar este registro?"))
        {
            await EliminarAlumno(id);
        }
    }
}
```

---

## 13. Resumen de Directivas Razor

| Directiva | Uso |
|-----------|-----|
| `@page` | Define la ruta del componente |
| `@inject` | Inyecta un servicio |
| `@code` | Bloque de código C# |
| `@bind` | Two-way binding |
| `@onclick` | Manejo de eventos |
| `@if / @else` | Renderizado condicional |
| `@foreach` | Iteración |
| `@using` | Importar namespace |
| `@inherits` | Herencia de componente base |
| `@implements` | Implementar interfaz |
| `@attribute` | Aplicar atributos (ej: `[Authorize]`) |