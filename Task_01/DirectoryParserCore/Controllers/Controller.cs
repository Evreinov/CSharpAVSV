using DirectoryParserCore.Infrastructure;

namespace DirectoryParserCore.Controllers
{
    public class Controller : BaseController
    {
        public override void ButtonClick()
        {
            var path = View.Path;
            View.Report = FileDirectorySearch.TraverseTree(path);
        }
    }
}
