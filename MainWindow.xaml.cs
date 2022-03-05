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

namespace CleanLauncher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        // 链接 main_page
        main_page.main_page main_page = new main_page.main_page();
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = System.Windows.ResizeMode.CanMinimize; // 禁止最大化
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            CtrlWindow.Content = new Frame()
            {
                Content = main_page
            };
        }
    }
}
