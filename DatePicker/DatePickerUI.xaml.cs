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

            sdl = new SpecialDateList();
            Console.WriteLine("DatePickerUI: before load, {0}", sdl.DateKeys.Count);
            sdl.LoadFile(fileName);
            Console.WriteLine("DatePickerUI: after load, {0}", sdl.DateKeys.Count);

            dpvm = new DatePickerViewModel(sdl);
            this.DataContext = dpvm;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dpvm.UpdateList();

            Console.WriteLine("Test");
            Console.WriteLine("Window_Closed: {0}", dpvm.DatesList.DatesToRemove.ToString());

            sdl.SynchronizeLists(dpvm.DatesList);

            sdl.SaveFile(fileName);
        }

        private void ButtonDayForward_Click(object sender, RoutedEventArgs e)
        {
            dpvm.NextDay();
        }

        private void ButtonMonthForward_Click(object sender, RoutedEventArgs e)
        {
            dpvm.NextMonth();
        }

        private void ButtonYearForward_Click(object sender, RoutedEventArgs e)
        {
            dpvm.NextYear();
        }

        private void ButtonDayBack_Click(object sender, RoutedEventArgs e)
        {
            dpvm.PreviousDay();
        }

        private void ButtonMonthBack_Click(object sender, RoutedEventArgs e)
        {
            dpvm.PreviousMonth();
        }

        private void ButtonYearBack_Click(object sender, RoutedEventArgs e)
        {
            dpvm.PreviousYear();
        }

        private void CheckBoxSpecial_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CheckBoxSpecial: {0}", this.CheckBoxSpecial.IsChecked);
            dpvm.IsSpecialDay = (bool) this.CheckBoxSpecial.IsChecked;
        }

        private void CheckBoxAnnual_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CheckBoxAnnual: {0}", this.CheckBoxAnnual.IsChecked);
            dpvm.IsAnnual = (bool) this.CheckBoxAnnual.IsChecked;
        }
    }
}
