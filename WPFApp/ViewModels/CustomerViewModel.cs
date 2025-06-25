using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services;
using System.Windows.Input;
using System.Windows;
using HoangLeThanhDucWPF.Commands;

namespace HoangLeThanhDucWPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        // --- Services ---
        private readonly ICustomerService icustomerService;
        private readonly IOrderService iorderService;
        private readonly IOrderDetailService iorderDetailService;

        // --- Properties ---
        public Customer CurrentCustomer { get; set; }
        public ObservableCollection<Order> CustomerOrders { get; set; }
        public ObservableCollection<OrderDetail> SelectedOrderDetails { get; set; }

        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                LoadSelectedOrderDetails();
                OnPropertyChanged();
            }
        }

        // --- Commands ---
        public ICommand SaveProfileCommand { get; private set; }

        public CustomerViewModel(Customer customer)
        {
            // Nhận thông tin customer đã đăng nhập
            CurrentCustomer = customer;

            // Khởi tạo services
            icustomerService = new CustomerService();
            iorderService = new OrderService();
            iorderDetailService = new OrderDetailService();

            // Tải lịch sử đơn hàng của customer này
            LoadOrderHistory();

            // Khởi tạo command
            SaveProfileCommand = new RelayCommand(ExecuteSaveProfile);
        }

        private void LoadOrderHistory()
        {
            var allOrders = iorderService.GetOrders();
            var ordersForThisCustomer = allOrders.Where(o => o.CustomerId == CurrentCustomer.CustomerId);
            CustomerOrders = new ObservableCollection<Order>(ordersForThisCustomer);
        }

        private void LoadSelectedOrderDetails()
        {
            if (_selectedOrder != null)
            {
                var allDetails = iorderDetailService.GetOrderDetails();
                var filteredDetails = allDetails.Where(od => od.OrderId == _selectedOrder.OrderId);
                SelectedOrderDetails = new ObservableCollection<OrderDetail>(filteredDetails);
                OnPropertyChanged(nameof(SelectedOrderDetails));
            }
        }

        private void ExecuteSaveProfile(object? obj)
        {
            icustomerService.UpdateCustomer(CurrentCustomer);
            MessageBox.Show("Your profile has been updated successfully!", "Success");
        }
    }
}
