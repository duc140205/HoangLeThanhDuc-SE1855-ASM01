using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace HoangLeThanhDucWPF.ViewModels
{
    public class CustomerDetailViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public Customer Customer { get; set; }

        public CustomerDetailViewModel(Customer customer)
        {
            Customer = customer;
            // Nếu ID = 0, đây là cửa sổ Add, ngược lại là Update
            Title = customer.CustomerId == 0 ? "Add New Customer" : "Update Customer";
        }
    }
}
