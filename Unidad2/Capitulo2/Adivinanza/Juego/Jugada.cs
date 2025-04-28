using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    internal class Jugada
    {
        private bool _adivino;
        private int _intentos;
        private int _numero;

        public Jugada(int maxNumero)
        {
            Random rnd = new Random();
            Numero = rnd.Next(maxNumero);
        }

        public bool Adivino
        {
            get
            {
                return _adivino;
            }
            set
            {
            }
        }

        public int Intento
        {
            get 
            {
            return _intentos;
            }
            set
            {
            }
        }

        public int Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                _numero = value;
            }
        }

        public void Comparar(int val)
        {
            _adivino = (val == this.Numero);
        }
    }
}
