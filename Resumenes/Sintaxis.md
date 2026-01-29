# Apuntes Técnicos: Fundamentos de Desarrollo en .NET y C#

---

## 1. La Plataforma .NET

.NET utiliza un único entorno de ejecución denominado **CLR** (Common Language Runtime). Todos los lenguajes soportados (C#, VB.NET, F#) compilan a un lenguaje intermedio llamado **MSIL** (Microsoft Intermediate Language). Esto garantiza que no existan diferencias de rendimiento significativas entre los lenguajes de la plataforma.

### 1.1. Compilación y Ejecución

```
Código Fuente (.cs) → Compilador C# → MSIL (.dll/.exe) → CLR (JIT) → Código Máquina
```

El **JIT (Just-In-Time)** compila el código MSIL a código máquina nativo en tiempo de ejecución, optimizando para la arquitectura específica del sistema.

---

## 2. Sistema de Tipos

En .NET, todos los tipos heredan directa o indirectamente de `System.Object`.

### 2.1. Tipos por Valor (Value Types)

Contienen directamente sus datos y se almacenan en el **stack**. Incluyen:
- **Primitivos:** `int`, `bool`, `char`, `double`, `float`, `decimal`
- **Estructuras:** `struct`
- **Enumeraciones:** `enum`

### 2.2. Tipos por Referencia (Reference Types)

Almacenan una referencia a la dirección de memoria donde se encuentran los datos (en el **heap**). Incluyen:
- `class`, `string`, `array`, `interface`, `delegate`

### 2.3. Declaración e Inicialización

Toda variable debe ser declarada e inicializada explícitamente antes de su uso.

```csharp
// Declaración explícita
int contador = 0;
string mensaje = "Hola Mundo";
bool esActivo = true;
decimal precio = 99.99m; // 'm' indica decimal

// Inferencia de tipos (var)
// El compilador determina el tipo basándose en el valor asignado
var numero = 666;      // Se compila como int
var texto = "Goodbye"; // Se compila como string
```

### 2.4. Nullable Types

Permiten que tipos por valor acepten `null`:

```csharp
int? edad = null;           // Nullable int
bool? tieneHijos = null;    // Nullable bool

// Operador null-coalescing
int edadReal = edad ?? 0;   // Si edad es null, usa 0
```

### 2.5. Ámbito y Visibilidad (Scope)

El alcance de una variable determina su tiempo de vida y visibilidad. Las variables declaradas dentro de un bloque (por ejemplo, dentro de un `if`) no son accesibles fuera de él.

---

## 3. La Palabra Clave `using`

En C#, `using` tiene dos usos fundamentales:

### 3.1. Como Directiva (Importar Namespaces)

Se coloca al inicio del archivo para no escribir nombres completos de clases:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
```

### 3.2. Como Sentencia (Gestión de Recursos)

Define un ámbito al final del cual el objeto se destruye automáticamente (llama al método `Dispose`). Crucial para recursos no administrados:

```csharp
// Sintaxis clásica
using (StreamReader sr = File.OpenText("data.txt")) 
{
    string s = sr.ReadLine();
    // Al salir, sr se cierra y libera automáticamente
}

// Sintaxis moderna (C# 8+)
using var sr = File.OpenText("data.txt");
string s = sr.ReadLine();
// Se libera al salir del método
```

---

## 4. Operadores

### 4.1. Operadores Aritméticos

| Operador | Descripción | Ejemplo |
|----------|-------------|---------|
| `+` | Suma | `5 + 3 = 8` |
| `-` | Resta | `5 - 3 = 2` |
| `*` | Multiplicación | `5 * 3 = 15` |
| `/` | División | `5 / 3 = 1` (enteros) |
| `%` | Módulo (resto) | `5 % 3 = 2` |
| `++` | Incremento | `i++` o `++i` |
| `--` | Decremento | `i--` o `--i` |

### 4.2. Operadores de Comparación

| Operador | Descripción |
|----------|-------------|
| `==` | Igual a |
| `!=` | Diferente de |
| `>` | Mayor que |
| `<` | Menor que |
| `>=` | Mayor o igual |
| `<=` | Menor o igual |

### 4.3. Operadores Lógicos

| Operador | Descripción | Ejemplo |
|----------|-------------|---------|
| `&&` | AND lógico | `a && b` |
| `\|\|` | OR lógico | `a \|\| b` |
| `!` | NOT lógico | `!a` |

### 4.4. Operador Ternario

```csharp
string resultado = (edad >= 18) ? "Mayor" : "Menor";
```

---

## 5. Estructuras de Control

### 5.1. Estructuras de Decisión

#### If / Else If / Else

```csharp
if (condicion1)
{
    // Código si condicion1 es verdadera
}
else if (condicion2)
{
    // Código si condicion2 es verdadera
}
else
{
    // Código si ninguna condición es verdadera
}
```

#### Switch

Es importante notar que C# es *case-sensitive* (sensible a mayúsculas/minúsculas).

```csharp
string pais = "Argentina";
switch (pais)
{
    case "Argentina":
        Console.WriteLine("América del Sur");
        break; // El break es obligatorio en cada case no vacío
    case "España":
        Console.WriteLine("Europa");
        break;
    default:
        Console.WriteLine("Otro");
        break;
}
```

#### Switch Expression (C# 8+)

```csharp
string continente = pais switch
{
    "Argentina" => "América del Sur",
    "España" => "Europa",
    _ => "Otro" // default
};
```

*Nota técnica:* Para capturar teclas en consola, se utiliza `Console.ReadKey()`, que retorna `ConsoleKeyInfo`. Este contiene el enumerado `ConsoleKey` (ej. `ConsoleKey.Enter`, `ConsoleKey.D1`).

### 5.2. Estructuras de Iteración

#### For

Utilizado cuando se conoce la cantidad de iteraciones:

```csharp
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

#### Foreach

Diseñado para recorrer colecciones. La variable de iteración es de **solo lectura**:

```csharp
string[] nombres = { "Ana", "Luis", "Pedro" };
foreach (string nombre in nombres)
{
    Console.WriteLine(nombre);
}
```

#### While

Ejecuta mientras la condición sea verdadera:

```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    i++;
}
```

#### Do-While

Garantiza **al menos una ejecución**:

```csharp
int i = 0;
do
{
    Console.WriteLine(i);
    i++;
} while (i < 10);
```

---

## 6. Arreglos (Arrays)

### 6.1. Declaración e Inicialización

```csharp
// Declaración con tamaño fijo
string[] telefonos = new string[3];

// Inicialización directa
string[] nombres = { "Juan", "Ana", "Pedro" };

// Inicialización explícita
int[] numeros = new int[] { 1, 5, 8 };

// Arreglos multidimensionales
int[,] matriz = new int[3, 3];
int[,] tablero = { {1, 2, 3}, {4, 5, 6}, {7, 8, 9} };

// Arreglos dentados (jagged arrays)
int[][] jaggedArray = new int[3][];
jaggedArray[0] = new int[] { 1, 2 };
jaggedArray[1] = new int[] { 3, 4, 5 };
```

### 6.2. Propiedades y Métodos Útiles

```csharp
int[] numeros = { 5, 2, 8, 1, 9 };

int longitud = numeros.Length;        // 5
Array.Sort(numeros);                  // Ordena: 1, 2, 5, 8, 9
Array.Reverse(numeros);               // Invierte: 9, 8, 5, 2, 1
int indice = Array.IndexOf(numeros, 5); // Busca el índice de 5
```

---

## 7. Enumeraciones (Enums)

Los `enum` son tipos por valor basados en enteros (por defecto `int`).

### 7.1. Declaración Básica

```csharp
public enum DiaSemana
{
    Lunes,      // 0
    Martes,     // 1
    Miercoles,  // 2
    Jueves,     // 3
    Viernes,    // 4
    Sabado,     // 5
    Domingo     // 6
}

// Uso
DiaSemana hoy = DiaSemana.Lunes;
```

### 7.2. Tipos Subyacentes y Valores Personalizados

```csharp
public enum Meses : byte  // Ahorra memoria
{
    Enero = 1,
    Febrero = 2,
    // ...
}
```

### 7.3. Atributo `[Flags]`

Permite combinaciones de valores usando operaciones de bits:

```csharp
[Flags]
public enum OpcionesAuto
{
    Ninguna = 0,
    TechoSolar = 1,   // 0001
    Aleron = 2,       // 0010
    LucesNiebla = 4   // 0100
}

// Combinar opciones (1 | 2 = 3)
OpcionesAuto miAuto = OpcionesAuto.TechoSolar | OpcionesAuto.Aleron;

// Verificar si tiene una opción
bool tieneTecho = miAuto.HasFlag(OpcionesAuto.TechoSolar); // true
```

---

## 8. Manejo de Errores y Excepciones

Una excepción es una alteración del flujo normal de ejecución.

### 8.1. Bloque Try-Catch-Finally

```csharp
try
{
    // Código propenso a errores
    int divisor = 0;
    int resultado = 10 / divisor;
}
catch (DivideByZeroException ex) // Excepción específica primero
{
    Console.WriteLine("No se puede dividir por cero.");
}
catch (Exception ex) // Excepción general al final
{
    Console.WriteLine($"Error general: {ex.Message}");
}
finally
{
    // Se ejecuta SIEMPRE (ideal para limpieza de recursos)
    Console.WriteLine("Operación finalizada.");
}
```

### 8.2. Lanzar Excepciones

```csharp
if (edad < 0)
{
    throw new ArgumentException("La edad no puede ser negativa", nameof(edad));
}
```

### 8.3. Excepciones Personalizadas

```csharp
public class SaldoInsuficienteException : Exception
{
    public decimal SaldoActual { get; }
    
    public SaldoInsuficienteException(string mensaje, decimal saldo) 
        : base(mensaje)
    {
        SaldoActual = saldo;
    }
}
```

### 8.4. Buenas Prácticas en Excepciones

1. **Captura Específica:** Capturar la excepción más específica posible antes que `Exception`.
2. **No "Tragar" Errores:** Evitar bloques `catch` vacíos.
3. **Propagar con `throw;`:** Usar `throw;` (sin variable) mantiene el *stack trace* original.
4. **Logging:** Registrar las excepciones para análisis posterior.

---

## 9. Programación Orientada a Objetos (POO)

C# es un lenguaje totalmente orientado a objetos con cuatro pilares fundamentales.

### 9.1. Clases y Objetos

```csharp
public class Persona
{
    // Campos privados
    private string _nombre;
    private int _edad;
    
    // Constructor
    public Persona(string nombre, int edad)
    {
        _nombre = nombre;
        _edad = edad;
    }
    
    // Propiedades
    public string Nombre 
    { 
        get { return _nombre; }
        set { _nombre = value; }
    }
    
    // Propiedad auto-implementada
    public string Direccion { get; set; }
    
    // Métodos
    public void Saludar()
    {
        Console.WriteLine($"Hola, soy {_nombre}");
    }
}

// Instanciación
Persona persona = new Persona("Juan", 30);
persona.Saludar();
```

### 9.2. Encapsulamiento

Oculta la complejidad interna mediante modificadores de acceso:

| Modificador | Acceso |
|-------------|--------|
| `public` | Desde cualquier lugar |
| `private` | Solo dentro de la clase |
| `protected` | Clase y clases derivadas |
| `internal` | Dentro del mismo ensamblado |
| `protected internal` | Ensamblado o clases derivadas |

### 9.3. Herencia

Permite crear clases basadas en otras (relación "es un"):

```csharp
public class Persona
{
    public string Nombre { get; set; }
    
    public Persona(string nombre)
    {
        Nombre = nombre;
    }
}

public class Empleado : Persona
{
    public int Legajo { get; set; }
    
    // Llama al constructor de la clase base
    public Empleado(string nombre, int legajo) : base(nombre)
    {
        Legajo = legajo;
    }
}
```

**Tipos de Clases:**
- **Clases Abstractas:** No pueden instanciarse. Pueden contener métodos abstractos (sin implementación).
- **Clases Sealed:** No permiten herencia.
- **Clases Parciales (`partial`):** Permiten dividir la definición en múltiples archivos.
- **Clases Estáticas (`static`):** No se pueden instanciar. Todos sus miembros deben ser estáticos.

### 9.4. Polimorfismo

Capacidad de objetos diferentes de responder al mismo mensaje de formas distintas.

#### Virtual / Override

```csharp
public class Animal
{
    public virtual void HacerSonido()
    {
        Console.WriteLine("...");
    }
}

public class Perro : Animal
{
    public override void HacerSonido()
    {
        Console.WriteLine("Guau!");
    }
}
```

#### Sobrecarga (Overloading)

Mismo nombre de método con diferentes parámetros:

```csharp
public int Sumar(int a, int b) => a + b;
public double Sumar(double a, double b) => a + b;
public int Sumar(int a, int b, int c) => a + b + c;
```

### 9.5. Abstracción e Interfaces

Una **Interfaz** define un contrato sin implementación:

```csharp
public interface ITransportable
{
    void Acelerar();
    void Frenar();
    int VelocidadActual { get; }
}

public class Auto : ITransportable
{
    public int VelocidadActual { get; private set; }
    
    public void Acelerar() 
    { 
        VelocidadActual += 10; 
    }
    
    public void Frenar() 
    { 
        VelocidadActual -= 10; 
    }
}
```

#### Implementación Explícita

Útil cuando una clase implementa múltiples interfaces con métodos del mismo nombre:

```csharp
public class MiClase : IInterfaz1, IInterfaz2
{
    // Implementación explícita
    void IInterfaz1.MiMetodo() { /* ... */ }
    void IInterfaz2.MiMetodo() { /* ... */ }
}

// Uso
MiClase obj = new MiClase();
((IInterfaz1)obj).MiMetodo(); // Accede vía interfaz
```

---

## 10. Colecciones y Genéricos

### 10.1. Colecciones No Genéricas (Legacy)

Pertenecen a `System.Collections`. Almacenan datos como `Object`, lo que requiere *casting* y puede causar errores en tiempo de ejecución:
- `ArrayList`, `Hashtable`, `Queue`, `Stack`

### 10.2. Genéricos (Generics)

Introducen seguridad de tipos y mejor rendimiento al evitar el *boxing/unboxing*:

```csharp
// List<T> - Reemplazo de ArrayList
List<int> numeros = new List<int>();
numeros.Add(10);
numeros.Add(20);
// numeros.Add("Texto"); // Error de compilación

// Dictionary<TKey, TValue> - Reemplazo de Hashtable
Dictionary<string, int> edades = new Dictionary<string, int>();
edades["Juan"] = 30;
edades["Ana"] = 25;

// Otras colecciones genéricas
Queue<string> cola = new Queue<string>();
Stack<string> pila = new Stack<string>();
HashSet<int> conjunto = new HashSet<int>(); // Sin duplicados
```

### 10.3. Métodos Útiles de List<T>

```csharp
List<int> numeros = new List<int> { 5, 2, 8, 1, 9 };

numeros.Add(10);                    // Agregar elemento
numeros.Remove(5);                  // Eliminar por valor
numeros.RemoveAt(0);                // Eliminar por índice
bool existe = numeros.Contains(8); // Verificar existencia
numeros.Sort();                     // Ordenar
numeros.Reverse();                  // Invertir
int cantidad = numeros.Count;       // Cantidad de elementos
```

---

## 11. Sintaxis Avanzada

### 11.1. Inicializadores de Objetos y Colecciones

```csharp
// Inicializador de objeto
var persona = new Persona 
{ 
    Nombre = "Juan", 
    Edad = 30 
};

// Inicializador de colección
var lista = new List<string> { "uno", "dos", "tres" };
```

### 11.2. Tipos Anónimos

Permiten crear objetos sin definir una clase explícita:

```csharp
var personaAnonima = new { Nombre = "Jenny", Edad = 31 };
Console.WriteLine(personaAnonima.Nombre);
```

### 11.3. Expresiones Lambda

Funciones anónimas compactas:

```csharp
// Sintaxis básica
Func<int, int> cuadrado = x => x * x;

// Con múltiples parámetros
Func<int, int, int> suma = (a, b) => a + b;

// Con cuerpo de bloque
Action<string> imprimir = mensaje => 
{
    Console.WriteLine(mensaje);
};
```

### 11.4. Métodos de Extensión

Permiten "agregar" métodos a tipos existentes:

```csharp
public static class StringExtensions
{
    public static int ContarPalabras(this string str)
    {
        return str.Split(' ').Length;
    }
}

// Uso
string texto = "Hola mundo";
int cantidad = texto.ContarPalabras(); // Parece método nativo
```

### 11.5. Interpolación de Strings

```csharp
string nombre = "Juan";
int edad = 30;

// Interpolación (recomendado)
string mensaje = $"Hola {nombre}, tienes {edad} años";

// Formato con decimales
decimal precio = 99.5m;
string precioFormateado = $"Precio: {precio:C2}"; // $99.50
```

---

## 12. LINQ (Language Integrated Query)

Provee una sintaxis unificada para consultar datos de cualquier fuente.

### 12.1. Sintaxis de Consulta (Query)

Similar a SQL:

```csharp
var resultado = from c in clientes
                where c.Provincia == "Santa Fe"
                orderby c.Nombre
                select c;
```

### 12.2. Sintaxis de Métodos (Lambda)

Más común en la práctica:

```csharp
var resultado = clientes
    .Where(c => c.Provincia == "Santa Fe")
    .OrderBy(c => c.Nombre);
```

### 12.3. Operadores LINQ Comunes

```csharp
List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Filtrado
var pares = numeros.Where(n => n % 2 == 0);

// Proyección
var cuadrados = numeros.Select(n => n * n);

// Ordenamiento
var ordenados = numeros.OrderByDescending(n => n);

// Agregación
int suma = numeros.Sum();
double promedio = numeros.Average();
int maximo = numeros.Max();
int cantidad = numeros.Count();

// Búsqueda
int primero = numeros.First(n => n > 5);
int? primerONull = numeros.FirstOrDefault(n => n > 100);
bool existeAlguno = numeros.Any(n => n > 5);
bool todosCumplen = numeros.All(n => n > 0);

// Agrupación
var grupos = clientes.GroupBy(c => c.Provincia);

// Joins
var resultado = from c in clientes
                join p in pedidos on c.Id equals p.ClienteId
                select new { c.Nombre, p.Total };
```

### 12.4. Métodos de String Útiles en LINQ

```csharp
var resultado = clientes
    .Where(c => c.Nombre.StartsWith("A"))
    .Where(c => c.Email.Contains("@gmail"))
    .Where(c => c.Ciudad.ToLower() == "buenos aires");
```

---

## 13. Programación Asíncrona

Esencial para mantener la capacidad de respuesta de la aplicación.

### 13.1. Modelo TAP (Task-based Asynchronous Pattern)

```csharp
public async Task<string> ObtenerDatosAsync()
{
    // await suspende la ejecución sin bloquear el hilo
    string resultado = await httpClient.GetStringAsync("https://api.ejemplo.com");
    return resultado;
}

// Uso
string datos = await ObtenerDatosAsync();
```

### 13.2. Palabras Clave

- **`async`:** Marca un método como asincrónico.
- **`await`:** Suspende la ejecución hasta que la tarea se complete.

### 13.3. Escenarios de Uso

#### I/O-bound (Entrada/Salida)

Operaciones que esperan respuesta externa (red, BD, archivos):

```csharp
// Usar await directamente (NO usar Task.Run)
string contenido = await File.ReadAllTextAsync("archivo.txt");
```

#### CPU-bound (Procesamiento)

Cálculos costosos:

```csharp
// Usar Task.Run para no bloquear el hilo principal
int resultado = await Task.Run(() => CalculoCostoso());
```

### 13.4. Ejecución Paralela

```csharp
// Iniciar tareas sin esperar
var tarea1 = GetUserAsync(1);
var tarea2 = GetUserAsync(2);
var tarea3 = GetUserAsync(3);

// Esperar a que TODAS finalicen
await Task.WhenAll(tarea1, tarea2, tarea3);

// Esperar a que CUALQUIERA finalice
var primeraCompletada = await Task.WhenAny(tarea1, tarea2, tarea3);
```

### 13.5. Cancelación con CancellationToken

```csharp
public async Task ProcesarAsync(CancellationToken token)
{
    for (int i = 0; i < 100; i++)
    {
        // Verifica si se solicitó cancelación
        token.ThrowIfCancellationRequested();
        
        await Task.Delay(100, token);
    }
}

// Uso
var cts = new CancellationTokenSource();

// Cancelar después de 5 segundos
cts.CancelAfter(5000);

try
{
    await ProcesarAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operación cancelada");
}
```

### 13.6. Reporte de Progreso

```csharp
public async Task ProcesarConProgresoAsync(IProgress<int> progreso)
{
    for (int i = 0; i <= 100; i += 10)
    {
        await Task.Delay(100);
        progreso.Report(i);
    }
}

// Uso
var progreso = new Progress<int>(valor => 
{
    Console.WriteLine($"Progreso: {valor}%");
});

await ProcesarConProgresoAsync(progreso);
```

### 13.7. Buenas Prácticas en Asincronía

1. **Sufijo Async:** Nombrar métodos como `MetodoAsync`.
2. **Evitar `async void`:** Solo usarlo en manejadores de eventos. Siempre devolver `Task`.
3. **No bloquear:** Evitar `.Wait()` o `.Result` en código asíncrono.
4. **Todo método `async` debe tener `await`:** Si no tiene, no debería ser async.

### 13.8. Thread.Sleep vs Task.Delay

```csharp
// ❌ MALO - Bloquea el hilo
Thread.Sleep(1000);

// ✅ BUENO - Libera el hilo
await Task.Delay(1000);
```

---

## 14. Buenas Prácticas de Programación

### 14.1. Principios de Clean Code

- **Nombres Significativos:** Variables y métodos deben revelar su intención. Evitar `a`, `b`, `x`, `data`.
- **Métodos Cortos:** Cada método debe tener una única responsabilidad.
- **Código Auto-documentado:** El código debe leerse como un texto narrativo.

### 14.2. Principios Generales

| Principio | Significado | Descripción |
|-----------|-------------|-------------|
| **DRY** | Don't Repeat Yourself | Evitar duplicación de código |
| **KISS** | Keep It Simple, Stupid | Priorizar soluciones simples |
| **YAGNI** | You Aren't Gonna Need It | No implementar funcionalidades basadas en supuestos futuros |

### 14.3. Convenciones de Nomenclatura

| Estilo | Uso | Ejemplo |
|--------|-----|---------|
| **PascalCase** | Clases, Métodos, Propiedades, Interfaces | `CustomerService`, `ProcessPayment` |
| **camelCase** | Variables locales, parámetros | `orderList`, `paymentAmount` |
| **_camelCase** | Campos privados | `_saldo`, `_nombre` |
| **SCREAMING_SNAKE** | Constantes | `MAX_ATTEMPTS` |

**Prefijos y Sufijos:**
- Interfaces: `I` (ej. `IRepository`)
- Métodos Asíncronos: `Async` (ej. `GetUserAsync`)

### 14.4. Documentación XML

```csharp
/// <summary>
/// Calcula el total de una orden incluyendo impuestos.
/// </summary>
/// <param name="subtotal">El monto antes de impuestos.</param>
/// <param name="tasaImpuesto">La tasa de impuesto a aplicar (ej. 0.21).</param>
/// <returns>El total con impuestos incluidos.</returns>
public decimal CalcularTotal(decimal subtotal, decimal tasaImpuesto)
{
    return subtotal * (1 + tasaImpuesto);
}
```

### 14.5. Herramientas de Logging

- **ILogger (Microsoft):** Abstracción nativa de .NET Core.
- **Serilog:** Logging estructurado (JSON).
- **NLog / Log4Net:** Librerías clásicas y robustas.

### 14.6. Arquitectura y Organización

**Estructura de Carpetas Recomendada:**

```
/MiProyecto
    /Controllers    → Controladores de API/Vistas
    /Models         → Modelos de datos
    /Views          → Vistas (UI)
    /Services       → Lógica de negocio
    /Repositories   → Acceso a datos
    /DTOs           → Objetos de transferencia
    /Interfaces     → Contratos
```

**Clean Architecture (Capas):**
- **Domain:** Entidades y lógica de negocio pura.
- **Application:** Casos de uso e interfaces.
- **Infrastructure:** Acceso a datos y servicios externos.
- **Presentation:** Interfaz con el usuario (API/UI).

---

## 15. Clases Útiles del Framework

### 15.1. Entrada/Salida de Consola

```csharp
// Salida
Console.WriteLine("Texto con salto de línea");
Console.Write("Texto sin salto");
Console.WriteLine($"Interpolación: {variable}");

// Entrada
string texto = Console.ReadLine();
ConsoleKeyInfo tecla = Console.ReadKey();
```

### 15.2. Conversiones

```csharp
// Parse (lanza excepción si falla)
int numero = int.Parse("123");

// TryParse (retorna bool)
if (int.TryParse("123", out int resultado))
{
    Console.WriteLine(resultado);
}

// Convert
int entero = Convert.ToInt32("123");
string texto = Convert.ToString(123);
```

### 15.3. Métodos de String

```csharp
string texto = "  Hola Mundo  ";

texto.ToUpper();           // "  HOLA MUNDO  "
texto.ToLower();           // "  hola mundo  "
texto.Trim();              // "Hola Mundo"
texto.Length;              // 14
texto.Contains("Mundo");   // true
texto.StartsWith("Hola");  // false (hay espacios)
texto.Replace("Mundo", "C#"); // "  Hola C#  "
texto.Split(' ');          // ["", "", "Hola", "Mundo", "", ""]
texto.Substring(2, 4);     // "Hola"
```

### 15.4. DateTime

```csharp
DateTime ahora = DateTime.Now;
DateTime hoy = DateTime.Today;
DateTime fecha = new DateTime(2024, 12, 25);

fecha.AddDays(10);
fecha.AddMonths(1);
fecha.Year;    // 2024
fecha.Month;   // 12
fecha.Day;     // 25

string formateada = fecha.ToString("dd/MM/yyyy");
```

### 15.5. Stopwatch (Medición de Rendimiento)

```csharp
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

// Código a medir...

sw.Stop();
Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds} ms");
```

---

## 16. Resumen de Laboratorios

### 16.1. Laboratorios de Sintaxis y Control

| Laboratorio | Temática | Conceptos Clave |
|-------------|----------|-----------------|
| **Lab 01** | Estructuras de Decisión e Iteración | `Console.ReadLine()`, `Console.ReadKey()`, `ConsoleKey`, métodos de String, algoritmos (Fibonacci, años bisiestos, números romanos) |
| **Lab 02** | POO | Clases, propiedades, métodos, herencia, polimorfismo |
| **Lab 03** | LINQ | `Where`, `OrderBy`, `Select`, `StartsWith`, `Contains`, `List<T>` |

### 16.2. Laboratorios de Programación Asíncrona

| Actividad | Temática | Conceptos Clave |
|-----------|----------|-----------------|
| **1. Intro** | `Thread.Sleep` vs `Task.Delay` | Bloqueo vs liberación de hilo, `Stopwatch` |
| **2. Paralela** | `Task.WhenAll` | Ejecución simultánea de múltiples tareas |
| **3. Excepciones** | Manejo de Errores | `try-catch` en métodos async |
| **4. Progreso** | `IProgress<T>` | Notificación de avance en tiempo real |
| **5. Cancelación** | `CancellationToken` | Cancelación cooperativa, `OperationCanceledException` |
| **6. Archivos** | Async I/O | `File.WriteAllTextAsync`, `File.ReadAllTextAsync` |

### 16.3. Algoritmos Comunes

- **Fibonacci:** Sumar los dos números anteriores de una secuencia.
- **Números Primos:** Verificar divisibilidad con operador módulo (`%`).
- **Año Bisiesto:** `(año % 4 == 0 && año % 100 != 0) || (año % 400 == 0)`
- **Triángulos de Asteriscos:** Bucles `for` anidados.