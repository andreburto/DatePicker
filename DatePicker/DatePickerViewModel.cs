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
        private DateTime _today;
        private DateTime _currentDay;
        private bool _annual;
        private bool _specialDay;
        private SpecialDate sd;

        public bool IsAnnual
        {
            get
            {
                return _annual;
            }
            set
            {
                _annual = value;
                
            }
        }

        public bool IsSpecialDay
        {
            get
            {
                return _specialDay;
            }
            set
            {
                _specialDay = value;
            }
        }

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
                return _today.Year > _currentDay.Year;
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

        private void _changeYear(int n)
        {
            _today = _today.AddYears(n);
            NotifyPropertyChanged("Year");
            NotifyPropertyChanged("YearGoBack");
        }

        public void NextYear()
        {
            _changeYear(1);
        }

        public void PreviousYear()
        {
            _changeYear(-1);
        }

        public DatePickerViewModel()
        {
            _today = DateTime.Now;
            _currentDay = DateTime.Now;
            _annual = false;
            _specialDay = false;
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
