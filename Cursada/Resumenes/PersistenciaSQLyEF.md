# Acceso a Datos y Persistencia en .NET

Este apunte consolida los conceptos fundamentales de acceso a datos utilizando tecnologías Microsoft, desde la persistencia básica en archivos, pasando por ADO.NET, hasta el enfoque moderno de ORM con Entity Framework Core.

---

## 1. Persistencia en Archivos (TXT y XML)

En .NET, la persistencia básica antes de llegar a las bases de datos relacionales se maneja a través del espacio de nombres `System.IO` para texto plano y `System.Xml` para estructuras jerárquicas.

### 1.1 Archivos de Texto Plano (.txt)

Para manipular archivos de texto, utilizamos un flujo (Stream) que conecta nuestra aplicación con el archivo físico en el disco.

#### Clases Principales (`System.IO`)

* **FileStream:** Proporciona el flujo de bytes para leer o escribir en el archivo. Define el modo de apertura (`Open`, `Create`, `Append`) y el acceso (`Read`, `Write`).
* **StreamWriter:** Se utiliza para escribir caracteres en el flujo en una codificación específica.
* **StreamReader:** Se utiliza para leer caracteres desde el flujo.

#### Escritura de Archivos

Se debe instanciar el `FileStream` definiendo si se creará un archivo nuevo o se agregará contenido al final (`FileMode.Append`).

```csharp
// 1. Crear el Stream (Flujo) al archivo
// FileMode.Append: Agrega al final. FileMode.Create: Sobrescribe.
FileStream fs = new FileStream("agenda.txt", FileMode.Append, FileAccess.Write);

// 2. Crear el escritor (Wrapper sobre el stream)
StreamWriter sw = new StreamWriter(fs);

// 3. Escribir líneas
sw.WriteLine("Juan;Perez;123456");
sw.WriteLine("Ana;Gomez;654321");

// 4. Cerrar recursos (Importante para liberar el archivo)
sw.Close();
fs.Close();
```

#### Lectura de Archivos

La lectura se realiza típicamente línea por línea dentro de un bucle hasta llegar al final del archivo.

```csharp
FileStream fs = new FileStream("agenda.txt", FileMode.Open, FileAccess.Read);
StreamReader sr = new StreamReader(fs);

// Leemos línea por línea hasta que no haya más datos (null)
while (sr.Peek() > -1)
{
    string linea = sr.ReadLine();
    Console.WriteLine(linea);
    // Aquí se suele hacer un .Split(';') para procesar los datos
}

sr.Close();
fs.Close();
```

### 1.2 Archivos XML (Enfoque Estructurado)

XML (eXtensible Markup Language) permite guardar datos con estructura jerárquica. Existen dos formas de trabajarlos: el enfoque de bajo nivel (escribiendo nodos) y el enfoque de alto nivel (integración con ADO.NET).

#### Enfoque de Bajo Nivel (`System.Xml`)

Se utiliza para escribir o leer el XML nodo por nodo manualmente.

**Escritura con `XmlTextWriter`:**

```csharp
XmlTextWriter xmlWriter = new XmlTextWriter("agenda.xml", System.Text.Encoding.UTF8);
xmlWriter.Formatting = Formatting.Indented;

xmlWriter.WriteStartDocument(true);
xmlWriter.WriteStartElement("Empleados");

    xmlWriter.WriteStartElement("Empleado");
    xmlWriter.WriteAttributeString("id", "1");
    
        xmlWriter.WriteStartElement("Nombre");
        xmlWriter.WriteString("Juan Perez");
        xmlWriter.WriteEndElement();
        
        xmlWriter.WriteStartElement("Sueldo");
        xmlWriter.WriteString("5000");
        xmlWriter.WriteEndElement();

    xmlWriter.WriteEndElement();

xmlWriter.WriteEndElement();
xmlWriter.WriteEndDocument();
xmlWriter.Close();
```

**Lectura con `XmlTextReader`:**

```csharp
XmlTextReader xmlReader = new XmlTextReader("agenda.xml");
while (xmlReader.Read())
{
    if (xmlReader.NodeType == XmlNodeType.Element)
    {
        Console.WriteLine("Elemento: " + xmlReader.Name);
        if (xmlReader.HasAttributes)
        {
            Console.WriteLine("Atributo Id: " + xmlReader.GetAttribute("id"));
        }
    }
}
xmlReader.Close();
```

#### Enfoque de Alto Nivel (ADO.NET - XML)

Permite persistir un `DataTable` o `DataSet` completo en XML con una sola línea de código.

* **WriteXml:** Vuelca el contenido de las filas del DataTable a un archivo XML.
* **WriteXmlSchema:** Guarda la estructura (columnas y tipos de datos) en un archivo `.xsd`.
* **ReadXml:** Carga datos desde un XML directamente a un DataTable.

```csharp
// Persistir un DataTable a XML
DataTable dt = new DataTable("Agenda");
// ... (Se definen columnas y agregan filas) ...

dt.WriteXml("agenda_datos.xml");
dt.WriteXmlSchema("agenda_schema.xsd");

// Recuperar datos desde XML a un DataTable
DataTable dtNuevo = new DataTable();
dtNuevo.ReadXmlSchema("agenda_schema.xsd");
dtNuevo.ReadXml("agenda_datos.xml");
```

---

## 2. Microsoft SQL Server

Es un sistema de gestión de bases de datos relacionales (RDBMS) desarrollado por Microsoft. En el entorno .NET, es el motor de base de datos por excelencia.

### 2.1 Componentes Principales

1. **Motor de Base de Datos:** El servicio central que almacena, procesa y asegura los datos. Existen varias ediciones, siendo **SQL Server Express** y **LocalDB** las más comunes para desarrollo por ser gratuitas y ligeras.
2. **SQL Server Management Studio (SSMS):** Es el entorno de desarrollo integrado (IDE) para administrar la infraestructura de SQL. Permite configurar, supervisar y administrar instancias de SQL Server, así como escribir y ejecutar consultas T-SQL.
3. **Proveedor de Datos (Data Provider):** Para conectar una aplicación .NET con SQL Server se requiere la librería `Microsoft.Data.SqlClient` (disponible vía NuGet).

---

## 3. ADO.NET (ActiveX Data Objects .NET)

ADO.NET es el conjunto de clases base en el .NET Framework para el acceso a datos. Permite conectar aplicaciones a diversas fuentes (SQL Server, OLE DB, ODBC, XML).

### 3.1 Arquitectura y Espacios de Nombres

Se basa en **Data Providers**. Los namespaces principales son:

* `System.Data`: Clases generales (DataSet, DataTable).
* `System.Data.SqlClient`: Específico para SQL Server (SqlConnection, SqlCommand).

### 3.2 Objetos Fundamentales

#### Connection (`SqlConnection`)

Establece la comunicación física ("el puente") con la base de datos.

* **ConnectionString:** Cadena con parámetros críticos (Server, Database, User/Password o Integrated Security).
* **Métodos:** `Open()`, `Close()`. *Tip: Siempre usar bloques `using` para asegurar el cierre de la conexión.*

```csharp
using (SqlConnection conn = new SqlConnection("Server=.;Database=MiBase;Integrated Security=True"))
{
    conn.Open();
    // Operaciones...
} // Aquí se cierra automáticamente (Dispose)
```

#### Command (`SqlCommand`)

Representa una instrucción SQL (SELECT, INSERT, UPDATE, DELETE) o un Procedimiento Almacenado.

* **ExecuteReader:** Para consultas que devuelven filas (SELECT).
* **ExecuteScalar:** Para consultas que devuelven un solo valor (ej: `COUNT`, `SUM`).
* **ExecuteNonQuery:** Para acciones que no devuelven datos (INSERT, UPDATE, DELETE).

#### DataReader (`SqlDataReader`)

Es un flujo de datos **conectado**, de **solo lectura** y **solo avance** (forward-only).

* **Ventaja:** Muy rápido y bajo consumo de memoria.
* **Desventaja:** Mantiene la conexión ocupada mientras se lee.

```csharp
using (SqlDataReader reader = cmd.ExecuteReader())
{
    while (reader.Read())
    {
        Console.WriteLine(reader["NombreColumna"].ToString());
    }
}
```

#### DataAdapter y DataSet (Modo Desconectado)

Diseñado para trabajar sin conexión persistente.

1. **DataAdapter:** Es el intermediario. Usa `Fill()` para traer datos de la BD a memoria y `Update()` para enviar cambios de memoria a la BD.
2. **DataSet / DataTable:** Representación de datos en memoria. Un `DataSet` puede contener múltiples `DataTable` y relaciones.

```csharp
DataTable tabla = new DataTable();
SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Clientes", conn);
adapter.Fill(tabla); // Abre conexión, trae datos, cierra conexión.

foreach (DataRow row in tabla.Rows)
{
    Console.WriteLine(row["Nombre"]);
}
```

### 3.3 Uso de Parámetros (Evitar Inyección SQL)

Nunca debes concatenar cadenas directamente en la consulta SQL. ADO.NET usa la colección `Parameters` para prevenir ataques de inyección SQL.

```csharp
using (SqlConnection conn = new SqlConnection("CadenaDeConexion"))
{
    string sql = "SELECT * FROM Alumnos WHERE Legajo = @Legajo AND Activo = @Activo";
    SqlCommand cmd = new SqlCommand(sql, conn);

    // Forma explícita (Recomendada)
    cmd.Parameters.Add("@Legajo", SqlDbType.VarChar).Value = "A-123";
    
    // Forma corta
    cmd.Parameters.AddWithValue("@Activo", true);

    conn.Open();
    using (SqlDataReader reader = cmd.ExecuteReader())
    {
        // ... lectura ...
    }
}
```

### 3.4 Ejecución de Procedimientos Almacenados

En entornos empresariales, a menudo se ejecutan Stored Procedures (SP) en lugar de SQL directo.

```csharp
using (SqlConnection conn = new SqlConnection("..."))
{
    SqlCommand cmd = new SqlCommand();
    cmd.Connection = conn;
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "sp_ObtenerPromedioAlumno";

    // Parámetro de Entrada
    cmd.Parameters.AddWithValue("@AlumnoId", 5);

    // Parámetro de Salida
    SqlParameter paramSalida = new SqlParameter("@Promedio", SqlDbType.Decimal);
    paramSalida.Direction = ParameterDirection.Output;
    cmd.Parameters.Add(paramSalida);

    conn.Open();
    cmd.ExecuteNonQuery();

    decimal promedio = (decimal)cmd.Parameters["@Promedio"].Value;
    Console.WriteLine($"El promedio es: {promedio}");
}
```

### 3.5 Transacciones (`TransactionScope`)

Cuando necesitas que varias operaciones se completen con éxito o ninguna (atomicidad), usas transacciones.

```csharp
using System.Transactions;

public void TransferirCurso(int alumnoId, int cursoOrigenId, int cursoDestinoId)
{
    using (TransactionScope scope = new TransactionScope())
    {
        using (SqlConnection conn = new SqlConnection("..."))
        {
            conn.Open();

            SqlCommand cmd1 = new SqlCommand("DELETE FROM Inscripciones WHERE...", conn);
            cmd1.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("INSERT INTO Inscripciones VALUES...", conn);
            cmd2.ExecuteNonQuery();
        }

        // Si llegamos aquí sin errores, confirmamos la transacción
        scope.Complete(); 
    }
}
```

---

## 4. Entity Framework Core (ORM)

Entity Framework (EF) Core es un **Mapeador Objeto-Relacional (ORM)**. Permite a los desarrolladores trabajar con una base de datos utilizando objetos .NET (clases), eliminando la necesidad de escribir la mayor parte del código de acceso a datos SQL.

### 4.1 Paquetes NuGet Necesarios

Para trabajar con EF Core en Visual Studio 2022, instalar los siguientes paquetes:

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools (para migraciones)
```

### 4.2 Concepto Clave: Code First

En lugar de diseñar primero la base de datos y luego las clases, en **Code First** escribimos las clases (Entidades) en C# y EF Core genera la base de datos automáticamente.

### 4.3 Componentes Principales

#### Entidades (Entities)

Son clases simples (POCOs) que representan tablas. Las propiedades representan columnas.

```csharp
public class Alumno
{
    public int Id { get; set; } // EF asume que "Id" es Primary Key
    public string Nombre { get; set; }
    public string Legajo { get; set; }
    
    // Propiedad de navegación (Relación)
    public List<Curso> Cursos { get; set; }
}
```

#### DbContext

Es la clase más importante. Representa una sesión con la base de datos y actúa como una unidad de trabajo (**Unit of Work**).

```csharp
public class UniversidadContext : DbContext
{
    public DbSet<Alumno> Alumnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=UniversidadDB;Trusted_Connection=True;");
    }
}
```

#### DbSet<T>

Representa la colección de todas las entidades de un tipo específico en el contexto. Permite operaciones como `Add`, `Remove`, `Find`, y consultas LINQ.

### 4.4 Configuración del Modelo (Mapeo)

EF Core necesita saber cómo traducir las clases a tablas. Existen dos formas:

#### Data Annotations

Atributos decorativos sobre las propiedades de la clase.

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Alumno
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }
    
    [Column("NumeroLegajo")]
    [StringLength(10)]
    public string Legajo { get; set; }
    
    [NotMapped] // Esta propiedad no se guarda en la BD
    public string NombreCompleto => $"{Nombre} ({Legajo})";
}
```

Atributos más comunes:
* `[Key]`: Define la clave primaria.
* `[Required]`: Campo NOT NULL.
* `[MaxLength(50)]` / `[StringLength(50)]`: Longitud máxima de cadena.
* `[Column("NombreColumna")]`: Nombre personalizado de columna.
* `[Table("NombreTabla")]`: Nombre personalizado de tabla.
* `[NotMapped]`: Ignora la propiedad en la BD.
* `[ForeignKey("PropiedadId")]`: Define clave foránea.

#### Fluent API

Configuración mediante código en el método `OnModelCreating` del `DbContext`. Es más potente y mantiene las clases de entidad "limpias".

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Alumno>(entity =>
    {
        entity.ToTable("Alumnos_Sistema");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Nombre)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("Nombre_Completo");

        entity.Property(e => e.Legajo)
              .IsRequired()
              .HasMaxLength(10);

        // Relación 1 a 1
        entity.HasOne(a => a.Direccion)
              .WithOne(d => d.Alumno)
              .HasForeignKey<Direccion>(d => d.AlumnoId);
    });
}
```

### 4.5 Operaciones CRUD con EF Core

#### Crear (Insert)

El estado de la entidad pasa a `Added`. Al guardar, se genera un `INSERT`.

```csharp
using (var context = new UniversidadContext())
{
    var alumno = new Alumno { Nombre = "Juan", Legajo = "A001" };
    context.Alumnos.Add(alumno);
    context.SaveChanges();
}
```

#### Leer (Select) usando LINQ

```csharp
using (var context = new UniversidadContext())
{
    // Todos los alumnos
    var todos = context.Alumnos.ToList();
    
    // Con filtro (WHERE)
    var lista = context.Alumnos
                       .Where(a => a.Nombre.Contains("Juan"))
                       .ToList();
    
    // Buscar por ID
    var alumno = context.Alumnos.Find(1);
    
    // Primera coincidencia o null
    var primero = context.Alumnos.FirstOrDefault(a => a.Legajo == "A001");
}
```

#### Actualizar (Update)

EF trackea los cambios de los objetos recuperados. Si modificas una propiedad y guardas, detecta el estado `Modified` y genera un `UPDATE`.

```csharp
var alumno = context.Alumnos.Find(1);
if (alumno != null)
{
    alumno.Nombre = "Juan Actualizado";
    context.SaveChanges();
}
```

#### Eliminar (Delete)

Se marca la entidad como `Deleted`.

```csharp
var alumno = context.Alumnos.Find(1);
if (alumno != null)
{
    context.Alumnos.Remove(alumno);
    context.SaveChanges();
}
```

### 4.6 Relaciones entre Entidades

#### Uno a Muchos (1:N)

```csharp
public class Curso
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    
    // Colección de navegación
    public List<Alumno> Alumnos { get; set; }
}

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    
    // Clave foránea
    public int CursoId { get; set; }
    
    // Propiedad de navegación
    public Curso Curso { get; set; }
}
```

#### Muchos a Muchos (N:M)

EF Core 5+ maneja automáticamente la tabla intermedia:

```csharp
public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Curso> Cursos { get; set; }
}

public class Curso
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Alumno> Alumnos { get; set; }
}
```

### 4.7 Carga de Datos Relacionados

#### Eager Loading (Carga Anticipada)

Usa `.Include()` para traer datos relacionados en una sola consulta.

```csharp
using (var context = new UniversidadContext())
{
    var alumnosConCursos = context.Alumnos
                                  .Include(a => a.Cursos)
                                  .ToList();

    foreach (var alumno in alumnosConCursos)
    {
        Console.WriteLine($"Alumno: {alumno.Nombre} - Cursos: {alumno.Cursos.Count}");
    }
}
```

#### Carga Multinivel con ThenInclude

```csharp
var alumnos = context.Alumnos
                     .Include(a => a.Cursos)
                         .ThenInclude(c => c.Profesor)
                     .ToList();
```

#### Explicit Loading (Carga Explícita)

Carga datos relacionados después de obtener la entidad principal.

```csharp
var alumno = context.Alumnos.Find(1);
context.Entry(alumno).Collection(a => a.Cursos).Load();
```

#### Lazy Loading (Carga Perezosa)

Carga automática al acceder a la propiedad de navegación. Requiere:
1. Instalar `Microsoft.EntityFrameworkCore.Proxies`
2. Configurar: `optionsBuilder.UseLazyLoadingProxies()`
3. Las propiedades de navegación deben ser `virtual`

### 4.8 Herencia en Entity Framework

#### TPH (Table Per Hierarchy) - Por Defecto

Una sola tabla guarda todas las clases de la jerarquía con una columna discriminadora.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Persona>()
        .HasDiscriminator<string>("TipoPersona")
        .HasValue<Alumno>("A")
        .HasValue<Profesor>("P");
}
```

* *Ventaja:* Rápido (sin JOINs).
* *Desventaja:* Muchos campos nulos.

#### TPT (Table Per Type)

Cada clase tiene su propia tabla. Las tablas derivadas tienen FK a la tabla base.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Persona>().ToTable("Personas");
    modelBuilder.Entity<Alumno>().ToTable("Alumnos");
    modelBuilder.Entity<Profesor>().ToTable("Profesores");
}
```

* *Ventaja:* Normalizado.
* *Desventaja:* Lento (requiere JOINs).

#### TPC (Table Per Concrete Class)

Cada clase concreta tiene su propia tabla con todas las columnas (incluyendo las heredadas).

* *Ventaja:* Sin JOINs complejos.
* *Desventaja:* Duplicidad de esquema.

### 4.9 Migraciones

Las migraciones permiten evolucionar el esquema de la BD sin perder datos.

**Comandos en Package Manager Console:**

```powershell
# Crear una migración
Add-Migration NombreMigracion

# Aplicar migraciones pendientes
Update-Database

# Revertir a una migración específica
Update-Database NombreMigracionAnterior

# Generar script SQL
Script-Migration
```

**Comandos con .NET CLI:**

```bash
dotnet ef migrations add NombreMigracion
dotnet ef database update
```

### 4.10 Consultas Avanzadas con LINQ

```csharp
using (var context = new UniversidadContext())
{
    // Ordenamiento
    var ordenados = context.Alumnos
                           .OrderBy(a => a.Nombre)
                           .ThenByDescending(a => a.Id)
                           .ToList();

    // Proyección (SELECT específico)
    var nombres = context.Alumnos
                         .Select(a => new { a.Nombre, a.Legajo })
                         .ToList();

    // Agrupación
    var porCurso = context.Alumnos
                          .GroupBy(a => a.CursoId)
                          .Select(g => new { CursoId = g.Key, Cantidad = g.Count() })
                          .ToList();

    // Paginación
    var pagina = context.Alumnos
                        .Skip(10)
                        .Take(5)
                        .ToList();

    // Any / All
    bool hayAlumnos = context.Alumnos.Any();
    bool todosActivos = context.Alumnos.All(a => a.Activo);
}
```

### 4.11 Transacciones en EF Core

```csharp
using (var context = new UniversidadContext())
using (var transaction = context.Database.BeginTransaction())
{
    try
    {
        context.Alumnos.Add(new Alumno { Nombre = "Juan" });
        context.SaveChanges();

        context.Cursos.Add(new Curso { Nombre = "Programación" });
        context.SaveChanges();

        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}
```

### 4.12 Ejecución de SQL Raw

Cuando necesitas ejecutar SQL directo:

```csharp
// Consultas que devuelven entidades
var alumnos = context.Alumnos
                     .FromSqlRaw("SELECT * FROM Alumnos WHERE Nombre = {0}", "Juan")
                     .ToList();

// Consultas que no devuelven entidades
context.Database.ExecuteSqlRaw("UPDATE Alumnos SET Activo = 1 WHERE Id = {0}", 5);
```

---

## 5. Resumen de Laboratorios

### Laboratorio 1: Archivos TXT y XML

**Enfoque:** Persistencia básica sin ADO.NET.

* **Objetivo:** Crear una aplicación (Agenda) que permita guardar y leer datos de personas sin usar bases de datos.
* **Desarrollo:**
  * Se utiliza `FileStream` y `StreamWriter` para guardar datos en formato CSV.
  * Se utiliza `XmlTextWriter` para generar archivos XML estructurados.
  * Se implementa la lectura con `XmlTextReader` iterando sobre los nodos.

### Laboratorio 2: Archivos TXT y XML con ADO.NET

**Enfoque:** Integración de archivos con objetos de datos en memoria.

* **Objetivo:** Simplificar el manejo de archivos utilizando las capacidades nativas de `DataTable` y `DataSet`.
* **Desarrollo:**
  * Se define la estructura de una tabla (`DataTable`) con columnas y filas.
  * Se utilizan los métodos `.WriteXml()` y `.WriteXmlSchema()` para exportar.
  * Se utiliza `.ReadXml()` para reconstruir el objeto en memoria.

### Laboratorio 3: ADO.NET SQL y DataBinding

**Enfoque:** Transición a Base de Datos (SQL Server).

* **Objetivo:** Reemplazar los archivos planos por un motor de base de datos real.
* **Desarrollo:**
  * Se introduce `SqlConnection` para conectar a SQL Server.
  * Se utiliza `SqlDataAdapter` para llenar el `DataSet` mediante `.Fill()`.
  * Se implementa **DataBinding** para vincular un `DataGridView` con el `DataTable`.

### Laboratorio 4: Entity Framework Core (ABMC/CRUD)

**Enfoque:** Persistencia moderna utilizando ORM.

* **Objetivo:** Crear una capa de persistencia utilizando objetos .NET.
* **Pasos:**
  1. Creación de Proyecto de Consola .NET.
  2. Instalación de NuGet: `Microsoft.EntityFrameworkCore.SqlServer`.
  3. Creación de clase POCO `Alumno`.
  4. Creación de `UniversidadContext` heredando de `DbContext`.
  5. Implementación de operaciones CRUD.
  6. Uso de `EnsureCreated()` para generar la BD automáticamente.