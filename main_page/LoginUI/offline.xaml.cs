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

namespace CleanLauncher.main_page.LoginUI
{
    /// <summary>
    /// offline.xaml 的交互逻辑
    /// </summary>
    public partial class offline : Page
    {
        public offline()
        {
            InitializeComponent();
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                playerName.Text = jsonObject["offlinePlayerName"].ToString();
                reader.Close();
            }
            catch
            {}
        }

        private void playerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject["offlinePlayerName"] = playerName.Text;
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
