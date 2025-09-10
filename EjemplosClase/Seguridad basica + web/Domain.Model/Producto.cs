using System;

namespace Domain.Model
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }

        public Producto(int id, string nombre, string descripcion, decimal precio, int stock)
        {
            SetId(id);
            SetNombre(nombre);
            SetDescripcion(descripcion);
            SetPrecio(precio);
            SetStock(stock);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(descripcion));
            Descripcion = descripcion;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio debe ser mayor o igual a 0.", nameof(precio));
            Precio = precio;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock debe ser mayor o igual a 0.", nameof(stock));
            Stock = stock;
        }
    }
}