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
        private SpecialDateList _datesList;

        public SpecialDateList DatesList
        {
            get
            {
                return _datesList;
            }
            set
            {
                if (_datesList == null)
                {
                    _datesList = value;
                }
            }
        }

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
                return YearGoBack || _today.Month > _currentDay.Month;
            }
        }

        public Boolean DayGoBack
        {
            get
            {
                return MonthGoBack || _today.Date > _currentDay.Date;
            }
        }

        private void _changeDate(DateTime newDate)
        {
            DateTime _oldDate = _today;

            // Never go past the current day.
            if (newDate.Date > _currentDay.Date)
            {
                _today = newDate;
            }
            else
            {
                _today = _currentDay;
            }
            
            if (_today.Year > _oldDate.Year)
            {
                NotifyPropertyChanged("Year");
            }

            if (_today.Month != _oldDate.Month)
            {
                NotifyPropertyChanged("Month");
            }

            // Always calculate these when the date changes.
            NotifyPropertyChanged("DayOfWeek");
            NotifyPropertyChanged("YearGoBack");
            NotifyPropertyChanged("MonthGoBack");
            NotifyPropertyChanged("DayGoBack");
            NotifyPropertyChanged("IsSpecialDay");
            NotifyPropertyChanged("IsAnnual");
        }

        public void NextYear()
        {
            _changeDate(_today.AddYears(1));
        }

        public void NextMonth()
        {
            _changeDate(_today.AddMonths(1));
        }

        public void NextDay()
        {
            _changeDate(_today.AddDays(1));
        }

        public void PreviousYear()
        {
            _changeDate(_today.AddYears(-1));
        }

        public void PreviousMonth()
        {
            _changeDate(_today.AddMonths(-1));
        }

        public void PreviousDay()
        {
            _changeDate(_today.AddDays(-1));
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
