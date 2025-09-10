using Domain.Model;

namespace Data
{
    public class ClienteInMemory
    {
        //No es ThreadSafe pero sirve para el proposito del ejemplo        
        public static List<Cliente> Clientes;

        static ClienteInMemory()
        {
            Clientes = new List<Cliente>
            {
                new Cliente(1, "Juan", "Pérez", "juan.perez@email.com", DateTime.Now.AddMonths(-5)),
                new Cliente(2, "María", "Gómez", "maria.gomez@email.com", DateTime.Now.AddMonths(-4)),
                new Cliente(3, "Carlos", "López", "carlos.lopez@email.com", DateTime.Now.AddMonths(-3)),
                new Cliente(4, "Ana", "Martínez", "ana.martinez@email.com", DateTime.Now.AddMonths(-2)),
                new Cliente(5, "Lucía", "Fernández", "lucia.fernandez@email.com", DateTime.Now.AddMonths(-1))
            };
        }

    }
}
