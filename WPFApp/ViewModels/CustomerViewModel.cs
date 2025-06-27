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

        public event Action RequestClose;

        // --- Properties ---
        public Customer CurrentCustomer { get; set; }
        public ObservableCollection<Order> CustomerOrders { get; set; }

        // Add separate property for phone input validation
        private string _phoneInput;
        public string PhoneInput
        {
            get => _phoneInput;
            set
            {
                _phoneInput = value;
                OnPropertyChanged();
                ValidatePhoneInput();
            }
        }

        private string _phoneValidationMessage;
        public string PhoneValidationMessage
        {
            get => _phoneValidationMessage;
            set
            {
                _phoneValidationMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _isPhoneValid;
        public bool IsPhoneValid
        {
            get => _isPhoneValid;
            set
            {
                _isPhoneValid = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrderDetail> _selectedOrderDetails;
        public ObservableCollection<OrderDetail> SelectedOrderDetails
        {
            get => _selectedOrderDetails;
            set { _selectedOrderDetails = value; OnPropertyChanged(); CalculateOrderTotal(); }
        }

        private double _orderTotal;
        public double OrderTotal
        {
            get => _orderTotal;
            set { _orderTotal = value; OnPropertyChanged(); }
        }

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
        public ICommand LogoutCommand { get; private set; }

        public CustomerViewModel(Customer customer)
        {
            // Nhận thông tin customer đã đăng nhập
            CurrentCustomer = customer;
            
            // Initialize phone input with current phone
            _phoneInput = customer.Phone;
            ValidatePhoneInput();

            // Khởi tạo services
            icustomerService = new CustomerService();
            iorderService = new OrderService();
            iorderDetailService = new OrderDetailService();

            // Tải lịch sử đơn hàng của customer này
            LoadOrderHistory();

            // Khởi tạo command
            SaveProfileCommand = new RelayCommand(ExecuteSaveProfile, CanExecuteSaveProfile);

            LogoutCommand = new RelayCommand(ExecuteLogout);
        }

        private void ValidatePhoneInput()
        {
            if (string.IsNullOrWhiteSpace(_phoneInput))
            {
                PhoneValidationMessage = "Phone number cannot be empty!";
                IsPhoneValid = false;
            }
            else if (!_phoneInput.All(char.IsDigit))
            {
                PhoneValidationMessage = "Phone number must contain only digits!";
                IsPhoneValid = false;
            }
            else if (_phoneInput.Length != 10)
            {
                PhoneValidationMessage = "Phone number must be exactly 10 digits!";
                IsPhoneValid = false;
            }
            else
            {
                PhoneValidationMessage = string.Empty;
                IsPhoneValid = true;
            }
        }

        private bool CanExecuteSaveProfile(object? obj)
        {
            return IsPhoneValid;
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
            }
            else
            {
                SelectedOrderDetails = new ObservableCollection<OrderDetail>();
            }
        }

        private void ExecuteSaveProfile(object? obj)
        {
            // Update the customer's phone number
            CurrentCustomer.Phone = PhoneInput;
            
            // Save to database
            icustomerService.UpdateCustomer(CurrentCustomer);
            MessageBox.Show("Your profile has been updated successfully!", "Success");
        }

        private void ExecuteLogout(object? obj) 
        {
            // Gửi yêu cầu đóng cửa sổ
            RequestClose?.Invoke();
        }

        private void CalculateOrderTotal()
        {
            if (SelectedOrderDetails != null && SelectedOrderDetails.Any())
            {
                OrderTotal = SelectedOrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - od.Discount));
            }
            else
            {
                OrderTotal = 0;
            }
        }
    }
}
