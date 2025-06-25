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
            // Báo cho cửa sổ cha biết là người dùng đã nhấn Save
            this.DialogResult = true;
        }
    }
}
