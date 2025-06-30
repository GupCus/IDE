using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrosoBolleroAgustin.Dominio
{
    public class Auto : Vehiculo
    {
        public string Color
        {
            get;set;
        }

        public Auto(string patente, int ruedas, int modelo,string color) : base(patente,ruedas,modelo)
        {
            this.Color = color;
        }
        public override string ToString()
        {
            return $"{base.ToString()} Color: {this.Color} ";
        }
    }
}
