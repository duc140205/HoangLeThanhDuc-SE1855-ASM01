using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(Product product)
        {
            ProductDAO.AddProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            ProductDAO.DeleteProduct(productId);
        }

        public Product GetProductById(int productId)
        {
            return ProductDAO.GetProductById(productId);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            return ProductDAO.SearchProducts(searchTerm);
        }

        public void UpdateProduct(Product product)
        {
            ProductDAO.UpdateProduct(product);
        }
    }
}
