using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class ProductoService 
    {
        public async Task<ProductoDTO?> GetAsync(int id)
        {
            var productoRepository = new ProductoRepository();
            Producto? producto = await productoRepository.GetAsync(id);
            
            if (producto == null)
                return null;
            
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productoRepository = new ProductoRepository();
            var productos = await productoRepository.GetAllAsync();
            
            return productos.Select(producto => new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock
            }).ToList();
        }
    }
}