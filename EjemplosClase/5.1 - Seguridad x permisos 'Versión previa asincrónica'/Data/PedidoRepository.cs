using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PedidoRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Pedido pedido)
        {
            using var context = CreateContext();
            await context.Pedidos.AddAsync(pedido);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var pedido = await context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                context.Pedidos.Remove(pedido);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Pedido?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(i => i.Producto)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Pedido pedido)
        {
            using var context = CreateContext();
            var existingPedido = await context.Pedidos
                .Include(p => p.ItemsPedido)
                .FirstOrDefaultAsync(p => p.Id == pedido.Id);
            
            if (existingPedido != null)
            {
                // Actualizar propiedades básicas del pedido
                existingPedido.SetClienteId(pedido.ClienteId);
                existingPedido.SetFechaPedido(pedido.FechaPedido);
                
                // Manejo inteligente de ItemsPedido
                
                // 1. Items a eliminar (están en BD pero no en la nueva lista)
                var itemsToDelete = existingPedido.ItemsPedido
                    .Where(existing => !pedido.ItemsPedido.Any(nuevo => nuevo.ProductoId == existing.ProductoId))
                    .ToList();
                
                foreach (var itemToDelete in itemsToDelete)
                {
                    existingPedido.RemoveItem(itemToDelete);
                }
                
                // 2. Items a actualizar o agregar
                foreach (var nuevoItem in pedido.ItemsPedido)
                {
                    var existingItem = existingPedido.ItemsPedido
                        .FirstOrDefault(e => e.ProductoId == nuevoItem.ProductoId);
                    
                    if (existingItem != null)
                    {
                        // Actualizar item existente
                        existingItem.SetCantidad(nuevoItem.Cantidad);
                        existingItem.SetPrecioUnitario(nuevoItem.PrecioUnitario);
                    }
                    else
                    {
                        // Agregar nuevo item
                        existingPedido.AddItem(nuevoItem);
                    }
                }
                
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}