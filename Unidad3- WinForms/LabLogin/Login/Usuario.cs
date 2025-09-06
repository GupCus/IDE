using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioN { get; set; }
        public string Email { get; set; }
        public bool Habilitado { get; set; }

        private static int _contador = 4;
        public Usuario(string? id, string nombre, string apellido, string usuario, string email, bool habilitado)
        {
            Nombre = nombre;
            Apellido = apellido;
            UsuarioN = usuario;
            Email = email;
            Habilitado = habilitado;

            if(id == null)
            {
                Id = _contador.ToString();
                _contador += 1;
            }
            else
            {
                Id = id;
            }
            
        }


    }
}
