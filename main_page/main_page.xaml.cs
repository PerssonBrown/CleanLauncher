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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

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

        int loginMode = 0;
        string javaPath { get; set; }
        int javaMemory;
        string launchVersion { get; set; }

        public main_page()
        {
            InitializeComponent();
            // 绑定游戏版本
            //
            AllTheExistingVersion[] gameVersion = new AllTheExistingVersion[0];
            gameVersion = tools.GetAllTheExistingVersion();
            for (int i = 0; i < gameVersion.Length; i++)
            {
                LaunchVersion.Items.Add(gameVersion[i].version);
            }
        }

        #region 版本选择
        private void Offline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject[loginMode] = 1;
                reader.Close();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            catch
            {
                loginMode = 0;
            }
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/offline.xaml");
            login.Content = frame;
        }

        private void Mojang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject[loginMode] = 2;
                reader.Close();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            catch
            {
                loginMode = 0;
            }
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/mojang.xaml");
            login.Content = frame;
        }

        private void Microsoft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject[loginMode] = 3;
                reader.Close();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            catch
            {
                loginMode = 0;
            }
            Frame frame = new Frame();
            frame.Source = new Uri("pack://application:,,,/main_page/LoginUI/microsoft.xaml");
            login.Content = frame;
        }
        #endregion
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取json文件内的登陆模式
            //
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                loginMode = Convert.ToInt32(jsonObject["loginMode"]);
                reader.Close();
            }
            catch
            {
                loginMode = 0;
            }

            // 获取java路径
            //
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                javaPath = jsonObject["javaPath"].ToString();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "未选择Java版本");
            }

            // 获取java内存
            //
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                javaMemory = Convert.ToInt32(jsonObject["javaMemory"]);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "未设置Java内存");
            }

            // 获取启动版本
            //
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                launchVersion = jsonObject["launchVersion"].ToString();
                reader.Close();
            }
            catch
            {
                MessageBox.Show("请选择要启动的游戏版本");
            }

            // 启动
            //
            try
            {
                switch (loginMode)
                {
                    //case 0:
                    //    MessageBox.Show("请选择登陆方式");
                    //    break;
                    //case 1:
                    //    await game.StartGame(launchVersion, javaPath, JavaMemory, offline.playerName.Text);
                    //    break;
                    //case 2:
                    //    await game.StartGame(launchVersion, javaPath, JavaMemory, mojang.email.Text, mojang.password.Password);
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
                    //    await game.StartGame(launchVersion, javaPath, JavaMemory, v1.name, v1.uuid, v1.mcToken, string.Empty, string.Empty);
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
