using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository iorderRepository;
        public OrderService()
        {
            // Assuming a concrete implementation of IOrderRepository is available
            iorderRepository = new OrderRepository(); // Replace with actual repository instantiation
        }
        public void AddOrder(Order order)
        {
            iorderRepository.AddOrder(order);
        }
        public void DeleteOrder(int orderId)
        {
            iorderRepository.DeleteOrder(orderId);
        }
        public Order GetOrderById(int orderId)
        {
            return iorderRepository.GetOrderById(orderId);
        }
        public List<Order> GetOrders()
        {
            return iorderRepository.GetOrders();
        }
        public List<Order> SearchOrders(string searchTerm)
        {
            return iorderRepository.SearchOrders(searchTerm);
        }
        public void UpdateOrder(Order order)
        {
            iorderRepository.UpdateOrder(order);
        }
        public List<Order> GetOrdersByPeriod(DateTime startDate, DateTime endDate)
        {
            return iorderRepository.GetOrdersByPeriod(startDate, endDate);
        }
    }
}
