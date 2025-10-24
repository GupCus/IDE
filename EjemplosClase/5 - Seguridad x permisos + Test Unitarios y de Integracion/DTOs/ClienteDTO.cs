using System.Text.RegularExpressions;

namespace DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Email { get; set; }
        public int PaisId { get; set; }
        public string? PaisNombre { get; set; }
        public DateTime FechaAlta { get; set; }

    }
}