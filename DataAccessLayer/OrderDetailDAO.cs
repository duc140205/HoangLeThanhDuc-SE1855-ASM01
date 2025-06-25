using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> orderDetails = new List<OrderDetail>
        {
            new OrderDetail { OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 30000000, Discount = 0.0 },
            new OrderDetail { OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 10000000, Discount = 0.1 },
            new OrderDetail { OrderId = 2, ProductId = 1, Quantity = 3, UnitPrice = 345000, Discount = 0.05 },
            new OrderDetail { OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 567000, Discount = 0.0 },
            new OrderDetail { OrderId = 3, ProductId = 2, Quantity = 4, UnitPrice = 1230000, Discount = 0.15 },
            new OrderDetail { OrderId = 3, ProductId = 4, Quantity = 2, UnitPrice = 3333333, Discount = 0.0 },
            new OrderDetail { OrderId = 4, ProductId = 1, Quantity = 1, UnitPrice = 5555555, Discount = 0.0 },
            new OrderDetail { OrderId = 4, ProductId = 3, Quantity = 2, UnitPrice = 343434, Discount = 0.1 },
            new OrderDetail { OrderId = 5, ProductId = 2, Quantity = 3, UnitPrice = 55555, Discount = 0.05 },
            new OrderDetail { OrderId = 5, ProductId = 4, Quantity = 1, UnitPrice = 9999999, Discount = 0.0 }

        };
        // CRUD
        public static List<OrderDetail> GetOrderDetails() => new List<OrderDetail>(orderDetails);

        public static void AddOrderDetail(OrderDetail orderDetail) => orderDetails.Add(orderDetail);

        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            var existingOrderDetail = orderDetails.FirstOrDefault(od => od.OrderId == orderDetail.OrderId && od.ProductId == orderDetail.ProductId);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                existingOrderDetail.Discount = orderDetail.Discount;
            }
        }

        public static void DeleteOrderDetail(int orderId)
        {
            var orderDetailToRemove = orderDetails.FirstOrDefault(od => od.OrderId == orderId);
            if (orderDetailToRemove != null)
            {
                orderDetails.Remove(orderDetailToRemove);
            }
        }
        public static List<OrderDetail> SearchOrderDetails(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<OrderDetail>(orderDetails);
            return orderDetails
                .Where(od => od.OrderId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             od.ProductId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             od.Quantity.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             od.UnitPrice.ToString("F2").Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             od.Discount.ToString("F2").Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public static OrderDetail GetOrderDetailById(int orderId)
        {
            return orderDetails.FirstOrDefault(od => od.OrderId == orderId);
        }
    }
}
