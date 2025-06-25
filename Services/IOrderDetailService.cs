using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailById(int orderDetailId);
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int orderDetailId);
        List<OrderDetail> SearchOrderDetails(string searchTerm);
    }
}
