# Unidad 3: Desarrollo de Aplicaciones de Escritorio (Windows Forms)

## Contenidos

1. Introducción a Windows Forms
2. Estructura de un Proyecto en Visual Studio
3. Controles y Propiedades
4. Gestión del Diseño (Layout)
5. Manejo de Eventos
6. Ventanas y Diálogos
7. DataGridView (Visualización de Datos)
8. Menús y Barras de Herramientas
9. Conceptos Técnicos Adicionales (Configuración y Despliegue)
10. Resumen de Laboratorios

---

## 1. Introducción a Windows Forms

Windows Forms (WinForms) es un conjunto de clases en el framework .NET (espacio de nombres `System.Windows.Forms`) que permite el desarrollo de aplicaciones de escritorio con una interfaz gráfica de usuario (GUI) rica.

**Características principales:**

* **Orientado a Eventos:** La ejecución del código responde a acciones del usuario (clics, teclado, movimientos del mouse).
* **Diseño Visual:** Visual Studio 2022 ofrece un diseñador "arrastrar y soltar" (WYSIWYG) que genera código automáticamente.
* **GDI+:** Utiliza `System.Drawing` para el renderizado de gráficos 2D, imágenes y texto.

---

## 2. Estructura de un Proyecto en Visual Studio

Al crear un proyecto de "Aplicación de Windows Forms" en C#, se generan archivos esenciales que separan la lógica de la interfaz.

### 2.1. Archivos Principales

1. **Program.cs:** Contiene el punto de entrada de la aplicación (`Main`). Aquí se inicia el bucle de mensajes y se define qué formulario se abre primero.
```csharp
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new FormMain()); // Inicia la app con el formulario principal
}

```


2. **Form1.cs (Código):** Donde se escribe la lógica del negocio y los manejadores de eventos.
3. **Form1.Designer.cs (Diseño):** Archivo generado automáticamente por Visual Studio.
* Contiene el método `InitializeComponent()`.
* **Importante:** Este método instancia los controles y configura sus propiedades. No debe modificarse manualmente, ya que el diseñador lo sobrescribe.



### 2.2. Jerarquía de Objetos

Todos los elementos visuales heredan de la clase base `Control`.

* **Object**  **Component**  **Control**
* **ContainerControl:** Controles que pueden alojar otros controles (ej. `Form`, `Panel`, `GroupBox`).
* **Windows Forms Controls:** Controles estándar (`Button`, `Label`, `TextBox`).



---

## 3. Controles y Propiedades

Los controles son los componentes visuales de la aplicación. Se configuran a través de la ventana de **Propiedades** en tiempo de diseño o mediante código.

### 3.1. Propiedades Comunes

* **Name:** El identificador del objeto en el código (ej. `txtUsuario`, `btnIngresar`).
* **Text:** El texto visible para el usuario (ej. "Aceptar", "Ingrese su nombre").
* **Enabled:** `true`/`false`. Determina si el control está activo.
* **Visible:** `true`/`false`. Determina si el control se muestra.
* **Dock / Anchor:** Fundamentales para el diseño adaptable (ver sección 4).

### 3.2. Controles de Uso Frecuente

* **Label:** Muestra texto de solo lectura.
* **TextBox:** Caja de texto para entrada de usuario.
* *PasswordChar:* Si se establece (ej. `*`), oculta los caracteres (útil para contraseñas).
* *ReadOnly:* Si es `true`, el usuario puede ver pero no modificar el texto.


* **Button:** Dispara una acción al ser presionado.
* **LinkLabel:** Etiqueta con comportamiento de hipervínculo.
* **MaskedTextBox:** Restringe la entrada del usuario a un formato específico (ej. fechas, teléfonos) sin necesidad de programar validaciones complejas.
* **WebBrowser:** Permite visualizar documentos HTML dentro de la aplicación.

### 3.3. Creación de Controles en Tiempo de Ejecución

Es posible agregar controles dinámicamente mediante código sin usar el diseñador:

```csharp
private void AgregarBotonDinamico()
{
    // 1. Instanciar
    Button nuevoBoton = new Button();
    
    // 2. Configurar propiedades
    nuevoBoton.Text = "Soy nuevo";
    nuevoBoton.Location = new Point(50, 50);
    nuevoBoton.Name = "btnDinamico";

    // 3. Agregar a la colección de controles del formulario
    this.Controls.Add(nuevoBoton); 
}

```

---

## 4. Gestión del Diseño (Layout)

Para que una aplicación se vea bien en diferentes resoluciones o al redimensionar la ventana, no se deben usar posiciones fijas absolutas. Se utilizan las propiedades de Layout.

### 4.1. Anchor (Anclaje)

Define la distancia constante entre los bordes del control y los bordes del contenedor (Formulario).

* Si se ancla a **Top, Left, Right**, el control se estirará horizontalmente al agrandar la ventana.
* Si se ancla a **Bottom, Right**, el control se moverá manteniéndose en la esquina inferior derecha (ideal para botones "Aceptar/Cancelar").

### 4.2. Dock (Acoplar)

Adhiere un control a un borde del contenedor, ocupando todo el ancho o alto disponible.

* **Fill:** Ocupa todo el espacio central restante (muy usado en `DataGridView` o `Panel` principal).
* **Top/Bottom/Left/Right:** Se pega al borde correspondiente.

### 4.3. Contenedores de Diseño Avanzado

* **TableLayoutPanel:** Organiza los controles en una cuadrícula (filas y columnas). Permite configurar celdas por porcentaje, píxeles o `AutoSize`. Ideal para formularios de entrada de datos alineados.
* **FlowLayoutPanel:** Los controles se colocan uno al lado del otro y fluyen a la siguiente línea si no hay espacio.
* **SplitContainer:** Divide el área en dos paneles redimensionables (ej. un menú a la izquierda y contenido a la derecha).

---

## 5. Manejo de Eventos

Los eventos son señales enviadas por un objeto (control) indicando que ha ocurrido una acción.

### 5.1. Ciclo de Vida del Formulario

1. **Load:** Ocurre antes de mostrar el formulario. Ideal para inicializar datos, configurar listas, etc.
2. **Activated:** Cuando el formulario recibe el foco.
3. **Paint:** Cuando el formulario debe redibujarse.
4. **FormClosing:** Se está intentando cerrar. Permite cancelar el cierre (ej. `e.Cancel = true`).
5. **FormClosed:** El formulario ya se cerró.
6. **Disposed:** Se liberan los recursos.

### 5.2. Eventos de Usuario

* **Click:** Presionar y soltar un botón.
* **MouseEnter / MouseLeave:** El puntero entra o sale del área del control.
* **KeyPress / KeyDown / KeyUp:** Interacción con el teclado.

### 5.3. Implementación

Para crear un manejador de eventos, se hace doble clic sobre el control en el diseñador (para el evento por defecto) o se selecciona desde la ventana de propiedades (icono del rayo).

```csharp
private void btnIngresar_Click(object sender, EventArgs e)
{
    // Código que se ejecuta al hacer clic
    MessageBox.Show("Se presionó el botón");
}

```

---

## 6. Ventanas y Diálogos

### 6.1. Formularios Modales vs. No Modales

* **No Modal (`Show`):** Permite interactuar con otras ventanas de la aplicación mientras esta permanece abierta.
```csharp
FormLogin login = new FormLogin();
login.Show();

```


* **Modal (`ShowDialog`):** Bloquea la interacción con el resto de la aplicación hasta que se cierre la ventana. Detiene la ejecución del código en el punto de llamada hasta obtener una respuesta.
```csharp
FormLogin login = new FormLogin();
login.ShowDialog(); // El código espera aquí

```



### 6.2. DialogResult

Para comunicar el resultado de un formulario modal (ej. Login o Confirmación):

1. En el formulario hijo (Login), se asigna el resultado:
```csharp
if (usuarioValido) {
    this.DialogResult = DialogResult.OK; // Esto cierra el formulario automáticamente
} else {
    MessageBox.Show("Error");
}

```


2. En el formulario padre (Main), se evalúa:
```csharp
FormLogin login = new FormLogin();
if (login.ShowDialog() == DialogResult.OK) {
    // Permitir acceso
} else {
    this.Close(); // Cerrar aplicación
}

```



### 6.3. MessageBox

Clase estática para mostrar mensajes emergentes simples.

```csharp
MessageBox.Show("Texto del mensaje", "Título", MessageBoxButtons.OK, MessageBoxIcon.Information);

```

### 6.4. Aplicaciones MDI (Multiple Document Interface)

Permiten tener ventanas "hijas" dentro de una ventana "padre".

* En el Formulario Padre: Propiedad `IsMdiContainer = true`.
* Para abrir un hijo:
```csharp
FormHijo hijo = new FormHijo();
hijo.MdiParent = this; // 'this' es el formulario padre
hijo.Show();

```



---

## 7. DataGridView (Visualización de Datos)

Control potente para mostrar y editar datos tabulares.

**Configuración clave:**

1. **DataSource:** Permite enlazar una lista de objetos o un `DataTable`.
```csharp
dgvUsuarios.DataSource = listaUsuarios;

```


2. **AutoGenerateColumns:** Si es `false`, permite definir las columnas manualmente en diseño para mayor control.
3. **SelectionMode:** Define si se selecciona una celda o la fila completa (`FullRowSelect`).

**Ejemplo de uso en capa de presentación:**

```csharp
public void Listar() {
    // Se asume una lista de objetos preexistente
    this.dgvUsuarios.DataSource = null; // Limpiar origen anterior
    this.dgvUsuarios.DataSource = _negocio.ObtenerUsuarios();
}

```

---

## 8. Menús y Barras de Herramientas

* **MenuStrip:** Menú principal estándar (Archivo, Editar, Ayuda) ubicado en la parte superior.
* **ToolStrip:** Barra de herramientas con iconos para acceso rápido (Guardar, Imprimir, Nuevo).
* **ContextMenuStrip:** Menú contextual que aparece al hacer clic derecho sobre un control. Se asigna mediante la propiedad `ContextMenuStrip` del control destino.

---

## 9. Conceptos Técnicos Adicionales (Configuración y Despliegue)

### 9.1. Archivo de Configuración (`App.config`)

Es un archivo basado en XML que permite separar la configuración de la aplicación del código fuente. Es la evolución de los antiguos archivos `.INI`.

* 
**Uso común:** Almacenar cadenas de conexión a bases de datos (`connectionStrings`) o parámetros generales (`appSettings`) que pueden cambiar sin necesidad de recompilar el programa.



### 9.2. Despliegue con ClickOnce

Es una tecnología que facilita la instalación y actualización de aplicaciones de escritorio.

* 
**Ventaja:** Permite al usuario instalar la aplicación desde una página web, una carpeta de red o un CD/USB con mínima interacción.


* 
**Actualizaciones:** Puede verificar automáticamente si hay nuevas versiones y actualizar solo los componentes necesarios.



### 9.3. Clases Parciales (`Partial Class`)

Es el mecanismo de C# que permite dividir la definición de una misma clase en varios archivos físicos.

* En Windows Forms, esto es clave para separar el código generado automáticamente por el diseñador (`Form1.Designer.cs`) del código lógico que tú escribes (`Form1.cs`), manteniendo el proyecto organizado y limpio.



### 9.4. Gestión del Foco y Navegación

* **TabIndex:** Propiedad numérica que determina el orden en que el foco salta de un control a otro cuando el usuario presiona la tecla `TAB`. Es vital para la usabilidad (ej. ir del campo "Usuario" al campo "Contraseña" y luego al botón "Ingresar").


* 
**Focus():** Método para asignar el foco a un control específico por código (ej. `txtUsuario.Focus()` al abrir el formulario).



### 9.5. Propiedades Avanzadas del Formulario

Para dar un acabado profesional a las ventanas (especialmente diálogos), se usan propiedades específicas:

* 
**AcceptButton:** Asigna un botón del formulario que se activará automáticamente al presionar `ENTER` (comúnmente el botón "Aceptar" o "Ingresar").


* **FormBorderStyle:** Define el estilo del borde. Para ventanas de diálogo fijas, se suele usar `FixedSingle` (evita que el usuario cambie el tamaño manualmente).


* **StartPosition:** Define dónde aparece la ventana al abrirse. `CenterParent` centra la ventana respecto a quien la llamó, muy usado en modales.


* 
**MaximizeBox / MinimizeBox:** Propiedades booleanas (`true`/`false`) para ocultar los botones de maximizar y minimizar en la barra de título.




---

## 10. Resumen de Laboratorios

Aquí te detallo en qué consiste cada actividad práctica para que tengas claro el objetivo de cada ejercicio.

### Laboratorio 10.1: Creación de Login y Ventanas Modales

**Objetivo:** Construir un sistema de acceso (Login) que funcione dentro de una aplicación principal (MDI).

1. **Diseño del Login:**
* Uso de `TextBox` para usuario y contraseña (con propiedad `PasswordChar` para ocultar caracteres como asteriscos).


* Uso de `LinkLabel` para la opción "Olvidé mi contraseña".




2. **Lógica de Validación:**
* Programación del evento `Click` del botón Ingresar.
* Validación condicional (`if`) de credenciales hardcodeadas ("Admin"/"admin").


* Uso de `MessageBox` para informar éxito o error al usuario.


* Uso de `DialogResult` para indicar al formulario padre si el logueo fue exitoso (`DialogResult.OK`).




3. **Integración MDI:**
* Creación de un formulario principal (`formMain`) configurado como contenedor MDI (`IsMdiContainer: True`).


* Llamada al Login usando `ShowDialog()` para que sea **modal** (bloquea el uso de la app principal hasta que te logueas).





### Laboratorio 10.2: Listado con Grilla (ABM Simple)

**Objetivo:** Crear una pantalla de gestión de usuarios que presente datos en una tabla y tenga una barra de herramientas.

1. **Layout (Diseño Adaptable):**
* Uso de `TableLayoutPanel` para dividir la pantalla en áreas proporcionales (filas y columnas) que se ajustan si se agranda la ventana.


* Configuración de filas/columnas con porcentajes (ej. 100%) o `AutoSize`.




2. **Controles de Datos:**
* Implementación de un `DataGridView` para listar información.
* Configuración de la grilla: Solo lectura, sin agregar filas manualmente (`AllowUserToAddRows = false`), y definición de columnas específicas (ID, Nombre, Email, Habilitado) .




3. **Barra de Herramientas:**
* Uso de `ToolStrip` para agregar botones con iconos (Nuevo, Editar, Eliminar) en la parte superior, simulando un menú de acciones estándar.




4. **Lógica de Carga:**
* Creación de un método `Listar()` que vincula una lista de objetos (hardcodeada por ahora) a la propiedad `DataSource` de la grilla para mostrar los datos automáticamente.


* Uso del evento `Load` del formulario para cargar la lista al iniciar.