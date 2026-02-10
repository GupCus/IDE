# Apuntes Técnicos: Plataforma .NET y Visual Studio 2022

## Índice

- [Apuntes Técnicos: Plataforma .NET y Visual Studio 2022](#apuntes-técnicos-plataforma-net-y-visual-studio-2022)
  - [Índice](#índice)
  - [1. La Plataforma .NET](#1-la-plataforma-net)
    - [1.1. Componentes de la Arquitectura .NET](#11-componentes-de-la-arquitectura-net)
    - [1.2. Gestión de Memoria e Interoperabilidad](#12-gestión-de-memoria-e-interoperabilidad)
    - [1.3. Ensamblados y Metadatos](#13-ensamblados-y-metadatos)
    - [1.4. El Proceso de Compilación y Ejecución](#14-el-proceso-de-compilación-y-ejecución)
  - [2. Entorno de Desarrollo Integrado: Visual Studio](#2-entorno-de-desarrollo-integrado-visual-studio)
    - [2.1. Organización de Archivos](#21-organización-de-archivos)
    - [2.2. Estructura Física de Directorios](#22-estructura-física-de-directorios)
    - [2.3. Configuraciones de Compilación: Debug vs. Release](#23-configuraciones-de-compilación-debug-vs-release)
    - [2.4. Ventanas Principales](#24-ventanas-principales)
  - [3. Productividad en el Editor de Código](#3-productividad-en-el-editor-de-código)
  - [4. Depuración (Debugging)](#4-depuración-debugging)
    - [4.1. Breakpoints (Puntos de Interrupción)](#41-breakpoints-puntos-de-interrupción)
    - [4.2. Ejecución Paso a Paso](#42-ejecución-paso-a-paso)
    - [4.3. Inspección de Variables](#43-inspección-de-variables)
    - [4.4. Edit and Continue](#44-edit-and-continue)
  - [5. Resumen de Atajos de Teclado](#5-resumen-de-atajos-de-teclado)

---

## 1. La Plataforma .NET

La plataforma .NET es un entorno de ejecución integral que proporciona servicios para el desarrollo y ejecución de aplicaciones. No se limita a un lenguaje específico, sino que soporta múltiples lenguajes (C#, VB.NET, F#) gracias a su arquitectura unificada.

### 1.1. Componentes de la Arquitectura .NET

* **CLR (Common Language Runtime):** Es el motor de ejecución de .NET. Gestiona la memoria, la ejecución de hilos, la seguridad y el manejo de excepciones. Es el encargado de tomar el código intermedio y ejecutarlo en el sistema operativo.
* **MSIL (Microsoft Intermediate Language):** Todos los lenguajes .NET se compilan primero a este lenguaje intermedio, también conocido como CIL (Common Intermediate Language). Esto permite la interoperabilidad entre lenguajes y la independencia de la arquitectura hasta el momento de la ejecución.
* **JIT (Just-In-Time) Compiler:** Es el componente del CLR que compila el código MSIL a código máquina nativo justo antes de ser ejecutado. Existen diferentes tipos (Normal JIT, Econo JIT) optimizados para rendimiento o velocidad de inicio.
* **BCL (Base Class Library):** Conjunto unificado de bibliotecas de clases disponibles para todos los lenguajes .NET. Provee funcionalidades comunes como entrada/salida (I/O), acceso a datos, gráficos, XML y seguridad.

### 1.2. Gestión de Memoria e Interoperabilidad

* **Garbage Collector (GC):** Administra automáticamente la memoria. Asigna memoria para nuevos objetos y libera la memoria de objetos que ya no son utilizados, evitando fugas de memoria (memory leaks) comunes en lenguajes como C++.
* **CTS (Common Type System):** Define cómo se declaran, usan y gestionan los tipos de datos en el runtime, asegurando que un `int` en C# sea compatible con un `Integer` en VB.NET.
* **CLS (Common Language Specification):** Define un subconjunto de reglas que cualquier lenguaje debe seguir para asegurar la interoperabilidad completa con otros componentes .NET.

### 1.3. Ensamblados y Metadatos

* **Ensamblados (Assemblies):** Son las unidades de despliegue en .NET (archivos `.dll` o `.exe`). Contienen tres elementos inseparables:
  1. Código MSIL.
  2. Metadatos.
  3. Manifiesto (índice de lo que hay dentro).

* **Metadatos:** Son tablas de datos binarios que describen el programa: qué tipos (clases) existen, sus métodos, propiedades y las dependencias externas. Permiten que funcionen características como *IntelliSense* y la *Reflexión* (capacidad del código de examinarse a sí mismo).

### 1.4. El Proceso de Compilación y Ejecución

Secuencia completa desde que escribes código hasta que corre en la CPU:

1. **Compilación (Tiempo de Diseño):** El compilador de C# (`csc.exe`) toma tu código fuente `.cs` y lo convierte a un ensamblado (`.dll` o `.exe`) con MSIL y Metadatos.
2. **Carga (Tiempo de Ejecución):** Al ejecutar, el **CLR** se activa y busca el ensamblado.
3. **Verificación:** El CLR verifica que el código MSIL sea seguro (Type Safety), asegurando que no intente acceder a memoria inválida.
4. **Compilación JIT:** El compilador **JIT** toma el MSIL *bajo demanda* (solo los métodos que se van a usar) y los traduce a **Código Máquina Nativo** (específico para tu procesador x86 o x64).
5. **Ejecución:** El procesador ejecuta el código nativo.

> **[Agregado]** En .NET moderno (.NET 5+), también existe **AOT (Ahead-Of-Time)** compilation que compila todo a código nativo antes de ejecutar, eliminando el JIT para mejor rendimiento inicial.

---

## 2. Entorno de Desarrollo Integrado: Visual Studio

Visual Studio es el IDE principal para el desarrollo en .NET, proporcionando herramientas integradas para escribir, depurar y compilar código.

### 2.1. Organización de Archivos

* **Solución (.sln):** Es el contenedor de nivel más alto. Agrupa uno o más proyectos relacionados que forman parte de una misma aplicación o sistema.
* **Proyecto (.csproj, .vbproj):** Contiene el código fuente, referencias a bibliotecas, recursos (imágenes, iconos) y configuraciones de compilación. El archivo del proyecto es un documento XML que MSBuild utiliza para construir la aplicación.

### 2.2. Estructura Física de Directorios

Al compilar, Visual Studio crea carpetas importantes:

* **Carpetas `bin/`:** Aquí van los binarios (el resultado de la compilación). Dentro tendrás `bin/Debug` o `bin/Release` según tu configuración.
* **Carpetas `obj/`:** Son archivos temporales de compilación intermedia. Se usan para hacer la compilación más rápida (compilación incremental), procesando solo los archivos modificados.

> **[Agregado]** Puedes eliminar `bin/` y `obj/` de forma segura para limpiar el proyecto; se regeneran al compilar.

### 2.3. Configuraciones de Compilación: Debug vs. Release

En la barra de herramientas verás un selector "Debug" o "Release":

* **Debug (Depuración):**
  * No optimiza el código (para que coincida exactamente con lo que escribiste).
  * Genera archivos `.pdb` (Program Database) que permiten al depurador mapear la ejecución línea por línea.
  * Define la constante `DEBUG`, permitiendo usar código condicional (`#if DEBUG`).

* **Release (Lanzamiento):**
  * El compilador realiza optimizaciones agresivas (elimina código muerto, inlinea métodos).
  * Es la versión que se debe entregar al cliente final.

### 2.4. Ventanas Principales

* **Explorador de Soluciones (Solution Explorer):** Vista jerárquica de la solución, proyectos y archivos. Permite gestionar referencias y propiedades de cada elemento.
* **Cuadro de Herramientas (Toolbox):** Contiene controles visuales (botones, cajas de texto) y componentes que se pueden arrastrar al diseñador visual (Windows Forms, WPF).
* **Ventana de Propiedades:** Permite ver y modificar las propiedades (tamaño, color, nombre) y eventos de los objetos seleccionados en tiempo de diseño.
* **Lista de Errores:** Muestra errores de sintaxis, advertencias (warnings) y mensajes informativos generados por el compilador o el análisis de código en tiempo real.
* **Vista de Clases (Class View):** Muestra una estructura lógica pura de tus clases y miembros, ignorando en qué archivo físico están guardados.
* **Examinador de Objetos (Object Browser):** Permite explorar todas las librerías (.dll) referenciadas, incluyendo las de .NET, para ver qué clases y métodos tienen disponibles.

---

## 3. Productividad en el Editor de Código

El editor de Visual Studio incluye características avanzadas para acelerar la escritura de código:

* **IntelliSense:** Sistema de autocompletado inteligente. Muestra listas de miembros disponibles (métodos, propiedades), información de parámetros y descripciones rápidas mientras se escribe.

* **Code Snippets (Fragmentos de código):** Plantillas de código predefinidas que se insertan mediante atajos.
  * Ejemplo: Escribir `for` y presionar `Tab` dos veces genera la estructura completa de un bucle `for`.
  * Ejemplo: `prop` + `Tab` + `Tab` genera una propiedad auto-implementada.
  * Ejemplo: `ctor` + `Tab` + `Tab` genera un constructor. **[Agregado]**

* **Refactorización:** Herramientas para reestructurar el código sin cambiar su comportamiento externo. Incluye:
  * Renombrar variables o métodos (actualiza todas las referencias).
  * Extraer métodos (mover bloque de código a un nuevo método).
  * Encapsular campos.

* **Regiones:** La directiva `#region` y `#endregion` permite colapsar y expandir bloques de código para mejorar la legibilidad del archivo.

---

## 4. Depuración (Debugging)

La depuración es el proceso de identificar y corregir errores en tiempo de ejecución. Visual Studio ofrece un depurador visual integrado.

### 4.1. Breakpoints (Puntos de Interrupción)

Marcadores (`F9`) que pausan la ejecución del programa en una línea específica, permitiendo inspeccionar el estado de la aplicación en ese momento.

> **[Agregado]** Puedes crear *breakpoints condicionales* (clic derecho en el breakpoint) que solo pausan cuando se cumple una condición específica.

### 4.2. Ejecución Paso a Paso

* **Step Over (F10):** Ejecuta la línea actual; si es una llamada a método, lo ejecuta completo sin entrar en él.
* **Step Into (F11):** Entra en el código del método llamado en la línea actual.
* **Step Out (Shift+F11):** Ejecuta el resto del método actual y pausa al volver al llamador.

### 4.3. Inspección de Variables

* **Data Tips:** Al pasar el mouse sobre una variable en modo pausa, muestra su valor actual.
* **Ventana Locals:** Muestra automáticamente las variables del ámbito actual.
* **Ventana Watch:** Permite monitorear variables o expresiones específicas definidas por el usuario.

### 4.4. Edit and Continue

Capacidad de modificar el código fuente mientras el programa está en pausa y continuar la ejecución con los cambios aplicados sin reiniciar la aplicación.

> **[Agregado]** Hot Reload (VS 2022) permite ver cambios en tiempo real sin pausar la ejecución, especialmente útil en aplicaciones de UI.

---

## 5. Resumen de Atajos de Teclado

| Acción                     | Atajo                       | Descripción                                                                 |
| -------------------------- | --------------------------- | --------------------------------------------------------------------------- |
| **Compilar**               | `Ctrl + Shift + B`          | Compila toda la solución.                                                   |
| **Ejecutar (Debug)**       | `F5`                        | Inicia la aplicación con el depurador (permite breakpoints).                |
| **Ejecutar (Sin Debug)**   | `Ctrl + F5`                 | Inicia la aplicación sin depurador (más rápido).                            |
| **Breakpoint**             | `F9`                        | Pone o quita un punto de interrupción en la línea actual.                   |
| **Paso a Paso**            | `F10` (Over) / `F11` (Into) | `F10` salta sobre métodos, `F11` entra en ellos.                            |
| **Salir del Método**       | `Shift + F11`               | Ejecuta hasta salir del método actual. **[Agregado]**                       |
| **Ver Propiedades**        | `F4`                        | Abre la ventana de propiedades del objeto seleccionado.                     |
| **Cuadro de Herramientas** | `Ctrl + Alt + X`            | Muestra el Toolbox.                                                         |
| **Comentar Código**        | `Ctrl + K, Ctrl + C`        | Comenta la selección.                                                       |
| **Descomentar**            | `Ctrl + K, Ctrl + U`        | Descomenta la selección.                                                    |
| **Ir a Definición**        | `F12`                       | Navega a la definición del símbolo. **[Agregado]**                          |
| **Buscar Referencias**     | `Shift + F12`               | Muestra todos los usos del símbolo. **[Agregado]**                          |
| **Formatear Documento**    | `Ctrl + K, Ctrl + D`        | Aplica formato automático al código. **[Agregado]**                         |