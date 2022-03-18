using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Panuon.UI.Silver;
using SquareMinecraftLauncher;
using System.IO;

namespace CleanLauncher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        // 链接 main_page
        main_page.main_page main_page = new main_page.main_page();
        // 链接 download
        downloader.downloader downloader = new downloader.downloader();
        // 链接 about
        about.about about = new about.about();
        // 链接 settings
        settings.settings settings = new settings.settings();

        // Launcher Data
        public string javaPath { get; set; }
        public MainWindow()
        {
            if (!File.Exists(System.IO.Path.GetTempPath() + @"\KEY.Square"))
            {
                FileStream fileStream = File.Create(System.IO.Path.GetTempPath() + @"\KEY.Square");
                fileStream.Write(Properties.Resources.KEY, 0, Properties.Resources.KEY.Length);
                fileStream.Dispose();
                fileStream.Close();
            }
            ResizeMode = System.Windows.ResizeMode.CanMinimize; // 禁止最大化
            this.Closed += delegate { Environment.Exit(0); };
            InitializeComponent();
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/main_page.xaml");
            CtrlWindow.Content = frame;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/about/about.xaml");
            CtrlWindow.Content = frame;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/settings/settings.xaml");
            CtrlWindow.Content = frame;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/downloader/downloader.xaml");
            CtrlWindow.Content = frame;
        }
    }
}
