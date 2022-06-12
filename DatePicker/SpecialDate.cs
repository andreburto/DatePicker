using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicker
{
    class SpecialDate
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

        private String Key
        {
            get
            {
                return String.Format("{0}{1}", _prependZero(_month), _prependZero(_day));
            }
        }

        public void AddYear(int y)
        {
            _years.Add(y);
        }

        private String _prependZero(int n)
        {
            string TwoDigits = n.ToString();
            if (n < 10)
            {
                TwoDigits = String.Format("0{0}", TwoDigits);
            }
            return TwoDigits;
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
