using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class Customer
    {
        public Customer(int customerId, string companyName, string contactName, string contactTitle, 
                         string address, int phone)
        {
            CustomerId = customerId;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            Phone = phone;
        }

        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return $"{CustomerId} - {CompanyName} - {ContactName} - {ContactTitle} - {Address} - {Phone}";
        }

    }
}
