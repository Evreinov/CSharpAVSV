using DirectoryParserCore.Models;
using System;
using System.Collections.Generic;

namespace DirectoryParserCore.Infrastructure
{
    public class FileDirectorySearch : IFileDirectorySearch
    {
        public Action<string> MessageTarget { get;  set; }

        public IReport TraverseTree(string path)
        {
            // Сам отчёт.
            Report report = new Report();

            // Структура данных для хранения имен вложенных папок.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(path))
            {
                MessageTarget?.Invoke(new ArgumentException().Message);
                return null;
            }
            dirs.Push(path);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;

                try
                {
                    // Получить вложенные папки в текущей директории.
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                // Исключение! Нет доступа.
                catch (UnauthorizedAccessException e)
                {
                    MessageTarget?.Invoke(e.Message);
                    continue;
                }
                // Исключение! Во время выполнения, была удалена или перемещена текущая директория.
                catch (System.IO.DirectoryNotFoundException e)
                {
                    MessageTarget?.Invoke(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    // Получить вложенные файлы в текущей директории.
                    files = System.IO.Directory.GetFiles(currentDir);
                }
                // Исключение! Нет доступа.
                catch (UnauthorizedAccessException e)
                {

                    MessageTarget?.Invoke(e.Message);
                    continue;
                }
                // Исключение! Во время выполнения, была удалена или перемещена текущая директория.
                catch (System.IO.DirectoryNotFoundException e)
                {
                    MessageTarget?.Invoke(e.Message);
                    continue;
                }

                // Проводим необходимые действия с файлами.
                foreach (string file in files)
                {
                    try
                    {

                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        report.AddItem(fi);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // Исключение! Удален текущий файл, продолжить выполнение.
                        MessageTarget?.Invoke(e.Message);
                        continue;
                    }
                }

                // Добавляем найденные папки в стек для последующего прохода.
                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                    report.AddItem("folder");
                }
            }
            return report;
        }
    }
}
