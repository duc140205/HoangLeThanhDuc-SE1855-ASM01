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
using HoangLeThanhDucWPF.ViewModels;
namespace HoangLeThanhDucWPF
{
    /// <summary>
    /// Interaction logic for CustomerDetailWindow.xaml
    /// </summary>
    public partial class CustomerDetailWindow : Window
    {
         public CustomerDetailWindow(CustomerDetailViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get the CustomerDetailViewModel from DataContext
            if (this.DataContext is CustomerDetailViewModel viewModel)
            {
                // Validate phone number before saving
                if (string.IsNullOrWhiteSpace(viewModel.Customer.Phone))
                {
                    MessageBox.Show("Phone number cannot be empty!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!viewModel.Customer.Phone.All(char.IsDigit))
                {
                    MessageBox.Show("Phone number must contain only digits!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (viewModel.Customer.Phone.Length != 10)
                {
                    MessageBox.Show("Phone number must be exactly 10 digits!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Báo cho cửa sổ cha biết là người dùng đã nhấn Save
            this.DialogResult = true;
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
