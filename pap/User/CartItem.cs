using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pap.User
{
    [Serializable]
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ImagemUrl1 { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }

        public CartItem(string productId, string imagemUrl1, string productName, decimal price, int quantity, string size, int sizeId, string color, int colorId)
        {
            ProductId = productId;
            ImagemUrl1 = imagemUrl1;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Size = size;
            SizeId = sizeId;
            Color = color;
            ColorId = colorId;
        }

        public decimal TotalPrice => Price * Quantity;
    }
}