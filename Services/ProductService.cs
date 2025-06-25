using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iproductRepository;
        public ProductService()
        {
            iproductRepository = new ProductRepository(); 
        }

        public void AddProduct(Product product)
        {
            iproductRepository.AddProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            iproductRepository.DeleteProduct(productId);
        }

        public Product GetProductById(int productId)
        {
            return iproductRepository.GetProductById(productId);
        }

        public List<Product> GetProducts()
        {
            return iproductRepository.GetProducts();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            return iproductRepository.SearchProducts(searchTerm);
        }

        public void UpdateProduct(Product product)
        {
            iproductRepository.UpdateProduct(product);
        }
    }
}
