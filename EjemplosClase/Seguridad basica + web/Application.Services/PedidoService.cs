using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PedidoService 
    {
        public PedidoDTO Add(PedidoDTO dto)
        {
            var pedidoRepository = new PedidoRepository();

            var fechaPedido = DateTime.Now;
            var pedido = new Pedido(0, dto.ClienteId, fechaPedido);

            // Agregar items al pedido
            foreach (var itemDto in dto.Items)
            {
                var item = new ItemPedido(0, itemDto.ProductoId, itemDto.Cantidad, itemDto.PrecioUnitario);
                pedido.AddItem(item);
            }

            pedidoRepository.Add(pedido);

            dto.Id = pedido.Id;
            dto.FechaPedido = pedido.FechaPedido;

            return dto;
        }

        public bool Delete(int id)
        {
            var pedidoRepository = new PedidoRepository();
            return pedidoRepository.Delete(id);
        }

        public PedidoDTO? Get(int id)
        {
            var pedidoRepository = new PedidoRepository();
            Pedido? pedido = pedidoRepository.Get(id);
            
            if (pedido == null)
                return null;
            
            return new PedidoDTO
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                ClienteNombre = pedido.Cliente != null ? $"{pedido.Cliente.Nombre} {pedido.Cliente.Apellido}" : null,
                FechaPedido = pedido.FechaPedido,
                Items = pedido.ItemsPedido.Select(item => new ItemPedidoDTO
                {
                    PedidoId = item.PedidoId,
                    ProductoId = item.ProductoId,
                    ProductoNombre = item.Producto?.Nombre,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario
                }).ToList()
            };
        }

        public IEnumerable<PedidoDTO> GetAll()
        {
            var pedidoRepository = new PedidoRepository();
            var pedidos = pedidoRepository.GetAll();
            
            return pedidos.Select(pedido => new PedidoDTO
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                ClienteNombre = pedido.Cliente != null ? $"{pedido.Cliente.Nombre} {pedido.Cliente.Apellido}" : null,
                FechaPedido = pedido.FechaPedido,
                Items = pedido.ItemsPedido.Select(item => new ItemPedidoDTO
                {
                    PedidoId = item.PedidoId,
                    ProductoId = item.ProductoId,
                    ProductoNombre = item.Producto?.Nombre,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario
                }).ToList()
            }).ToList();
        }

        public bool Update(PedidoDTO dto)
        {
            var pedidoRepository = new PedidoRepository();

            var pedido = new Pedido(dto.Id, dto.ClienteId, dto.FechaPedido);

            // Agregar items al pedido
            foreach (var itemDto in dto.Items)
            {
                var item = new ItemPedido(dto.Id, itemDto.ProductoId, itemDto.Cantidad, itemDto.PrecioUnitario);
                pedido.AddItem(item);
            }

            return pedidoRepository.Update(pedido);
        }
    }
}