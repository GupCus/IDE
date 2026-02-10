using System.Linq;

namespace DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNombre { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<ItemPedidoDTO> Items { get; set; } = new();

        public int CantidadItems => Items?.Sum(i => i.Cantidad) ?? 0;
        public decimal Total => Items?.Sum(i => i.Total) ?? 0;
    }
}