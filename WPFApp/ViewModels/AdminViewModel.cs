using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services;
using System.Windows;

namespace HoangLeThanhDucWPF.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly ICustomerService icustomerService;
        private readonly IProductService iproductService;
        private readonly IOrderService iorderService;
        private readonly IOrderDetailService iorderDetailService;

        // --- Properties for Data Binding ---
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


        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(); }
        }

        private ObservableCollection<OrderDetail> _selectedOrderDetails;
        public ObservableCollection<OrderDetail> SelectedOrderDetails
        {
            get => _selectedOrderDetails;
            set { _selectedOrderDetails = value; OnPropertyChanged(); }
        }





        // --- Search Properties ---
        #region Search Properties
        // Thuộc tính để binding với ô TextBox tìm kiếm Customer
        private string _searchCustomerText = "";
        public string SearchCustomerText
        {
            get => _searchCustomerText;
            set { _searchCustomerText = value; OnPropertyChanged(); }
        }

        // Thuộc tính để binding với ô TextBox tìm kiếm Product
        private string _searchProductText = "";
        public string SearchProductText
        {
            get => _searchProductText;
            set { _searchProductText = value; OnPropertyChanged(); }
        }
        #endregion


        #region Report Properties
        // Gán giá trị mặc định cho ngày bắt đầu và kết thúc
        public DateTime ReportStartDate { get; set; } = DateTime.Now.AddMonths(-1); // Mặc định là 1 tháng trước
        public DateTime ReportEndDate { get; set; } = DateTime.Now; // Mặc định là hôm nay
        #endregion

        

        // Selected Item Properties
        #region Selected Item Properties
        // Thuộc tính để binding với item được chọn trong bảng Customer
        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        // Thuộc tính để binding với item được chọn trong bảng Product
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                // Khi một Order được chọn, tải các OrderDetail tương ứng
                LoadSelectedOrderDetails();
                OnPropertyChanged();
            }
        }
        #endregion


        // --- Commands ---
        #region Commands cho Product và Customer
        public ICommand DeleteCustomerCommand { get; private set; }
        public ICommand DeleteProductCommand { get; private set; }

        public ICommand AddCustomerCommand { get; private set; }
        public ICommand UpdateCustomerCommand { get; private set; }
      
        public ICommand AddProductCommand { get; private set; }
        public ICommand UpdateProductCommand { get; private set; }

        public ICommand SearchCustomerCommand { get; private set; }
        public ICommand SearchProductCommand { get; private set; }

        #endregion

        public ICommand GenerateReportCommand { get; private set; }


        public AdminViewModel()
        {
            icustomerService = new CustomerService();
            iproductService = new ProductService();
            iorderService = new OrderService();
            iorderDetailService = new OrderDetailService();
            LoadData();

            // Khởi tạo các Command và trỏ chúng tới các phương thức xử lý
            SearchCustomerCommand = new RelayCommand(ExecuteSearchCustomer);
            SearchProductCommand = new RelayCommand(ExecuteSearchProduct);


            // Khởi tạo Delete Commands
            DeleteCustomerCommand = new RelayCommand(ExecuteDeleteCustomer, CanExecuteUpdateOrDelete);
            DeleteProductCommand = new RelayCommand(ExecuteDeleteProduct, CanExecuteUpdateOrDelete);


            // Khởi tạo Add và Update Commands
            AddCustomerCommand = new RelayCommand(ExecuteAddCustomer);
            UpdateCustomerCommand = new RelayCommand(ExecuteUpdateCustomer, CanExecuteUpdateOrDelete);

            // Khởi tạo các Command cho Product
            AddProductCommand = new RelayCommand(ExecuteAddProduct);
            UpdateProductCommand = new RelayCommand(ExecuteUpdateProduct, CanExecuteUpdateOrDeleteProduct);


            GenerateReportCommand = new RelayCommand(ExecuteGenerateReport);
        }

        private void LoadData()
        {
            Customers = new ObservableCollection<Customer>(icustomerService.GetCustomers());
            Products = new ObservableCollection<Product>(iproductService.GetProducts());
            Orders = new ObservableCollection<Order>(iorderService.GetOrders());
        }

        private void LoadSelectedOrderDetails()
        {
            if (SelectedOrder != null)
            {
                // Lấy tất cả OrderDetail và lọc ra những cái có OrderId trùng khớp
                var allDetails = iorderDetailService.GetOrderDetails();
                var filteredDetails = allDetails.Where(od => od.OrderId == SelectedOrder.OrderId);
                SelectedOrderDetails = new ObservableCollection<OrderDetail>(filteredDetails);
            }
            else
            {
                // Nếu không có order nào được chọn, làm trống danh sách details
                SelectedOrderDetails = new ObservableCollection<OrderDetail>();
            }
        }

        // --- Các phương thức thực thi Command ---
        private void ExecuteSearchCustomer(object? obj)
        {
            // Cập nhật lại danh sách Customers dựa trên từ khóa tìm kiếm
            Customers = new ObservableCollection<Customer>(icustomerService.SearchCustomers(SearchCustomerText));
        }

        private void ExecuteSearchProduct(object? obj)
        {
            // Cập nhật lại danh sách Products dựa trên từ khóa tìm kiếm
            Products = new ObservableCollection<Product>(iproductService.SearchProducts(SearchProductText));
        }



        #region Delete Command Methods

        // Phương thức kiểm tra điều kiện để thực thi lệnh Delete or Update
        private bool CanExecuteUpdateOrDelete(object? obj)
        {
            return SelectedCustomer != null;
        }


        // Hành động khi nhấn nút Delete Customer
        private void ExecuteDeleteCustomer(object? obj)
        {
            if (SelectedCustomer == null) return;

            // Hiển thị hộp thoại xác nhận
            if (MessageBox.Show($"Are you sure you want to delete '{SelectedCustomer.ContactName}'?",
                                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                icustomerService.DeleteCustomer(SelectedCustomer.CustomerId);
                LoadData(); // Tải lại dữ liệu
                MessageBox.Show("Customer deleted successfully.", "Success");
            }
        }

        // Hành động khi nhấn nút Delete Product
        private void ExecuteDeleteProduct(object? obj)
        {
            if (SelectedProduct == null) return;

            if (MessageBox.Show($"Are you sure you want to delete '{SelectedProduct.ProductName}'?",
                                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                iproductService.DeleteProduct(SelectedProduct.ProductId);
                LoadData(); // Tải lại dữ liệu
                MessageBox.Show("Product deleted successfully.", "Success");
            }
        }

        #endregion


        #region Add/Update Command Methods

        private void ExecuteAddCustomer(object? obj)
        {
            var newCustomer = new Customer(0, "", "", "", "", 0); // Tạo customer rỗng
            var vm = new CustomerDetailViewModel(newCustomer);
            var detailWindow = new CustomerDetailWindow(vm);

            if (detailWindow.ShowDialog() == true)
            {
                icustomerService.AddCustomer(vm.Customer);
                LoadData(); // Tải lại danh sách
                MessageBox.Show("Customer added successfully.", "Success");
            }
        }

        private void ExecuteUpdateCustomer(object? obj)
        {
            if (SelectedCustomer == null) return;

            // Tạo một bản sao của customer để chỉnh sửa, tránh ảnh hưởng trực tiếp đến list
            var customerToUpdate = (Customer)SelectedCustomer.Clone();
            var vm = new CustomerDetailViewModel(customerToUpdate);
            var detailWindow = new CustomerDetailWindow(vm);

            if (detailWindow.ShowDialog() == true)
            {
                icustomerService.UpdateCustomer(vm.Customer);
                LoadData(); // Tải lại danh sách
                MessageBox.Show("Customer updated successfully.", "Success");
            }
        }

        #endregion


        #region Product Command Methods

        private bool CanExecuteUpdateOrDeleteProduct(object? obj)
        {
            return SelectedProduct != null;
        }

        private void ExecuteAddProduct(object? obj)
        {
            var newProduct = new Product { ProductId = 0, ProductName = "", CategoryId = 0, UnitPrice = 0, UnitsInStock = 0 };
            var vm = new ProductDetailViewModel(newProduct);
            var detailWindow = new ProductDetailWindow(vm);

            if (detailWindow.ShowDialog() == true)
            {
                iproductService.AddProduct(vm.Product);
                LoadData();
                MessageBox.Show("Product added successfully.", "Success");
            }
        }

        private void ExecuteUpdateProduct(object? obj)
        {
            if (SelectedProduct == null) return;

            var productToUpdate = (Product)SelectedProduct.Clone();
            var vm = new ProductDetailViewModel(productToUpdate);
            var detailWindow = new ProductDetailWindow(vm);

            if (detailWindow.ShowDialog() == true)
            {
                iproductService.UpdateProduct(vm.Product);
                LoadData();
                MessageBox.Show("Product updated successfully.", "Success");
            }
        }

        #endregion

        #region Report Command Methods
        private void ExecuteGenerateReport(object? obj)
        {
            // Kiểm tra nếu ngày kết thúc nhỏ hơn ngày bắt đầu
            if (ReportEndDate < ReportStartDate)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Gọi service để lấy dữ liệu đã lọc và sắp xếp
            var reportData = iorderService.GetOrdersByPeriod(ReportStartDate, ReportEndDate);

            // Cập nhật lại danh sách Orders để hiển thị trên DataGrid
            Orders = new ObservableCollection<Order>(reportData);

            MessageBox.Show($"Report generated successfully. Found {reportData.Count} orders.", "Success");
        }
        #endregion
    }
}
