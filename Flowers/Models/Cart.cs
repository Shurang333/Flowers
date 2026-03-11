using System.Collections.Generic;
using System.Linq;

namespace Flowers.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }

    public class Cart
    {
        private static readonly Cart _instance = new Cart();
        public static Cart Instance => _instance;

        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyList<CartItem> Items => _items;

        public void AddItem(int productId, string name, decimal unitPrice, int quantity, string imagePath)
        {
            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing == null)
            {
                _items.Add(new CartItem
                {
                    ProductId = productId,
                    Name = name,
                    ImagePath = imagePath,
                    UnitPrice = unitPrice,
                    Quantity = quantity
                });
            }
            else
            {
                existing.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            _items.RemoveAll(i => i.ProductId == productId);
        }

        public decimal GetTotal()
        {
            return _items.Sum(i => i.Total);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}

