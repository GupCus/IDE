namespace DTOs
{
    public class ItemPedidoDTO
    {
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total => Cantidad * PrecioUnitario;
    }
}