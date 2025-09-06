using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02TXTADO
{
    internal class ManejadorArchivoTxt : ManejadorArchivo
    {
        //Para más información visitar http://www.connectionstrings.com/
        protected string connectionString
        {
            get
            {
                return @"Provider=Microsoft.Jet.OLEDB.4.0;
                Data Source=C:\Users\Admin\Desktop\Git\IDE\Unidad4- Persistencia\Lab02TXTADO\Lab02TXTADO\bin\Debug;" +
                "Extended Properties='text;HDR=Yes;FMT=Delimited'";
            }
        }
        /*
            Cada origen de datos requiere datos específicos, en el caso de archivos planos se requiere:
            •	Provider: indica cual driver de conexión utilizaremos.
            •	DataSource: en el caso de los archivos planos es la carpeta donde se encuentran los archivos.
            Además de los datos obligatorios los connection string tienen datos opcionales, 
            en este caso las Extended Properties que indica que el archivo es de tipo texto, 
            tienen Header (encabezado con el nombre de las columnas) y 
            el formato del archivo (en este caso delimitado por comas).
            El arroba es para evitar tener que usar \\ en vez de \ en el string
        */
        public override DataTable GetTabla()
        {
            using (OleDbConnection Conn = new(connectionString))
            {
                OleDbCommand cmdSelect = new("select * from agenda.txt", Conn);
                Conn.Open();
                OleDbDataReader reader = cmdSelect.ExecuteReader();
                DataTable contactos = new DataTable();
                if (reader != null)
                {
                    contactos.Load(reader);
                }
                Conn.Close();
                return contactos;
            }
        }
        /*
            Este método aplica los cambios realizados en el DataTable 'miContactos' a un archivo de texto 'agenda.txt' usando OleDb.
            - Inserta filas nuevas, actualiza filas modificadas y elimina filas borradas.
            - Utiliza comandos parametrizados para evitar inyección SQL y manejar los valores dinámicos.
            - Obtiene los cambios (agregados, eliminados, modificados) del DataTable y los sincroniza con el archivo.
            - La conexión a la base (archivo de texto) se abre solo una vez para mayor eficiencia.
            - Permite mantener sincronizados los datos en memoria con los datos persistidos en el archivo de texto.
        */
        public override void AplicaCambios()
        {
            using (OleDbConnection Conn = new OleDbConnection(connectionString))
            {
                OleDbCommand cmdInsert = new OleDbCommand("insert into agenda.txt values(@id,@nombre,@apellido,@email,@telefono)", Conn);
                cmdInsert.Parameters.Add("@id", OleDbType.Integer);
                cmdInsert.Parameters.Add("@nombre", OleDbType.VarChar);
                cmdInsert.Parameters.Add("@apellido", OleDbType.VarChar);
                cmdInsert.Parameters.Add("@email", OleDbType.VarChar);
                cmdInsert.Parameters.Add("@telefono", OleDbType.VarChar);

                OleDbCommand cmdUpdate = new OleDbCommand("update agenda.txt set nombre=@nombre, apellido=@apellido, e-mail=@email, telefono=@telefono where id=@id", Conn);
                cmdUpdate.Parameters.Add("@nombre", OleDbType.VarChar);
                cmdUpdate.Parameters.Add("@apellido", OleDbType.VarChar);
                cmdUpdate.Parameters.Add("@email", OleDbType.VarChar);
                cmdUpdate.Parameters.Add("@telefono", OleDbType.VarChar);
                cmdUpdate.Parameters.Add("@id", OleDbType.Integer);

                OleDbCommand cmdDelete = new OleDbCommand("delete from agenda.txt where id=@id", Conn);
                cmdDelete.Parameters.Add("@id", OleDbType.Integer);

                DataTable filasNuevas = this.misContactos.GetChanges(DataRowState.Added);
                DataTable filasBorradas = this.misContactos.GetChanges(DataRowState.Deleted);
                DataTable filasModificadas = this.misContactos.GetChanges(DataRowState.Modified);

                Conn.Open();

                if (filasNuevas != null)
                {
                    foreach (DataRow fila in filasNuevas.Rows)
                    {
                        cmdInsert.Parameters["@id"].Value = fila["id"];
                        cmdInsert.Parameters["@nombre"].Value = fila["nombre"];
                        cmdInsert.Parameters["@apellido"].Value = fila["apellido"];
                        cmdInsert.Parameters["@email"].Value = fila["email"];
                        cmdInsert.Parameters["@telefono"].Value = fila["telefono"];
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                if (filasBorradas != null)
                {
                    foreach (DataRow fila in filasBorradas.Rows)
                    {
                        cmdDelete.Parameters["@id"].Value = fila["id", DataRowVersion.Original];
                        cmdDelete.ExecuteNonQuery();
                    }
                }

                if (filasModificadas != null)
                {
                    foreach (DataRow fila in filasModificadas.Rows)
                    {
                        cmdUpdate.Parameters["@id"].Value = fila["id"];
                        cmdUpdate.Parameters["@nombre"].Value = fila["nombre"];
                        cmdUpdate.Parameters["@apellido"].Value = fila["apellido"];
                        cmdUpdate.Parameters["@email"].Value = fila["email"];
                        cmdUpdate.Parameters["@telefono"].Value = fila["telefono"];
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
