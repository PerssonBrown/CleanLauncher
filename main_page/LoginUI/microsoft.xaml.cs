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
using microsoft_launcher;

namespace CleanLauncher.main_page.LoginUI
{
    /// <summary>
    /// microsoft.xaml 的交互逻辑
    /// </summary>
    public partial class microsoft : Page
    {
        public microsoft()
        {
            InitializeComponent();
            MicrosoftAPIs microsoftAPIs = new MicrosoftAPIs();
            microsoftAPIs.SuppressWininetBehavior();
            wb.Source = microsoftAPIs.loginWebsite;
        }
    }
}
