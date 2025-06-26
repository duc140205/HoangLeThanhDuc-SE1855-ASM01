using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BusinessObjects;
using Services;
using HoangLeThanhDucWPF.Commands;
using System.Windows;

namespace HoangLeThanhDucWPF.ViewModels
{
    public class OrderDetailViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public string Title { get; set; }
        public Order Order { get; set; }
        
        private ObservableCollection<OrderDetail> _orderDetails;
        public ObservableCollection<OrderDetail> OrderDetails
        {
            get => _orderDetails;
            set { _orderDetails = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        private OrderDetail _selectedOrderDetail;
        public OrderDetail SelectedOrderDetail
        {
            get => _selectedOrderDetail;
            set { _selectedOrderDetail = value; OnPropertyChanged(); }
        }

        // Properties for new order detail
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        private double _discount = 0;
        public double Discount
        {
            get => _discount;
            set { _discount = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand AddOrderDetailCommand { get; private set; }
        public ICommand RemoveOrderDetailCommand { get; private set; }

        public OrderDetailViewModel(Order order)
        {
            _customerService = new CustomerService();
            _productService = new ProductService();

            Order = order;
            Title = order.OrderId == 0 ? "Create New Order" : "Edit Order";

            // Load data
            LoadData();

            // Initialize order details
            OrderDetails = new ObservableCollection<OrderDetail>();

            // Initialize commands
            AddOrderDetailCommand = new RelayCommand(ExecuteAddOrderDetail, CanExecuteAddOrderDetail);
            RemoveOrderDetailCommand = new RelayCommand(ExecuteRemoveOrderDetail, CanExecuteRemoveOrderDetail);
        }

        private void LoadData()
        {
            Customers = new ObservableCollection<Customer>(_customerService.GetCustomers());
            Products = new ObservableCollection<Product>(_productService.GetProducts());
        }

        private bool CanExecuteAddOrderDetail(object parameter)
        {
            return SelectedProduct != null && Quantity > 0;
        }

        private void ExecuteAddOrderDetail(object parameter)
        {
            if (SelectedProduct == null) return;

            // Validate discount percentage (should be between 0 and 100)
            if (Discount < 0 || Discount > 100)
            {
                MessageBox.Show("Discount percentage must be between 0 and 100.", "Invalid Discount");
                return;
            }

            // Convert discount from percentage (3) to decimal (0.03)
            double discountDecimal = Discount / 100.0;

            // Check if product already exists in order details
            var existingDetail = OrderDetails.FirstOrDefault(od => od.ProductId == SelectedProduct.ProductId);
            if (existingDetail != null)
            {
                // Update existing quantity
                existingDetail.Quantity += Quantity;
                existingDetail.Discount = discountDecimal;
            }
            else
            {
                // Add new order detail
                var newDetail = new OrderDetail
                {
                    ProductId = SelectedProduct.ProductId,
                    UnitPrice = SelectedProduct.UnitPrice,
                    Quantity = Quantity,
                    Discount = discountDecimal
                };
                OrderDetails.Add(newDetail);
            }

            // Reset form
            SelectedProduct = null;
            Quantity = 1;
            Discount = 0;
        }

        private bool CanExecuteRemoveOrderDetail(object parameter)
        {
            return SelectedOrderDetail != null;
        }

        private void ExecuteRemoveOrderDetail(object parameter)
        {
            if (SelectedOrderDetail != null)
            {
                OrderDetails.Remove(SelectedOrderDetail);
            }
        }

        public bool ValidateOrder()
        {
            if (Order.CustomerId <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error");
                return false;
            }

            if (OrderDetails.Count == 0)
            {
                MessageBox.Show("Please add at least one product to the order.", "Validation Error");
                return false;
            }

            return true;
        }
    }
}