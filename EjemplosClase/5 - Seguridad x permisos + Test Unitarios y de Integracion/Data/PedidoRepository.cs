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

        public void Add(Pedido pedido)
        {
            using var context = CreateContext();
            context.Pedidos.Add(pedido);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var pedido = context.Pedidos.Find(id);
            if (pedido != null)
            {
                context.Pedidos.Remove(pedido);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Pedido? Get(int id)
        {
            using var context = CreateContext();
            return context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pedido> GetAll()
        {
            using var context = CreateContext();
            return context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(i => i.Producto)
                .ToList();
        }

        public bool Update(Pedido pedido)
        {
            using var context = CreateContext();
            var existingPedido = context.Pedidos
                .Include(p => p.ItemsPedido)
                .FirstOrDefault(p => p.Id == pedido.Id);
            
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
                
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}