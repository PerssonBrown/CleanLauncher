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
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Minecraft;
using System.Threading;
using Panuon.UI.Silver;

namespace CleanLauncher.downloader
{
    /// <summary>
    /// downloader.xaml 的交互逻辑
    /// </summary>
    public partial class downloader : Page
    {
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        SquareMinecraftLauncher.MinecraftDownload minecraftDownload = new SquareMinecraftLauncher.MinecraftDownload();
        Gac.DownLoadFile downloadFile = new Gac.DownLoadFile();

        //CleanLauncher.downloader.downloader downloader = new downloader();

        public static int id = 0;
        string downloadVersion;
        List<DownloadItem> list = new List<DownloadItem>();
        internal int Download(string path, string url)
        {
            this.downloadFile.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);
            this.downloadFile.StartDown(3);
            id++;
            list.Add(new DownloadItem { id = id - 1, msg = "" });
            return id - 1;
        }
        public downloader()
        {
            InitializeComponent();
            tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
            initDataGrid();
            downloadFile.doSendMsg += DownloadFile_doSendMsg;
        }

        private void DownloadFile_doSendMsg(Gac.DownMsg msg)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == msg.Id)
                {
                    list[i].msg = msg.Tag == Gac.DownStatus.End ? "完成" : msg.Tag == Gac.DownStatus.Error ? "错误" : "下载中";
                }
            }
        }
        public async void initDataGrid()
        {
            List<VersionList> list = new List<VersionList>();
            var version = await tools.GetMCVersionList();
            foreach (var i in version)
            {
                VersionList versionList = new VersionList();
                versionList.id = i.id;
                versionList.type = i.type;
                versionList.releaseTime = i.releaseTime;
                list.Add(versionList);
            }
            MCVersions.ItemsSource = list;
        }

        public class VersionList
        {
            public string id { get; set; }
            public string type { get; set; }
            public string releaseTime { get; set; }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the version what is the user selected
                var v = MCVersions.SelectedItem as VersionList;
                downloadVersion = v.id.ToString();

                // Download Minecraft main files(.jar and .json)
                MessageBox.Show("jar json");
                downloadMinecraftMainFile();

                // Download assets files
                MessageBox.Show("assets");
                await downloadMinecraftAssetsFile();

                // Download Libraries Files
                MessageBox.Show("libraries");
                await Libraries(downloadVersion);

                // Download completed
                MessageBox.Show("下载完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "下载失败");
            }
        }
        public void downloadMinecraftMainFile()
        {
            var jarFile = minecraftDownload.MCjarDownload(downloadVersion);
            var jsonFile = minecraftDownload.MCjsonDownload(downloadVersion);
            Download(jarFile.path, jarFile.Url);
            Download(jsonFile.path, jsonFile.Url);
        }
        public async Task downloadMinecraftAssetsFile()
        {
            AssetDownload assetDownload = new AssetDownload();
            assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;
            await assetDownload.BuildAssetDownload(1000, downloadVersion);
        }
        private void AssetDownload_DownloadProgressChanged(AssetDownload.DownloadIntermation Log)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                progress.Value = Log.Progress;
                Console.WriteLine(Log.Progress + "  " + Log.Speed);
            }));
        }
        public async Task Libraries(string version)
        {
            MCDownload[] libraries = tools.GetMissingFile(version);
            if (libraries.Length == 0) return;
            MessageBox.Show("还有" + libraries.Length.ToString() + "个文件");
            foreach (MCDownload lib in libraries)
            {
                Download(lib.path, lib.Url);
            }
            await Task.Run(() =>
            {
                while (list.Count != 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].msg == "错误" || list[i].msg == "完成")
                        {
                            list.RemoveAt(i);
                        }
                    }
                    Thread.Sleep(500);
                }
            });
            await Libraries(version);
        }
        #region
        /// <summary>
        /// 官方源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Official(object sender, RoutedEventArgs e)
        //{
        //    tools.DownloadSourceInitialization(DownloadSource.MinecraftSource);
        //}

        /// <summary>
        /// MCBBS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MCBBS(object sender, RoutedEventArgs e)
        //{
        //    tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
        //}
        /// <summary>
        /// BMCLAPI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BMCLAPI(object sender, RoutedEventArgs e)
        //{
        //    tools.DownloadSourceInitialization(DownloadSource.bmclapiSource);
        //}
        #endregion
    }
    public class DownloadItem
    {
        public int id { set; get; }
        public string msg { set; get; }
    }
}
