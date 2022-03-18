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
using SquareMinecraftLauncher.Minecraft;

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
        // 绑定 microsoft
        CleanLauncher.main_page.LoginUI.microsoft microsoft = new LoginUI.microsoft();

        CleanLauncher.settings.settings settings = new CleanLauncher.settings.settings();

        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        SquareMinecraftLauncher.Minecraft.Game game = new SquareMinecraftLauncher.Minecraft.Game();
        SquareMinecraftLauncher.MinecraftDownload minecraftDownload = new SquareMinecraftLauncher.MinecraftDownload();

        int launchMode = 0;
        public main_page()
        {
            InitializeComponent();
            AllTheExistingVersion[] gameVersion = new AllTheExistingVersion[0];
            gameVersion = tools.GetAllTheExistingVersion();
            //MessageBox.Show(gameVersion.Length.ToString());
            for (int i = 0; i < gameVersion.Length; i++)
            {
                LaunchVersion.Items.Add(gameVersion[i].version);
            }
        }

        private void Offline_Click(object sender, RoutedEventArgs e)
        {
            launchMode = 1;
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/offline.xaml");
            login.Content = frame;
        }

        private void Mojang_Click(object sender, RoutedEventArgs e)
        {
            launchMode = 2;
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/mojang.xaml");
            login.Content = frame;
        }

        private void Microsoft_Click(object sender, RoutedEventArgs e)
        {
            launchMode = 3;
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/microsoft.xaml");
            login.Content = frame;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (launchMode)
                {
                    //case 0:
                    //    MessageBox.Show("请选择登陆方式");
                    //    break;
                    //case 1:
                    //    await game.StartGame(LaunchVersion.Text, settings.JavaVersionSelect.SelectedValuePath.ToString(), Convert.ToInt32(settings.JavaMemory.Text), offline.playerName.Text);
                    //    break;
                    //case 2:
                    //    await game.StartGame(LaunchVersion.Text, settings.JavaVersionSelect.SelectedValuePath.ToString(), Convert.ToInt32(settings.JavaMemory.Text), mojang.email.Text, mojang.password.Password);
                    //    break;
                    //case 3:
                    //    microsoft_launcher.MicrosoftAPIs microsoftAPIs = new microsoft_launcher.MicrosoftAPIs();
                    //    var v = microsoft.wb.Source.ToString().Replace(microsoftAPIs.cutUri, string.Empty);
                    //    var t = Task.Run(() =>
                    //    {
                    //        return microsoftAPIs.GetAccessTokenAsync(v, false).Result;
                    //    });
                    //    await t;
                    //    var v1 = microsoftAPIs.GetAllThings(t.Result.access_token, false);
                    //    await game.StartGame(LaunchVersion.Text, settings.JavaVersionSelect.SelectedValuePath.ToString(), Convert.ToInt32(settings.JavaMemory.Text), v1.name, v1.uuid, v1.mcToken, string.Empty, string.Empty);
                    //    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
