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

namespace CleanLauncher.main_page
{
    /// <summary>
    /// main_page.xaml 的交互逻辑
    /// </summary>
    public partial class main_page : Page
    {
        // 绑定 offline
        CleanLauncher.main_page.LoginUI.offline offline = new LoginUI.offline();
        // 绑定 mojang
        CleanLauncher.main_page.LoginUI.mojang mojang = new LoginUI.mojang();
        public main_page()
        {
            InitializeComponent();
        }

        private void Offline_Click(object sender, RoutedEventArgs e)
        {
            login.Content = new Frame
            {
                Content = offline
            };
        }

        private void Mojang_Click(object sender, RoutedEventArgs e)
        {
            login.Content = new Frame
            {
                Content = mojang
            };
        }
    }
}
