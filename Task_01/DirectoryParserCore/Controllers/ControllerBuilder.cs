using DirectoryParserCore.Infrastructure;
using DirectoryParserCore.Models;
using DirectoryParserCore.Views;

namespace DirectoryParserCore.Controllers
{
    public class ControllerBuilder
    {
        Controller controller;

        public ControllerBuilder() => controller = new Controller();

        public ControllerBuilder SetReport(IReport report)
        {
            controller.Report = report;
            return this;
        }

        public ControllerBuilder SetView(IView view)
        {
            controller.View = view;
            return this;
        }

        public ControllerBuilder SetFileDirectorySearch(IFileDirectorySearch fileDirectorySearch)
        {
            controller.FileDirectorySearch = fileDirectorySearch;
            return this;
        }

        public Controller Build() => controller;
    }
}
