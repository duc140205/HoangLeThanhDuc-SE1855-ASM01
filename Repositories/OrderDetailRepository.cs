using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.AddOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(int orderDetailId)
        {
            OrderDetailDAO.DeleteOrderDetail(orderDetailId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return OrderDetailDAO.GetOrderDetails();
        }

        public List<OrderDetail> SearchOrderDetails(string searchTerm)
        {
            return OrderDetailDAO.SearchOrderDetails(searchTerm);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.UpdateOrderDetail(orderDetail);
        }
        public OrderDetail GetOrderDetailById(int orderDetailId)
        {
            return OrderDetailDAO.GetOrderDetailById(orderDetailId);
        }
    }
}
