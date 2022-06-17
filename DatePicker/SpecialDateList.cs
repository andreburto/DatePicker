using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DatePicker
{
    class SpecialDateList : SpecialDateBase
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

        public void RemoveAnnualDate(int m, int d)
        {

        }

        public void RemoveSpecificDate(int m, int d, int y)
        {

        }

        // TODO: Clear out the list before loading
        public void LoadFile(String file)
        {
            if (!File.Exists(file))
            {
                return;
            }

            StreamReader fs = new StreamReader(file);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fs.ReadToEnd());

            foreach (XmlNode node in doc.SelectNodes("/dates/date"))
            {
                int d = int.Parse(node["day"].InnerText);
                int m = int.Parse(node["month"].InnerText);
                SpecialDate sd = new SpecialDate(m, d);

                foreach (XmlNode year in node.SelectNodes("/years/year"))
                {
                    sd.AddYear(int.Parse(year.InnerText));
                }

                _specialDateList.Add(sd.Key, sd);
            }
        }

        public void SaveFile(String file)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode datesNode = doc.CreateElement("dates");
            doc.AppendChild(datesNode);

            foreach (SpecialDate sd in _specialDateList.Values)
            {
                XmlNode dateNode = doc.CreateElement("date");

                XmlNode dayNode = doc.CreateElement("day");
                dayNode.InnerText = sd.Day.ToString();
                dateNode.AppendChild(dayNode);

                XmlNode monthNode = doc.CreateElement("month");
                monthNode.InnerText = sd.Month.ToString();
                dateNode.AppendChild(monthNode);

                if (sd.Years.Count > 0)
                {
                    XmlNode yearsNode = doc.CreateElement("years");
                    dateNode.AppendChild(yearsNode);

                    foreach (int y in sd.Years)
                    {
                        XmlNode yearNode = doc.CreateElement("year");
                        yearNode.InnerText = y.ToString();
                        yearsNode.AppendChild(yearNode);
                    }
                }

                datesNode.AppendChild(dateNode);
            }

            // Save that file.
            doc.Save(file);
        }

        public SpecialDateList()
        {
            _specialDateList = new Dictionary<string, SpecialDate>();
        }
    }
}
