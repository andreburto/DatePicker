using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicker
{
    class DatePickerViewModel : INotifyPropertyChanged
    {
        public DateTime _today;
        public DateTime _currentDay;

        public String Year
        {
            get
            {
                return this._today.ToString("yyyy");
            }
        }

        public String Month
        {
            get
            {
                return this._today.ToString("MMMM");
            }
        }

        public String DayOfWeek
        {
            get
            {
                return this._today.ToString("ddd dd");
            }
        }

        public Boolean YearGoBack
        {
            get
            {
                return _currentDay.Year > _today.Year;
            }
        }

        public Boolean MonthGoBack
        {
            get
            {
                return _currentDay.Month > _today.Month;
            }
        }

        public Boolean DayGoBack
        {
            get
            {
                return _currentDay.Date > _today.Date;
            }
        }

        public DatePickerViewModel()
        {
            _today = DateTime.Now;
            _currentDay = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
