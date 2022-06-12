using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicker
{
    class SpecialDateList
    {
        private Dictionary<String, SpecialDate> _specialDateList;

        public SpecialDate GetDate(String key)
        {
            return _specialDateList[key];
        }

        public void AddAnnualDate(int m, int d)
        {

        }

        public void AddSpecificDate(int m, int d, int y)
        {

        }

        public void LoadFile(String file)
        {

        }

        public void SaveFile(String file)
        {

        }

        public SpecialDateList()
        {
            _specialDateList = new Dictionary<string, SpecialDate>();
        }
    }
}
