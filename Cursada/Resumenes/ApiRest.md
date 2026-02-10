# Unidad 6: Servicios Web y API REST

## 1. Fundamentos Teóricos: Servicios Web y REST

### ¿Qué es un Servicio Web?

Un servicio web es una pieza de lógica de negocio accesible mediante protocolos de internet estándar. Permite la comunicación e intercambio de datos entre diferentes aplicaciones (interoperabilidad), independientemente del lenguaje de programación o plataforma en la que estén desarrolladas.

Evolución histórica de la comunicación distribuida:

1. **Sockets:** Comunicación de bajo nivel, compleja de implementar.
2. **RPC/RMI:** Llamada a procedimientos remotos.
3. **Servicios Web (SOAP/XML):** Basados en estándares estrictos, seguros pero "pesados" en ancho de banda.
4. **Servicios REST (JSON):** El estándar actual para aplicaciones web y móviles por ser ligero y fácil de consumir.

### Arquitectura REST (Representational State Transfer)

REST no es un protocolo, sino un estilo de arquitectura de software para sistemas hipermedia distribuidos. Una API que cumple con estos principios se denomina **RESTful**.

**Conceptos Clave:**

* **Recurso:** Cualquier información que puede ser nombrada (un usuario, un producto, una imagen).
* **URI (Uniform Resource Identifier):** La dirección única que identifica al recurso (ej. `http://api.tienda.com/productos/1`).
* **Representación:** El formato en el que se transfiere el recurso, comúnmente **JSON** (JavaScript Object Notation) o XML.
* **Stateless (Sin estado):** El servidor no guarda información del estado del cliente entre peticiones. Cada petición debe contener toda la información necesaria para ser procesada.

### Verbos HTTP y Códigos de Estado

REST utiliza el protocolo HTTP para realizar operaciones sobre los recursos. Los verbos HTTP definen la acción a realizar:

* **GET:** Consultar o leer información de un recurso. (No modifica datos).
* **POST:** Crear un nuevo recurso.
* **PUT:** Actualizar un recurso existente (reemplazo completo).
* **DELETE:** Eliminar un recurso.
* **PATCH:** Actualización parcial de un recurso (no mencionado explícitamente pero común en REST).

Códigos de Estado (Status Codes) principales:

* **200 OK:** Petición exitosa.
* **201 Created:** Recurso creado exitosamente (respuesta a POST).
* **204 No Content:** Acción exitosa pero sin contenido a devolver (común en DELETE).
* **400 Bad Request:** La petición del cliente está mal formada.
* **404 Not Found:** El recurso no existe.
* **500 Internal Server Error:** Error del lado del servidor.

---

## 2. Creación de una API REST con .NET Core

### Configuración del Proyecto

Para crear una API en Visual Studio 2022:

1. Seleccionar "Crear un nuevo proyecto".
2. Elegir la plantilla **ASP.NET Core Web API**.
3. Configurar el nombre y ubicación.
4. Asegurarse de marcar "Habilitar soporte para OpenAPI" (esto integra Swagger) y "Usar controladores".

### Estructura del Proyecto

* **Carpetas:**
  * `Controllers`: Contiene las clases que manejan las peticiones HTTP.
  * `Properties`: Configuración de lanzamiento (`launchSettings.json`).
* **Archivos:**
  * `Program.cs`: Punto de entrada, configuración de servicios e inyección de dependencias.
  * `appsettings.json`: Configuraciones de la aplicación (cadenas de conexión, claves, etc.).
  * `WeatherForecast.cs`: Clase modelo de ejemplo que viene por defecto.

### Implementación del Controlador

Un controlador en una API REST es una clase que hereda de `ControllerBase` y está decorada con atributos específicos.

**Atributos Esenciales:**

* `[ApiController]`: Habilita comportamientos específicos de API (como validación automática de modelos).
* `[Route("api/[controller]")]`: Define la ruta base. `[controller]` se sustituye por el nombre de la clase sin el sufijo "Controller".

**Ejemplo de Controlador:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    // Método GET: api/usuarios
    [HttpGet]
    public IEnumerable<Usuario> Get()
    {
        // Lógica para obtener usuarios
        return listaDeUsuarios;
    }

    // Método GET con parámetro: api/usuarios/5
    [HttpGet("{id}")]
    public ActionResult<Usuario> Get(int id)
    {
        var usuario = BuscarUsuario(id);
        if (usuario == null) return NotFound(); // Retorna 404
        return usuario; // Retorna 200 con el objeto
    }

    // Método POST: api/usuarios
    [HttpPost]
    public ActionResult Post([FromBody] Usuario nuevoUsuario)
    {
        // Lógica para guardar
        return Ok(); // Retorna 200
    }
}

```

### Swagger / OpenAPI

Es una herramienta integrada por defecto en las plantillas modernas de .NET (biblioteca *Swashbuckle*).

* **Función:** Genera automáticamente documentación interactiva para la API.
* **Uso:** Al ejecutar el proyecto (F5), se abre una página web donde se listan todos los endpoints, permitiendo probarlos directamente desde el navegador sin necesidad de crear un cliente externo.

---

## 3. Consumo de API REST con .NET (`HttpClient`)

Para interactuar con una API desde una aplicación .NET (ya sea de Consola o WinForms), se utiliza la clase `HttpClient` (del espacio de nombres `System.Net.Http`). Esta clase permite enviar peticiones HTTP y recibir respuestas de recursos identificados por URIs.

### 3.1. Conceptos Fundamentales

#### Programación Asíncrona (`async` / `await`)

Las operaciones de red son lentas comparadas con el procesamiento local. Para evitar que la aplicación se "congele" mientras espera la respuesta del servidor, se utiliza programación asíncrona.

* **`async`**: Modificador que se coloca en la firma del método para indicar que contiene operaciones asíncronas.
* **`await`**: Operador que pausa la ejecución del método hasta que la tarea asíncrona (ej. descargar datos) termine, devolviendo el control al hilo principal mientras tanto.
* **Task**: Representa una operación asíncrona que devolverá un valor en el futuro.

#### Serialización y Deserialización JSON

La API envía y recibe datos en formato JSON (texto). Para usarlos en C#, debemos convertirlos a objetos (Clases).

* **Deserializar:** Convertir JSON → Objeto C# (Lectura/GET).
* **Serializar:** Convertir Objeto C# → JSON (Envío/POST/PUT).
* **Librerías:** Se usa comúnmente `Newtonsoft.Json` (Json.NET) o la nativa `System.Text.Json`.

### 3.2. Operaciones Principales

#### Configuración Inicial

Se debe instanciar `HttpClient` y configurar la dirección base.

```csharp
HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7001/"); // URL de tu API
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

```

#### GET (Consultar Datos)

Se utiliza el método `GetAsync`.

```csharp
// Ejemplo: Obtener lista de usuarios
HttpResponseMessage response = await client.GetAsync("api/usuarios");
if (response.IsSuccessStatusCode)
{
    var jsonString = await response.Content.ReadAsStringAsync();
    var listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(jsonString);
    // Usar la lista...
}

```

#### POST (Enviar Datos)

Se utiliza `PostAsync`. Requiere serializar el objeto a enviar.

```csharp
Usuario nuevoUsuario = new Usuario { Nombre = "Juan", Email = "juan@mail.com" };
// Serializar a JSON
var jsonContent = new StringContent(
    JsonConvert.SerializeObject(nuevoUsuario), 
    Encoding.UTF8, 
    "application/json");

// Enviar
HttpResponseMessage response = await client.PostAsync("api/usuarios", jsonContent);

```

#### DELETE (Eliminar Datos)

Se utiliza `DeleteAsync` enviando el ID en la URL.

```csharp
int idEliminar = 5;
HttpResponseMessage response = await client.DeleteAsync($"api/usuarios/{idEliminar}");

```

---

## 4. Integración con Windows Forms (Cliente de Escritorio)

Al consumir una API desde WinForms, el desafío principal es mantener la interfaz responsiva.

1. **Eventos Asíncronos:** Los manejadores de eventos de los botones deben marcarse como `async void`.
```csharp
private async void btnConsultar_Click(object sender, EventArgs e) 
{
    await ConsultarApi();
}

```


2. **Binding (Enlace de Datos):** Una vez deserializada la respuesta JSON a una `List<Usuario>`, se puede asignar directamente al `DataSource` de una grilla.
```csharp
dgvUsuarios.DataSource = listaUsuarios; // La grilla se llena automáticamente

```


3. **Manejo de Excepciones:** Es vital envolver las llamadas `await` en bloques `try-catch` para capturar errores de conexión (ej. API caída) y mostrar un `MessageBox` al usuario en lugar de que la app se cierre.

---

## 5. Configuración del Entorno de Desarrollo

### 5.1. Configuración de la Solución (Ejecución Múltiple)

Para consumir una API, necesitas que tu API (Servidor) esté corriendo al mismo tiempo que tu Cliente (Consola o WinForms). Por defecto, Visual Studio solo inicia un proyecto.

**Concepto:** En una arquitectura Cliente-Servidor local, ambos procesos deben estar activos para comunicarse.

**Configuración en Visual Studio:**

1. Clic derecho sobre la **Solución** (el nodo raíz en el Explorador de Soluciones) → **Propiedades**.
2. Seleccionar **"Proyectos de inicio múltiples"**.
3. Establecer la "Acción" en **Iniciar** tanto para el proyecto de la API como para el proyecto Cliente.
4. Esto asegura que al presionar F5, se levanten ambos sistemas y puedan "hablar" entre sí.

### 5.2. Swagger (OpenAPI) como Herramienta de Prueba

Antes de construir cualquier cliente, se utiliza Swagger para validar la API.

* **¿Qué es?** Una interfaz gráfica web que se genera automáticamente leyendo tu código.
* **Utilidad:** Permite enviar peticiones HTTP (GET, POST, etc.) a tu API sin escribir una sola línea de código de cliente.
* **Uso:**
1. Ejecutas la API.
2. En la web que se abre, despliegas un endpoint (ej. `GET /api/personas`).
3. Haces clic en **"Try it out"** y luego en **"Execute"**.
4. Swagger muestra el **Curl**, la **URL de la petición** y el **Body de la respuesta**. Esto es vital para verificar que tu lógica de servidor funciona antes de pelearte con el código del cliente.

### 5.3. Detalles sobre Serialización JSON

Es importante considerar la coincidencia de nombres entre C# y JSON.

**Problema:** C# usa convención *PascalCase* (ej. `NombreUsuario`), mientras que JSON en APIs suele usar *camelCase* (ej. `nombreUsuario`).
**Solución:**

* Las librerías modernas (`System.Text.Json` o `Newtonsoft`) suelen ser **insensibles a mayúsculas/minúsculas** (case-insensitive) por defecto al deserializar, lo que facilita la tarea.
* **Atributos:** Si los nombres son muy distintos, puedes decorar tus propiedades en la clase Modelo:
```csharp
[JsonProperty("user_name")] // Si viene de Newtonsoft
public string NombreUsuario { get; set; }

```



### 5.4. `BindingSource` en Windows Forms

Para mostrar los datos en la grilla de forma ordenada, es buena práctica usar un intermediario en lugar de asignar la lista directamente al `DataGridView`.

**Concepto:** `BindingSource` actúa como un intermediario entre los datos (Lista de usuarios) y el control visual (Grilla). Facilita el filtrado, la navegación y la actualización de la vista.

**Implementación:**

```csharp
// 1. Obtener datos
var lista = await ObtenerUsuariosApi();

// 2. Usar BindingSource
BindingSource bs = new BindingSource();
bs.DataSource = lista;

// 3. Asignar a la grilla
dgvUsuarios.DataSource = bs;
```

*Esto ayuda a que, si modificas la lista subyacente o quieres refrescar, el control responda mejor.*

### 5.5. Manejo de Puertos (Localhost)

Un error común es que el cliente intente conectarse a un puerto incorrecto.

* Al iniciar la API, fíjate en la barra de direcciones del navegador o en la configuración de Swagger (ej. `https://localhost:7152`).
* Ese **mismo puerto** debe ser el que configures en el `BaseAddress` de tu `HttpClient` en el proyecto de escritorio. Si no coinciden, recibirás un error de conexión (`System.Net.Http.HttpRequestException`).

### 5.6. Ejemplo de Código Robusto

Es útil crear un método genérico o reutilizable para las peticiones.

**Ejemplo de patrón para GET:**

```csharp
private async Task CargarGrilla()
{
    try 
    {
        // Bloque Try-Catch es vital en llamadas de red
        HttpResponseMessage response = await client.GetAsync("api/personas");
        
        if (response.IsSuccessStatusCode)
        {
            var contenido = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Persona>>(contenido);
            dgvPersonas.DataSource = lista;
        }
        else
        {
            MessageBox.Show($"Error: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("No se pudo conectar con el servidor: " + ex.Message);
    }
}

```

---

## 6. Arquitectura y Buenas Prácticas

### 6.1. Biblioteca de Clases Compartida (Refactoring)

Para evitar duplicar código, la clase Modelo (ej. `Alumno` o `Usuario`) no debe escribirse dos veces (una en la API y otra en el Cliente).

**Solución:** Crear un tercer proyecto del tipo **Class Library (Biblioteca de Clases)** llamado generalmente `Dominio` o `Entidades`.

**Pasos de implementación:**

1. Crear el proyecto `Dominio` (Biblioteca de Clases .NET Standard).
2. Mover la clase `Alumno.cs` a este proyecto.
3. En la API y en el WinForms, agregar una **Referencia de Proyecto** hacia `Dominio`.
4. Esto garantiza que si cambias un dato del modelo, se actualiza en ambos lados automáticamente.

**Estructura de la Solución:**

```
MiSolucion/
├── Dominio/              (Class Library)
│   └── Alumno.cs
├── MiApi/                (ASP.NET Core Web API)
│   └── Controllers/
└── MiClienteWinForms/    (Windows Forms)
    └── Form1.cs
```

### 6.2. Validaciones con DataAnnotations

Las clases del modelo pueden decorarse con atributos para validar datos automáticamente antes de ser procesados por la API.

**Namespace requerido:** `System.ComponentModel.DataAnnotations`

**Atributos principales:**

* `[Key]`: Indica que la propiedad es la Clave Primaria (PK).
* `[Required]`: No permite valores nulos.
* `[StringLength(n)]`: Limita el texto a n caracteres.
* `[Range(min, max)]`: Define un rango numérico válido.
* `[EmailAddress]`: Valida formato de email.

**Ejemplo de Modelo con Validaciones:**

```csharp
using System.ComponentModel.DataAnnotations;

public class Alumno 
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string ApellidoNombre { get; set; }

    [Required]
    [StringLength(20)]
    public string DNI { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
```

### 6.3. Capa de Negocio en el Cliente

Para mayor organización y mantenibilidad, se recomienda separar la lógica de acceso a la API en una capa intermedia.

**Estructura sugerida:**

* **Proyecto Entidades:** Contiene las clases del modelo (`Alumno`).
* **Proyecto Negocio:** Contiene la lógica del `HttpClient` (métodos `Get`, `Post`, `Delete`).
* **Proyecto Cliente (Form):** El formulario llama a `Negocio`, y `Negocio` llama a la `API`.

**Ventaja:** Si el día de mañana cambias la API, solo modificas la capa de Negocio sin afectar los formularios.

**Ejemplo de clase en capa Negocio:**

```csharp
public class AlumnoNegocio
{
    private HttpClient client;

    public AlumnoNegocio()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7001/");
    }

    public async Task<List<Alumno>> ObtenerTodos()
    {
        var response = await client.GetAsync("api/alumnos");
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Alumno>>(json);
    }

    public async Task<bool> Agregar(Alumno alumno)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(alumno),
            Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/alumnos", content);
        return response.IsSuccessStatusCode;
    }
}
```

### 6.4. Persistencia: Memoria vs. Base de Datos

Los laboratorios iniciales usan una `static List<Alumno>` en memoria para simplificar. En un entorno real, la API conecta con una base de datos.

**Almacenamiento en memoria (solo para pruebas):**

```csharp
public static List<Alumno> listaAlumnos = new List<Alumno>();
```

**Almacenamiento en Base de Datos (producción):**

Se utiliza **Entity Framework** o **ADO.NET** para conectar con SQL Server.

**Script SQL de ejemplo:**

```sql
CREATE TABLE Alumnos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DNI VARCHAR(20) NOT NULL,
    ApellidoNombre VARCHAR(50) NOT NULL,
    Email VARCHAR(100)
);
```

### 6.5. Paquetes NuGet Requeridos

Dependiendo de la versión de .NET, puede ser necesario instalar paquetes adicionales:

| Paquete | Uso | Comando NuGet |
|---------|-----|---------------|
| `Newtonsoft.Json` | Serialización JSON | `Install-Package Newtonsoft.Json` |
| `System.Text.Json` | Serialización JSON (nativo en .NET Core 3+) | Incluido por defecto |
| `Microsoft.AspNet.WebApi.Client` | Métodos de extensión como `PostAsJsonAsync` | `Install-Package Microsoft.AspNet.WebApi.Client` |

**Nota:** En versiones modernas de .NET (5+), `System.Text.Json` viene incluido. Verifica si tu profesor exige usar `Newtonsoft.Json` específicamente.