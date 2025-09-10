using System;

namespace Domain.Model
{
    public class ItemPedido
    {
        public int PedidoId { get; private set; }
        
        private int _productoId;
        private Producto? _producto;
        
        public int ProductoId 
        { 
            get => _producto?.Id ?? _productoId;
            private set => _productoId = value; 
        }
        
        public Producto? Producto 
        { 
            get => _producto;
            private set 
            {
                _producto = value;
                if (value != null && _productoId != value.Id)
                {
                    _productoId = value.Id;
                }
            }
        }
        
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        
        public decimal Total => Cantidad * PrecioUnitario;

        public ItemPedido(int pedidoId, int productoId, int cantidad, decimal precioUnitario)
        {
            SetPedidoId(pedidoId);
            SetProductoId(productoId);
            SetCantidad(cantidad);
            SetPrecioUnitario(precioUnitario);
        }

        public void SetPedidoId(int pedidoId)
        {
            if (pedidoId < 0)
                throw new ArgumentException("El PedidoId debe ser mayor o igual a 0.", nameof(pedidoId));
            PedidoId = pedidoId;
        }

        public void SetProductoId(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("El ProductoId debe ser mayor que 0.", nameof(productoId));
        
            _productoId = productoId;
            
            if (_producto != null && _producto.Id != productoId)
            {
                _producto = null;
            }
        }

        public void SetProducto(Producto producto)
        {
            ArgumentNullException.ThrowIfNull(producto);
            _producto = producto;
            _productoId = producto.Id;
        }

        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que 0.", nameof(cantidad));
            Cantidad = cantidad;
        }

        public void SetPrecioUnitario(decimal precioUnitario)
        {
            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario debe ser mayor o igual a 0.", nameof(precioUnitario));
            PrecioUnitario = precioUnitario;
        }
    }
}