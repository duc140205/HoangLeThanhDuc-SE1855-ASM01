using System.Windows;
using HoangLeThanhDucWPF.ViewModels;

namespace HoangLeThanhDucWPF
{
    public partial class OrderDetailWindow : Window
    {
        public OrderDetailViewModel ViewModel { get; private set; }

        public OrderDetailWindow(OrderDetailViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = viewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ValidateOrder())
            {
                DialogResult = true;
                Close();
            }
        }
    }
}