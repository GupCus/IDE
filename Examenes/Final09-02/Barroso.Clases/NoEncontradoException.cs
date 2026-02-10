using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroso.Clases
{
    public class NoEncontradoException : Exception
    {
        public NoEncontradoException(string message) : base(message)
        {
        }
    }
    public class MalaSolicitudException : Exception
    {
        public MalaSolicitudException(string message) : base(message)
        {
        }
    } 
}
