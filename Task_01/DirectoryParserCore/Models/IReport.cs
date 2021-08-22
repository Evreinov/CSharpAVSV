using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DirectoryParserCore.Models
{
    public interface IReport : IEnumerable
    {
        /// <summary>
        /// Добавить тип документа заданного имени.
        /// </summary>
        /// <param name="name">Имя документа.</param>
        public void AddItem(string name);

        /// <summary>
        /// Добавить тип документа.
        /// </summary>
        /// <param name="fi">Документ.</param>
        public void AddItem(FileInfo fi);

    }
}
