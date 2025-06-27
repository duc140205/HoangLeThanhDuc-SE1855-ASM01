using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;
using Services;

namespace HoangLeThanhDucWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Khai báo các service cần dùng
        private readonly IEmployeeService iemployeeService;
        private readonly ICustomerService icustomerService;

        public LoginWindow()
        {
            InitializeComponent();
            // Khởi tạo các service
            iemployeeService = new EmployeeService();
            icustomerService = new CustomerService();
        }


        private void Role_Checked(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem control đã được khởi tạo hay chưa
            if (lblPass == null || txtPass == null)
            {
                return;
            }

            if (rbAdmin.IsChecked == true)
            {
                // Hiện lại ô Password và đổi nhãn
                lblUser.Content = "Username:";
                lblPass.Visibility = Visibility.Visible;
                txtPass.Visibility = Visibility.Visible;
                // Admin không cần giới hạn số ký tự
                txtUser.MaxLength = 0;
            }
            else // rbCustomer được chọn
            {
                // Ẩn ô Password đi và đổi nhãn
                lblUser.Content = "Phone Number:";
                lblPass.Visibility = Visibility.Collapsed;
                txtPass.Visibility = Visibility.Collapsed;
                // Customer chỉ được nhập 10 số
                txtUser.MaxLength = 10;
            }
        }

        private void txtUser_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Chỉ kiểm tra khi đang ở chế độ Customer login
            if (rbCustomer.IsChecked == true)
            {
                // Chỉ cho phép nhập số
                if (!char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (rbAdmin.IsChecked == true)
            {
                // Đăng nhập Admin
                Employee employee = iemployeeService.Login(txtUser.Text, txtPass.Password);
                if (employee != null)
                {
                    MessageBox.Show($"Login successful! Welcome '{employee.Name}'");
                    // Mở cửa sổ Admin ở đây
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong username or password!");
                }
            }
            else // Đăng nhập Customer
            {
                string phone = txtUser.Text.Trim();
                if (!string.IsNullOrEmpty(phone) && phone.All(char.IsDigit) && phone.Length == 10)
                {
                    Customer customer = icustomerService.GetCustomerByPhone(phone);
                    if (customer != null)
                    {
                        MessageBox.Show($"Login successful! Welcome '{customer.ContactName}'");
                        // Mở cửa sổ Customer ở đây
                        var customerWindow = new CustomerWindow(customer);
                        customerWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This phone number is not registered!");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid 10-digit phone number for customer login.");
                }
            }
        }
    }
}
