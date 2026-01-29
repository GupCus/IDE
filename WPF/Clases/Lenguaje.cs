using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Lenguaje
    {
        public string Nombre { get; set; }
        public List<Snippet?> Snippets { get; set; } = new();

        public Lenguaje() 
        {
            
        }
    }
}
