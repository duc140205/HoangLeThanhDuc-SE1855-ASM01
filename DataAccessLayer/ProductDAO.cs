using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        public static List<Product> products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", CategoryId = 1, UnitPrice = 999, UnitsInStock = 50 },
            new Product { ProductId = 2, ProductName = "Smartphone", CategoryId = 1, UnitPrice = 499, UnitsInStock = 100 },
            new Product { ProductId = 3, ProductName = "Headphones", CategoryId = 1, UnitPrice = 199, UnitsInStock = 200 },
            new Product { ProductId = 4, ProductName = "Novel", CategoryId = 2, UnitPrice = 15, UnitsInStock = 300 },
            new Product { ProductId = 5, ProductName = "Textbook", CategoryId = 2, UnitPrice = 59, UnitsInStock = 150 },
            new Product { ProductId = 6, ProductName = "T-Shirt", CategoryId = 3, UnitPrice = 19, UnitsInStock = 400 },
            new Product { ProductId = 7, ProductName = "Jeans", CategoryId = 3, UnitPrice = 39, UnitsInStock = 250 },
            new Product { ProductId = 8, ProductName = "Running Shoes", CategoryId = 4, UnitPrice = 89, UnitsInStock = 300 },
            new Product { ProductId = 9, ProductName = "Basketball", CategoryId = 4, UnitPrice = 29, UnitsInStock = 150 },
            new Product { ProductId = 10, ProductName = "Action Figure", CategoryId = 5, UnitPrice = 24, UnitsInStock = 200 },
            new Product { ProductId = 11, ProductName = "Lego Set", CategoryId = 5, UnitPrice = 49, UnitsInStock = 100 },
            new Product { ProductId = 12, ProductName = "Blender", CategoryId = 6, UnitPrice = 79, UnitsInStock = 80 },
            new Product { ProductId = 13, ProductName = "Cookware Set", CategoryId = 6, UnitPrice = 129, UnitsInStock = 60 },
            new Product { ProductId = 14, ProductName = "Shampoo", CategoryId = 7, UnitPrice = 9, UnitsInStock = 500 },
            new Product { ProductId = 15, ProductName = "Face Cream", CategoryId = 7, UnitPrice = 29, UnitsInStock = 300 },
            new Product { ProductId = 16, ProductName = "Car Battery", CategoryId = 8, UnitPrice = 89, UnitsInStock = 70 },
            new Product { ProductId = 17, ProductName = "Tire", CategoryId = 8, UnitPrice = 119, UnitsInStock = 40 }
        };

        //CRUD methods
        public static List<Product> GetProducts() => new List<Product>(products);
        public static Product GetProductById(int id) =>
            products.FirstOrDefault(p => p.ProductId == id);
        public static void AddProduct(Product product)
        {
            // Tìm ID lớn nhất hiện có
            int maxId = products.Any() ? products.Max(p => p.ProductId) : 0;
            // Tạo ID mới
            product.ProductId = maxId + 1;
            products.Add(product);
        }
        public static void UpdateProduct(Product product)
        {
            var existing = products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.CategoryId = product.CategoryId;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
            }
        }
        public static void DeleteProduct(int id)
        {
            var toRemove = products.FirstOrDefault(p => p.ProductId == id);
            if (toRemove != null)
            {
                products.Remove(toRemove);
            }
        }
        public static List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Product>(products);
            return products
                .Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }      
    }
}
