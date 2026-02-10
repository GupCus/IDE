using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab02TXTADO
{
    public class ManejadorArchivo
    {
        protected DataTable misContactos;
        public ManejadorArchivo()
        {
            this.misContactos = this.GetTabla();
        }

        //Los métodos getTabla y aplicaCambios tienen el modificador virtual para permitir ser sobrescritos en las clases hijas.
        public virtual DataTable GetTabla()
        {
            return new DataTable();
        }
        public virtual void AplicaCambios() 
        {

        }

        public void Listar() 
        {
            foreach (DataRow fila in this.misContactos.Rows)
            {
                if(fila.RowState != DataRowState.Deleted)
                {
                    foreach(DataColumn col in this.misContactos.Columns)
                    {
                        Console.WriteLine("{0}: {1}", col.ColumnName, fila[col]);
                    }
                }
            }
        }
        /*
        En el método listar los contactos recorremos la colección Rows de la DataTable.
        Para listar el nombre de la columna recorremos la colección de columnas del DataTable y 
        utilizamos la propiedad ColumnName y para acceder a cada una de las celdas de la DataTable 
        recorremos la colección de filas (DataRows) con el foreach y luego para cada fila 
        accedemos a  los valores de las celdas como si fuese un array pero no sólo podemos 
        acceder con el índice de la columna sino también con el nombre de la columna o como en este caso con el objeto columna.
        */
        public void NuevaFila() 
        {
            DataRow fila = this.misContactos.NewRow();
            foreach(DataColumn col in this.misContactos.Columns)
            {
                Console.Write("Ingrese {0}: ", col.ColumnName);
                fila[col] = Console.ReadLine();
            }
            this.misContactos.Rows.Add(fila);
        }
        /*
            Para crear un nuevo contacto creamos una nueva fila en la DataTable con: this.misContactos.NewRow();
            Es muy importante destacar que la única forma de crear una nueva fila es a partir de un DataTable. 
            Esto se debe a que la fila debe tener una estructura de celdas y la misma es proporcionada por el DataTable a partir del cual se genera. 
            Cabe aclarar que aunque una fila se genere a partir de una DataTable no significa que la misma esté agregada dentro de la colección de filas de la DataTable
            Luego asignamos los valores a cada una de las celdas de la fila con:
            foreach (DataColumn col in this.misContactos.Columns)
            {
              	 Console.Write("Ingrese {0}:",col.ColumnName);
                 fila[col] = Console.ReadLine();
            }
            Finalmente agregamos la fila a la tabla con:
            this.misContactos.Rows.Add(fila);
        */
        public void EditarFila() 
        {
            Console.Write("Ingrese el numero de fila a editar (1 default): ");
            int nroFila= int.TryParse(Console.ReadLine(), out nroFila)? nroFila: 1;
            DataRow fila = this.misContactos.Rows[nroFila - 1];
            for (int nroCol=1; nroCol < this.misContactos.Columns.Count; nroCol++)
            {
                DataColumn col = this.misContactos.Columns[nroCol];
                Console.Write("Ingrese {0}: ", col.ColumnName);
                fila[col] = Console.ReadLine();
            }
        }
        /*
        Primero identificamos la fila de la tabla que queremos modificar aquí lo hacemos con el índice: DataRow fila = this.misContactos.Rows[nroFila - 1];
        También podría hacerse con un método de la DataTable llamado Select que permite filtrar un grupo de filas cuyos datos cumplen con determinados criterios.
        Luego modificamos los datos de la fila con:
        for (int nroCol = 1; nroCol< this.misContactos.Columns.Count; nroCol++)
        //el 0 se omite por ser la ID
        {
            DataColumn col = this.misContactos.Columns[nroCol];
            Console.Write("Ingrese {0}:", col.ColumnName);
            fila[col] = Console.ReadLine();
        }
        Aquí no es necesario agregarla a la DataTable como en el caso anterior porque la fila ya existía y se encontraba en la DataTable.
        */
        public void EliminarFila() 
        {
            Console.Write("Ingrese el numero de fila a eliminar (1 default): ");
            int nroFila = int.TryParse(Console.ReadLine(), out nroFila) ? nroFila : 1;
            this.misContactos.Rows[nroFila - 1].Delete();
        }
        /*
         Para eliminar una fila simplemente localizamos la DataRow que deseamos eliminar. 
         Luego simplemente eliminamos la fila con el método Delete de la DataRow.
        */
    }
}
