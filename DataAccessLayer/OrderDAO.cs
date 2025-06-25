using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        public static List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerId = 1, EmployeeId = 1, OrderDate = new DateTime(2025, 10, 1) },
            new Order { OrderId = 2, CustomerId = 2, EmployeeId = 2, OrderDate = new DateTime(2025, 10, 2) },
            new Order { OrderId = 3, CustomerId = 3, EmployeeId = 1, OrderDate = new DateTime(2025, 10, 3) },
            new Order { OrderId = 4, CustomerId = 4, EmployeeId = 2, OrderDate = new DateTime(2025, 10, 4) },
            new Order { OrderId = 5, CustomerId = 5, EmployeeId = 1, OrderDate = new DateTime(2025, 10, 5) }
        };

        // CRUD
        public static List<Order> GetOrders() => new List<Order>(orders);
        public static Order GetOrderById(int orderId) =>
            orders.FirstOrDefault(o => o.OrderId == orderId);
        public static void AddOrder(Order order) => orders.Add(order);
        public static void UpdateOrder(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.EmployeeId = order.EmployeeId;
                existingOrder.OrderDate = order.OrderDate;
            }
        }
        public static void DeleteOrder(int orderId)
        {
            var orderToRemove = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToRemove != null)
            {
                orders.Remove(orderToRemove);
            }
        }
        public static List<Order> SearchOrders(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Order>(orders);
            return orders
                .Where(o => o.CustomerId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            o.EmployeeId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            o.OrderDate.ToString("d").Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public static List<Order> GetOrdersByPeriod(DateTime startDate, DateTime endDate)
        {
            // Lấy phần ngày (loại bỏ phần thời gian) của startDate
            DateTime startOfDay = startDate.Date;
            // Lấy phần ngày của endDate và cộng thêm 1 ngày để bao trọn cả ngày cuối cùng
            DateTime endOfDay = endDate.Date.AddDays(1);

            return orders
                // Sửa lại điều kiện Where như sau:
                .Where(o => o.OrderDate >= startOfDay && o.OrderDate < endOfDay)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}
