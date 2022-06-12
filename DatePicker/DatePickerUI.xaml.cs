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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatePicker
{
    /// <summary>
    /// Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePickerUI : Window
    {
        private DatePickerViewModel dpvm;

        public DatePickerUI()
        {
            InitializeComponent();
            dpvm = new DatePickerViewModel();
            this.DataContext = dpvm;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Test");
            Console.WriteLine(dpvm._today.ToString());
        }

        private void ButtonDayForward_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonMonthForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonYearForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDayBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMonthBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonYearBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxSpecial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxAnnual_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
