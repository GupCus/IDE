using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego
{
    public class JugadaConAyuda : Jugada
    {
        public JugadaConAyuda(int max):base(max)
        {

        }

        public new void Comparar(int val)
        {
            _adivino = (val == this.Numero);
            string? rta = null;
            if (!_adivino) { 
                _intentos += 1;
                int dist = Math.Abs(this.Numero - val);
                if (dist > 100)
                {
                    rta = "Friiioooo... el numero está a más de 100 unidades de distancia ";
                }else if(dist < 6)
                {
                    rta = "Calienteee... el numero está a menos de 5, cercaa";
                }
            }
            Console.WriteLine(rta);
        }
    }
}