using System.Windows;
using DirectoryParserCore.Views;
using DirectoryParserCore.Controllers;
using DirectoryParserCore.Models;
using System.Windows.Forms;
using System.IO;
using DirectoryParserCore.Infrastructure;

namespace DirectoryParserWpf
{
    public partial class MainWindow : Window, IWpfView
    {
        Controller Controller { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Controller = new ControllerBuilder()
                .SetView(this)
                .SetReport(new Report())
                .SetFileDirectorySearch(new FileDirectorySearch())
                .Build();

            Controller.FileDirectorySearch.MessageTarget += Message;

            btnResult.Click += delegate { Controller.ButtonClick(); };

            btnSelectFolder.Click += delegate
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        tbPath.Text = fbd.SelectedPath;
                    }
                }
            };
        }

        public string Path => tbPath.Text;

        public IReport Report { set => dgResult.ItemsSource = value; }

        private void Message(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }
    }
}
