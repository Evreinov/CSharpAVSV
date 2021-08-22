using DirectoryParserCore.Models;
using System;

namespace DirectoryParserCore.Infrastructure
{
    public interface IFileDirectorySearch
    {
        Action<string> MessageTarget { get; set; }
        IReport TraverseTree(string path);
    }
}
