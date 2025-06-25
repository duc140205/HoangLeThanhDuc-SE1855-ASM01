using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository iorderDetailRepository;
        public OrderDetailService()
        {
            // Assuming a concrete implementation of IOrderDetailRepository is available
            iorderDetailRepository = new OrderDetailRepository(); // Replace with actual repository instantiation
        }
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            iorderDetailRepository.AddOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(int orderDetailId)
        {
            iorderDetailRepository.DeleteOrderDetail(orderDetailId);
        }

        public OrderDetail GetOrderDetailById(int orderDetailId)
        {
            return iorderDetailRepository.GetOrderDetailById(orderDetailId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return iorderDetailRepository.GetOrderDetails();
        }

        public List<OrderDetail> SearchOrderDetails(string searchTerm)
        {
            return iorderDetailRepository.SearchOrderDetails(searchTerm);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            iorderDetailRepository.UpdateOrderDetail(orderDetail);
        }
    }
}
