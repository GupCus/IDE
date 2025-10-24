using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PedidoService 
    {
        public async Task<PedidoDTO> AddAsync(PedidoDTO dto)
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

            await pedidoRepository.AddAsync(pedido);

            dto.Id = pedido.Id;
            dto.FechaPedido = pedido.FechaPedido;

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedidoRepository = new PedidoRepository();
            return await pedidoRepository.DeleteAsync(id);
        }

        public async Task<PedidoDTO?> GetAsync(int id)
        {
            var pedidoRepository = new PedidoRepository();
            Pedido? pedido = await pedidoRepository.GetAsync(id);
            
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

        public async Task<IEnumerable<PedidoDTO>> GetAllAsync()
        {
            var pedidoRepository = new PedidoRepository();
            var pedidos = await pedidoRepository.GetAllAsync();
            
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

        public async Task<bool> UpdateAsync(PedidoDTO dto)
        {
            var pedidoRepository = new PedidoRepository();

            var pedido = new Pedido(dto.Id, dto.ClienteId, dto.FechaPedido);

            // Agregar items al pedido
            foreach (var itemDto in dto.Items)
            {
                var item = new ItemPedido(dto.Id, itemDto.ProductoId, itemDto.Cantidad, itemDto.PrecioUnitario);
                pedido.AddItem(item);
            }

            return await pedidoRepository.UpdateAsync(pedido);
        }
    }
}