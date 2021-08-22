using System.Drawing;

namespace DirectoryParserCore.Models
{
    /// <summary>
    /// Базовый интерфейс элемента отчета.
    /// </summary>
    public interface IReportItem
    {
        /// <summary>
        /// Тип документа (расширение файла).
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Количество документов заданного типа.
        /// </summary>
        public int Count { get; set; }
    }
}
