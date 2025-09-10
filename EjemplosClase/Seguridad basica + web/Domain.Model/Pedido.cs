using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Pedido
    {
        public int Id { get; private set; }
        
        private int _clienteId;
        private Cliente? _cliente;
        
        public int ClienteId 
        { 
            get => _cliente?.Id ?? _clienteId;
            private set => _clienteId = value; 
        }
        
        public Cliente? Cliente 
        { 
            get => _cliente;
            private set 
            {
                _cliente = value;
                if (value != null && _clienteId != value.Id)
                {
                    _clienteId = value.Id;
                }
            }
        }
        
        public DateTime FechaPedido { get; private set; }
        
        private readonly List<ItemPedido> _itemsPedido = new();
        public IReadOnlyList<ItemPedido> ItemsPedido => _itemsPedido.AsReadOnly();

        public Pedido(int id, int clienteId, DateTime fechaPedido)
        {
            SetId(id);
            SetClienteId(clienteId);
            SetFechaPedido(fechaPedido);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetClienteId(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("El ClienteId debe ser mayor que 0.", nameof(clienteId));
        
            _clienteId = clienteId;
            
            if (_cliente != null && _cliente.Id != clienteId)
            {
                _cliente = null;
            }
        }

        public void SetCliente(Cliente cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente);
            _cliente = cliente;
            _clienteId = cliente.Id;
        }

        public void SetFechaPedido(DateTime fechaPedido)
        {
            if (fechaPedido == default)
                throw new ArgumentException("La fecha del pedido no puede ser nula.", nameof(fechaPedido));
            FechaPedido = fechaPedido;
        }

        public void AddItem(ItemPedido item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _itemsPedido.Add(item);
        }

        public void RemoveItem(ItemPedido item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _itemsPedido.Remove(item);
        }

        public void ClearItems()
        {
            _itemsPedido.Clear();
        }
    }
}