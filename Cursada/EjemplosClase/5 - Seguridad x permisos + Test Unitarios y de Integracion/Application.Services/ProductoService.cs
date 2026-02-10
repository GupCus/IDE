using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class ProductoService 
    {
        public ProductoDTO? Get(int id)
        {
            var productoRepository = new ProductoRepository();
            Producto? producto = productoRepository.Get(id);
            
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

        public IEnumerable<ProductoDTO> GetAll()
        {
            var productoRepository = new ProductoRepository();
            var productos = productoRepository.GetAll();
            
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