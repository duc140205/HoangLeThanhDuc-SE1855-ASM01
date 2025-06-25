using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace HoangLeThanhDucWPF.ViewModels
{
    public class ProductDetailViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public Product Product { get; set; }

        public ProductDetailViewModel(Product product)
        {
            Product = product;
            Title = product.ProductId == 0 ? "Add New Product" : "Update Product";
        }
    }
}
