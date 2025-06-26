using System;
using System.Globalization;
using System.Windows.Data;

namespace HoangLeThanhDucWPF
{
    public class OrderDetailTotalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 3 && 
                values[0] is double unitPrice && 
                values[1] is int quantity && 
                values[2] is double discount)
            {
                return (unitPrice * quantity) * (1 - discount);
            }
            return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}