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

        public void UpdateList()
        {
            SpecialDate sd = _datesList.GetDate(_today.Month, _today.Day);
            if (this.IsSpecialDay)
            {
                if (this.IsAnnual)
                {
                    sd = new SpecialDate(_today.Month, _today.Day);
                }
                else
                {
                    sd = new SpecialDate(_today.Month, _today.Day, _today.Year);
                }
                _datesList.SynchronizeDate(sd);
                Console.WriteLine("UpdateList: {0}", sd.ToString());
            }
            else
            {
                if (sd != null)
                {
                    _datesList.SetDateToRemove(sd.Key);
                }
                Console.WriteLine("UpdateList: No date");
            }
            Console.WriteLine("UpdateList: {0}", _datesList.DateKeys.Count);
        }

        private void _updateCheckBoxes(SpecialDate sd)
        {
            if (sd == null)
            {
                Console.WriteLine("_updateCheckBoxes: has date");
                _specialDay = false;
                _annual = false;
            }
            else
            {
                Console.WriteLine("_updateCheckBoxes: no date");
                _specialDay = true;
                _annual = sd.IsAnnual;
            }

            Console.WriteLine("_updateCheckBoxes: {0}", IsSpecialDay);

            NotifyPropertyChanged("IsSpecialDay");
            NotifyPropertyChanged("IsAnnual");
        }

        private void _changeDate(DateTime newDate)
        {
            Console.WriteLine("newDate: {0}-{1}", newDate.Month, newDate.Day);
            DateTime _oldDate = _today;
            
            // Update Special Day list
            UpdateList();

            // Never go past the current day.
            if (newDate.Date > _currentDay.Date)
            {
                _today = newDate;
            }
            else
            {
                _today = _currentDay;
            }

            if (_today.Month != _oldDate.Month)
            {
                NotifyPropertyChanged("Month");
            }
            
            if (_today.Year > _oldDate.Year)
            {
                NotifyPropertyChanged("Year");
            }

            // Always calculate these when the date changes.
            NotifyPropertyChanged("Year");
            NotifyPropertyChanged("Month");
            NotifyPropertyChanged("DayOfWeek");
            NotifyPropertyChanged("YearGoBack");
            NotifyPropertyChanged("MonthGoBack");
            NotifyPropertyChanged("DayGoBack");

            // Update checkboxes
            SpecialDate newSpecialDay = _datesList.GetDate(_today.Month, _today.Day);
            _updateCheckBoxes(newSpecialDay);
            Console.WriteLine("newSpecialDay: {0}", newSpecialDay);
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

        public DatePickerViewModel(SpecialDateList sdl)
        {
            // Setup the object
            _today = DateTime.Now;
            _currentDay = DateTime.Now;
            _datesList = sdl;

            // Tweak the UI check boxes
            SpecialDate sd = _datesList.GetDate(_today.Day, _today.Month);
            _updateCheckBoxes(sd);
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
