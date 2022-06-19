using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicker
{
    class SpecialDate : SpecialDateBase
    {
        private int _month;
        private int _day;
        private List<int> _years;

        public int Month
        {
            get
            {
                return _month;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }

        public List<int> Years
        {
            get
            {
                return _years;
            }
        }

        public bool IsAnnual
        {
            get
            {
                return _years.Count > 0;
            }
        }

        public String Key
        {
            get
            {
                return _getKeyFromDate(_month, _day);
            }
        }

        public void AddYear(int y)
        {
            if (!_years.Contains(y))
            {
                _years.Add(y);
            }
        }

        public void RemoveYear(int y)
        {
            _years.Remove(y);
        }

        private void _setupSpecialDate(int m, int d)
        {
            _month = m;
            _day = d;
            _years = new List<int>();
        }

        public SpecialDate(int m, int d)
        {
            _setupSpecialDate(m, d);
        }

        public SpecialDate(int m, int d, int y)
        {
            _setupSpecialDate(m, d);
            _years.Add(y);
        }
    }
}
