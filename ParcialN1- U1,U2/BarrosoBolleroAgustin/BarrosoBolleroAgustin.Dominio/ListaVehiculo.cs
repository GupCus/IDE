using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrosoBolleroAgustin.Dominio
{
    public class ListaVehiculo
    {
        public static Vehiculo? BuscarPatenteLinq(List<Vehiculo> lista, string patente)
        {
            var encontrado = lista.FirstOrDefault(v => v.Patente == patente);

            return encontrado;
        }
        public static Vehiculo? BuscarPatenteIterativa(List<Vehiculo> lista, string patente)
        {
            foreach (Vehiculo v in lista)
            {
                if (v.Patente == patente)
                    return v;
            }
            return null;
        }
    }
}
