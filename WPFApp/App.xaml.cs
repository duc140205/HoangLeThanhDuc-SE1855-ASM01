using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Tạo một CultureInfo cho tiếng Việt
            var culture = new CultureInfo("vi-VN");

            // Cài đặt culture này cho luồng hiện tại của ứng dụng
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Cài đặt culture cho toàn bộ Framework (WPF) để các control như DatePicker hiểu đúng
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }
    }

}
