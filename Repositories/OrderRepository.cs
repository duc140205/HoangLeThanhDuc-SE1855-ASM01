using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            OrderDAO.AddOrder(order);
        }

        public void DeleteOrder(int orderId)
        {
            OrderDAO.DeleteOrder(orderId);
        }

        public Order GetOrderById(int orderId)
        {
            return OrderDAO.GetOrderById(orderId);
        }

        public List<Order> GetOrders()
        {
            return OrderDAO.GetOrders();
        }

        public List<Order> SearchOrders(string searchTerm)
        {
            return OrderDAO.SearchOrders(searchTerm);
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.UpdateOrder(order);
        }
        public List<Order> GetOrdersByPeriod(DateTime startDate, DateTime endDate)
        {
            return OrderDAO.GetOrdersByPeriod(startDate, endDate);
        }
    }
}
