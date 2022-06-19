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
        private List<string> _datesToRemove;

        public List<string> DateKeys
        {
            get
            {
                List<string> keys = new List<string>();
                keys.AddRange(_specialDateList.Keys.Cast<string>());
                return keys;
            }
        }

        public List<string> DatesToRemove
        {
            get
            {
                return _datesToRemove;
            }
        }

        public SpecialDate GetDate(String key)
        {
            try
            {
                return _specialDateList[key];
            }
            catch
            {
                return null;
            }

        }

        private void AddAnnualDate(int m, int d)
        {
            SpecialDate sd = new SpecialDate(m, d);

            if (_specialDateList.Keys.Contains(sd.Key))
            {
                _specialDateList[sd.Key] = sd;
            }
        }

        private void AddSpecificDate(int m, int d, int y)
        {
            AddAnnualDate(m, d);
            SpecialDate sd = GetDate(_getKeyFromDate(m, d));

            if (!sd.Years.Contains(y))
            {
                sd.Years.Add(y);
            }

            _specialDateList[sd.Key] = sd;
        }

        private void RemoveAnnualDate(int m, int d)
        {
            _specialDateList.Remove(_getKeyFromDate(m, d));
        }

        private void RemoveSpecificDate(int m, int d, int y)
        {
            SpecialDate sd = GetDate(_getKeyFromDate(m, d));

            if (sd != null)
            {
                sd.Years.Remove(y);
                _specialDateList[sd.Key] = sd;
            }
        }

        public void SetDateToRemove(string key)
        {
            _datesToRemove.Add(key);
        }

        // Table:
        // +----------------+-------------+---------------+--------------------------------------------+
        // | newDate annual | date exists | year specific | action to take                             |
        // +----------------+-------------+---------------+--------------------------------------------+
        // | yes            | no          | no            | AddAnnualDate                              |
        // | yes            | yes         | no            | take no action                             |
        // | yes            | yes         | yes           | remove all years from SpecificDate         |
        // | no             | no          | no            | AddSpecificDate                            |
        // | no             | yes         | no            | AddSpecificDate for each year              |
        // | no             | yes         | yes           | compare years, remove past years, add year |
        // +----------------+-------------+---------------+--------------------------------------------+
        //
        // Notes:
        // newDate annual: The new date has month, day, but no year attached. 
        //                 If not annual the day does not repeat the next year.
        //                 Holidays that occur on the same day are annual, e.g. Christmas.
        //                 Holidays that occur of different days are specific, e.g. Thanksgiving.
        // date exists: SpecialDate with a key matching newDate is in _specialDateList.
        // year specific: SpecialDate matching newDate's key has years attached to it.
        public void SynchronizeDate(SpecialDate newDate)
        {
            SpecialDate existingDate = GetDate(newDate.Key);

            if (existingDate == null)
            {
                if (newDate.IsAnnual)
                {
                    AddAnnualDate(newDate.Month, newDate.Day);
                }
                else
                {
                    foreach (int y in newDate.Years)
                    {
                        AddSpecificDate(newDate.Month, newDate.Day, y);
                    }
                }
            }
            else
            {
                if (newDate.IsAnnual && !existingDate.IsAnnual)
                {
                    // Overwrite the existing specific date with an annual one.
                    _specialDateList[newDate.Key] = newDate;
                }
                else
                {
                    // Loop through the years associated with the new date.
                    foreach (int newYear in newDate.Years)
                    {
                        // Remove old years if they are in existingDate.
                        foreach (int existingYear in existingDate.Years)
                        {
                            if (existingYear < newYear)
                            {
                                existingDate.RemoveYear(existingYear);
                            }
                        }

                        // Update the existing date with any new years.
                        existingDate.AddYear(newYear);
                    }

                    // Once the years have been synchrnized, overwrite the old entry.
                    _specialDateList[existingDate.Key] = existingDate;
                }
            }
        }

        // TODO: Remove dates that were special.
        public void SynchronizeLists(SpecialDateList newList)
        {
            // Remove existing dates that were unset as special.
            foreach (String key in newList.DatesToRemove)
            {
                _specialDateList.Remove(key);
            }

            // Merge new dates with the existing ones.
            foreach (String key in newList.DateKeys)
            {
                SynchronizeDate(newList.GetDate(key));
            }
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

            // TODO: Look into with/as pattern like you do in Python.
            fs.Close();
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
            _datesToRemove = new List<string>();
        }
    }
}
