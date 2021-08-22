using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DirectoryParserCore.Models
{
    public class ReportItem : IReportItem
    {
        public string Extension { get; }
        public Image AssociatedIcon { get; }
        public int Count { get; set; } = 1;

        public ReportItem(string extension)
        {
            Extension = extension;
        }
    }
}
