using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
