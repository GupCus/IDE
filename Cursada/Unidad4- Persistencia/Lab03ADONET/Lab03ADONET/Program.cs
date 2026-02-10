using System.Data;

namespace Lab03ADONET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSet dsUniversidad = new("Universidad");

            DataTable dtAlumnos = new("Alumnos");
            DataColumn colIDAlumno = new("ID", typeof(int));
            colIDAlumno.ReadOnly = true;
            colIDAlumno.AutoIncrement = true;
            colIDAlumno.AutoIncrementSeed = 0;
            colIDAlumno.AutoIncrementStep = 1;
            DataColumn colNombre = new("Nombre",typeof(string));
            DataColumn colApellido = new("Apellido", typeof(string));
            dtAlumnos.Columns.Add(colNombre);
            dtAlumnos.Columns.Add(colApellido);
            dtAlumnos.Columns.Add(colIDAlumno);
            dtAlumnos.PrimaryKey = new DataColumn[] { colIDAlumno };
            DataRow rwAlumno = dtAlumnos.NewRow();
            rwAlumno[colNombre] = "Juan";
            rwAlumno[colApellido] = "Perez";

            dtAlumnos.Rows.Add(rwAlumno);

            DataRow rwAlumno2 = dtAlumnos.NewRow();
            rwAlumno2[colNombre] = "Marcelo";
            rwAlumno2[colApellido] = "Perez";
            dtAlumnos.Rows.Add(rwAlumno2);

            /*
            Console.WriteLine("Listado de Alumnos");
            foreach (DataRow row in dtAlumnos.Rows)
            {
                Console.WriteLine(row[colApellido].ToString() + ", " + row[colNombre].ToString());
            }
            */

            DataTable dtCursos = new("Cursos");
            DataColumn colIDCurso = new("ID", typeof(int));
            colIDCurso.ReadOnly = true;
            colIDCurso.AutoIncrement = true;
            colIDCurso.AutoIncrementSeed = 1;
            colIDCurso.AutoIncrementStep = 1;
            DataColumn colCurso = new("Curso", typeof(string));
            dtCursos.Columns.Add(colIDCurso);
            dtCursos.Columns.Add(colCurso);
            dtCursos.PrimaryKey = new[] { colIDCurso };

            DataRow rwCurso = dtCursos.NewRow();
            rwCurso[colCurso] = "Informatica";
            dtCursos.Rows.Add(rwCurso);

            DataTable dtAlum_Curso = new("Alumnos_Cursos");
            DataColumn col_ac_idAlumno = new("IDAlumno", typeof(int));
            DataColumn col_ac_idCurso = new("IDCurso", typeof(int));
            dtAlum_Curso.Columns.Add(col_ac_idCurso);
            dtAlum_Curso.Columns.Add(col_ac_idAlumno);


            dsUniversidad.Tables.Add(dtCursos);
            dsUniversidad.Tables.Add(dtAlumnos);
            dsUniversidad.Tables.Add(dtAlum_Curso);

            DataRelation relAlumno_ac = new("Alumno_Cursos", colIDAlumno, col_ac_idAlumno);
            DataRelation relCurso_ac = new("Curso_Alumnos", colIDCurso, col_ac_idCurso);

            dsUniversidad.Relations.Add(relCurso_ac);
            dsUniversidad.Relations.Add(relAlumno_ac);

            DataRow rwAlumnosCursos = dtAlum_Curso.NewRow();
            rwAlumnosCursos[col_ac_idAlumno] = 0;
            rwAlumnosCursos[col_ac_idCurso] = 1;
            dtAlum_Curso.Rows.Add(rwAlumnosCursos);

            rwAlumnosCursos = dtAlum_Curso.NewRow();
            rwAlumnosCursos[col_ac_idAlumno] = 1;
            rwAlumnosCursos[col_ac_idCurso] = 1;
            dtAlum_Curso.Rows.Add(rwAlumnosCursos);

            Console.Write("Por favor ingrese el nombre del curso: ");
            string materia = Console.ReadLine();
            Console.WriteLine("Listado de Alumnos del curso " + materia);
            DataRow[] row_CursoInf = dtCursos.Select("Curso = '" + materia + "'");
            foreach (DataRow rowCu in row_CursoInf)
            {
                DataRow[] row_AlumnosInf = rowCu.GetChildRows(relCurso_ac);
                foreach (DataRow rowAl in row_AlumnosInf)
                {
                    Console.WriteLine(
                        rowAl.GetParentRow(relAlumno_ac)[colApellido].ToString()
                        + ", " +
                        rowAl.GetParentRow(relAlumno_ac)[colNombre].ToString()
                    );
                }
            }
            Console.ReadLine();

            /*
             Este código solicita al usuario el nombre de un curso y muestra un listado de los alumnos que pertenecen a dicho curso.
                - Lee el nombre del curso por consola.
                - Busca las filas en la tabla dtCursos cuyo nombre coincide con el curso ingresado.
                - Para cada curso encontrado, obtiene los alumnos relacionados usando la relación relCurso_ac.
                - Por cada alumno, muestra el apellido y nombre, accediendo a la fila padre del alumno a través de la relación relAlumno_ac.
                - Utiliza DataRow, relaciones entre tablas y métodos como Select, GetChildRows y GetParentRow para navegar la estructura de datos relacional.
            */
        }
    }
}
