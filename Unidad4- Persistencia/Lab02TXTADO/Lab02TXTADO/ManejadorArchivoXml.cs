using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02TXTADO
{
    internal class ManejadorArchivoXml:ManejadorArchivo
    {
        protected DataSet ds;
        public override DataTable GetTabla()
        {
            ds = new();
            ds.ReadXml("agenda.xml");
            return ds.Tables["contactos"];
        }

        public override void AplicaCambios()
        {
            ds.WriteXml("agenda.xml");
            ds.WriteXml("agendaconschema.xml", XmlWriteMode.WriteSchema);
        }

        /*En este caso utilizaremos el objeto DataSet, el cual puede contener varias DataTables
        El mismo permite guardar todas las DataTable con o sin su estructura a través del método WriteXml.
        Adicionalmente, como en este caso, puede crear los DataTable y cargarlos con datos a partir 
        de un XML a través del método ReadXml.
        Los DataTables también tienen estas capacidades pero la razón por la cual elegimos 
        el objeto DataSet es que tiene la capacidad de extrapolar la estructura de los DataTable 
        a partir de un XML bien formado y valido.
        La razón por la cual escribimos el XML dos veces es porque la primera vez lo hacemos sólo con los datos y en el segundo caso también almacenamos la estructura del mismo.
        */
    }
}
