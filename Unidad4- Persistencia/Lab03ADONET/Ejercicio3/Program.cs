using Ejercicio3;
namespace Ejercicio3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dsUniversidad miUniversidad = new dsUniversidad();

            dsUniversidad.dtAlumnosDataTable dtAlumnos = new dsUniversidad.dtAlumnosDataTable();
            dsUniversidad.dtCursosDataTable dtCursos = new dsUniversidad.dtCursosDataTable();
            dsUniversidad.dtAlumnosRow rowAlumno = dtAlumnos.NewdtAlumnosRow();

            rowAlumno.Apellido = "Perez";
            rowAlumno.Nombre = "Juan";
            dtAlumnos.AdddtAlumnosRow(rowAlumno);

            dsUniversidad.dtCursosRow rowCurso = dtCursos.NewdtCursosRow();
            rowCurso.Curso = "Informatica";
            dtCursos.AdddtCursosRow(rowCurso);

            // Primero creamos el objeto datatable
            dsUniversidad.dt_Alumnos_CursosDataTable dtAlumnos_Cursos = new dsUniversidad.dt_Alumnos_CursosDataTable();

            dsUniversidad.dt_Alumnos_CursosRow rowAlumnosCursos = dtAlumnos_Cursos.Newdt_Alumnos_CursosRow();

            dtAlumnos_Cursos.Adddt_Alumnos_CursosRow(rowAlumno, rowCurso);
        }
        /*
  // RESUMEN DEL USO DE DATASETS TIPADOS:
  Este bloque de código utiliza DataSets tipados para manejar datos relacionales de una universidad.
  - Se crean DataTables tipados para alumnos y cursos, asociados al DataSet tipado 'dsUniversidad'.
  - Se agregan filas a las tablas de alumnos y cursos con datos de ejemplo.
  - Se crea una tabla relacional que asocia alumnos y cursos ('dt_Alumnos_Cursos'), representando la relación muchos a muchos.
  - Se agregan filas a la tabla de relación que vinculan un alumno con un curso.
  El uso de DataSets tipados permite trabajar con objetos fuertemente tipados, 
  facilitando el acceso a las columnas y mejorando la legibilidad y robustez del código.
*/
    }
}
