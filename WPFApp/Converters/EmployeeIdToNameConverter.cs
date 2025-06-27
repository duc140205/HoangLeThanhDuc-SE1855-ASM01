using System;
using System.Globalization;
using System.Windows.Data;
using DataAccessLayer;

namespace HoangLeThanhDucWPF
{
    public class EmployeeIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int employeeId)
            {
                var employee = EmployeeDAO.GetEmployeeById(employeeId);
                return employee?.Name ?? "Unknown Employee";
            }
            return "Unknown Employee";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}