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
        private SpecialDateList sdl;
        private String fileName = "speacialDates.xml";

        public DatePickerUI()
        {
            InitializeComponent();
            dpvm = new DatePickerViewModel();
            this.DataContext = dpvm;

            sdl = new SpecialDateList();
            sdl.LoadFile(fileName);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Test");
            Console.WriteLine("IsAnnual: {0}", dpvm.IsAnnual);

            sdl.SaveFile(fileName);
        }

        private void ButtonDayForward_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonMonthForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonYearForward_Click(object sender, RoutedEventArgs e)
        {
            dpvm.NextYear();
        }

        private void ButtonDayBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMonthBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonYearBack_Click(object sender, RoutedEventArgs e)
        {
            dpvm.PreviousYear();
        }

        private void CheckBoxSpecial_Click(object sender, RoutedEventArgs e)
        {
            dpvm.IsSpecialDay = (bool) this.CheckBoxSpecial.IsChecked;
        }

        private void CheckBoxAnnual_Click(object sender, RoutedEventArgs e)
        {
            dpvm.IsAnnual = (bool) this.CheckBoxAnnual.IsChecked;
        }
    }
}
