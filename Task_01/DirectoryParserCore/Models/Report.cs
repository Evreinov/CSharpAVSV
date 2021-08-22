using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DirectoryParserCore.Models
{
    public class Report : IReport
    {
        // Структура данных для хранения элементов отчета.
        List<ReportItem> items = new List<ReportItem>();

        public void AddItem(string name)
        {
            ReportItem item = items.Find(i => i.Extension.ToLower() == name.ToLower());
            if (item is null)
            {
                items.Add(new ReportItem(name.ToLower()));
            }
            else
            {
                item.Count++;
            }
        }

        public void AddItem(FileInfo fi)
        {
            ReportItem item = items.Find(i => i.Extension.ToLower() == fi.Extension.ToLower());
            if (item is null)
            {
                items.Add(new ReportItem(fi.Extension.ToLower()));
            }
            else
            {
                item.Count++;
            }
        }


        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
