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

namespace CleanLauncher.settings
{
    /// <summary>
    /// settings.xaml 的交互逻辑
    /// </summary>
    public partial class settings : Page
    {
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        public settings()
        {
            InitializeComponent();
            JavaVersionList.ItemsSource = tools.GetJavaPath();
            // 尝试打开json文件读取数据
            // 失败则使用默认值
            // 
            // JavaMemory数据读取
            //
            try 
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                JavaMemory.Text = jsonObject["javaMemory"].ToString(); //user ,passwd 类似
                reader.Close();
            }
            catch
            {
                JavaMemory.Text = @"2048";
            }
            // JavaVersion数据读取
            //
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                JavaVersionList.SelectedItem = JavaVersionList.Items[Convert.ToInt32(jsonObject["javaLaunchVersionIndex"])];
                reader.Close();
            }
            catch
            {
                if (JavaVersionList.Items.Count > 0)
                {
                    JavaVersionList.SelectedItem = JavaVersionList.Items[0];
                }
            }
        }

        private void JavaMemory_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject["javaMemory"] = JavaMemory.Text.ToString();
                reader.Close();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void JavaVersionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject["javaLaunchVersionPath"] = ((JavaVersion)JavaVersionList.SelectedValue).Path;
                jsonObject["javaLaunchVersionIndex"] = JavaVersionList.SelectedIndex.ToString();
                reader.Close();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
