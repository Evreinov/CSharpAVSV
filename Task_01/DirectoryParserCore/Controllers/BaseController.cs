using DirectoryParserCore.Infrastructure;
using DirectoryParserCore.Models;
using DirectoryParserCore.Views;

namespace DirectoryParserCore.Controllers
{
    public abstract class BaseController
    {
        public virtual IView View { get; set; }
        public virtual IReport Report { get; set; }
        public virtual IFileDirectorySearch FileDirectorySearch { get; set; }
        public abstract void ButtonClick();
    }
}
