using System;
using System.Text;

namespace DatePicker
{
    class SpecialDateBase
    {
        protected String _getKeyFromDate(int m, int d)
        {
            return String.Format("{0}{1}", _prependZero(m), _prependZero(d));
        }

        protected String _prependZero(int n)
        {
            string TwoDigits = n.ToString();
            if (n < 10)
            {
                TwoDigits = String.Format("0{0}", TwoDigits);
            }
            return TwoDigits;
        }
    }
}
