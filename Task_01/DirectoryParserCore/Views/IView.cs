using DirectoryParserCore.Models;

namespace DirectoryParserCore.Views
{
    public interface IView
    {
        string Path { get; }
        IReport Report { set; }
    }
}
