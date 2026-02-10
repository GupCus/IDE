using System.Data;
using Microsoft.Data.SqlClient;

namespace Ejercicio4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creamos un DataTable para almacenar los datos de las empresas
            DataTable dtEmpresas = new("Empresas");
            dtEmpresas.Columns.Add("CustomerID", typeof(string)); // Columna para el ID del cliente
            dtEmpresas.Columns.Add("CompanyName", typeof(string)); // Columna para el nombre de la empresa

            // Creamos la conexión a la base de datos SQL Server
            SqlConnection myconn = new();
            myconn.ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Pwd=bdagus2025;TrustServerCertificate=True";

            /* EJ con sql command
            // Creamos el comando SQL para obtener los datos deseados
            SqlCommand myCommand = new();
            myCommand.CommandText = "SELECT CustomerID, CompanyName FROM Customers"; 
            myCommand.Connection = myconn;
            
            myconn.Open(); // Abrimos la conexión a la base de datos
            SqlDataReader mydr = myCommand.ExecuteReader(); // Ejecutamos la consulta y obtenemos un lector de datos
            dtEmpresas.Load(mydr); // Cargamos los datos del lector en el DataTable
            myconn.Close(); // Cerramos la conexión
            */

            //EJ con sql adapter

            SqlDataAdapter myadap = new("SELECT CustomerID, CompanyName FROM Customers",myconn);

            myconn.Open(); // Abrimos la conexión a la base de datos
            myadap.Fill(dtEmpresas);//Cargo el contenido en el datatable
            myconn.Close(); // Cerramos la conexión

            Console.WriteLine("Listado");
            // Recorremos el DataTable y mostramos los resultados
            foreach (DataRow row in dtEmpresas.Rows)
            {
                string ide = row["CustomerID"].ToString(); // Obtenemos el ID del cliente
                string nome = row["CompanyName"].ToString(); // Obtenemos el nombre de la empresa
                Console.WriteLine(ide + " - " + nome); 
            }

            Console.Write("Escriba el ID a modificar: ");
            string custid = Console.ReadLine();

            DataRow[] rwempresas = dtEmpresas.Select("CustomerID = '" + custid + "'");
            if(rwempresas.Length != 1)
            {
                Console.WriteLine("No encontrado");
                Console.ReadLine();
                return;
            }
            DataRow rowmiemp = rwempresas[0];
            string nomact = rowmiemp["CompanyName"].ToString();
            Console.WriteLine("Nombre actual: "+ nomact);
            Console.Write("\n Nuevo nombre: ");
            string nnombre = Console.ReadLine();

            rowmiemp.BeginEdit();
            rowmiemp["CompanyName"] = nnombre;
            rowmiemp.EndEdit();

            //Ahora creo un objeto Command que utilizaré para
            //guardar los cambios en la base de datos
            SqlCommand updcommand = new SqlCommand();
            //Le indico la conexión
            updcommand.Connection = myconn;
            //Le indico la cadena TSQL
            updcommand.CommandText =
                "UPDATE Customers SET CompanyName = @CompanyName WHERE CustomerID = @CustomerID";
            //Indico los parámetros que estoy utilizando.
            //Como así también, el tipo de dato, la longitud del dato
            //el nombre del campo del datatable
            updcommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 50, "CompanyName");
            updcommand.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 5, "CustomerID");

            myadap.UpdateCommand = updcommand;
            myadap.Update(dtEmpresas);

        }
    }
}
