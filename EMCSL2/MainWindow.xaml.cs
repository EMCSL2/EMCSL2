// ReSharper disable All

using MICore.Ini;
using MICore.Web;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;


#pragma warning disable SYSLIB0025
[assembly: SuppressIldasm()]
#pragma warning restore SYSLIB0025
namespace EMCSL2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class Link
    {
        public string? Version { get; set; }
        public string? Url { get; set; }
    }

    public partial class MainWindow
    {
        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            首页_Click();
            bool success = false;
            string a = "";
            try
            {
                a = Http.Get("https://flowersacrifice.github.io/update");
                success = true;
            }
            catch
            {
                this.公告.Text = "公告获取失败！";
            }
            finally
            {
                if (success)
                {
                    this.公告.Text = a;
                }
            }
        }

        private Dictionary<string, string?> release = new();
        private Dictionary<string, List<string?>> servers = new();

        private Ookii.Dialogs.Wpf.VistaFolderBrowserDialog save = new();

        private void WinMsg(string? Msg)
        {
            new ToastContentBuilder()
                .AddArgument("action",
                    "viewConversation")
                .AddArgument("conversationId",
                    9813)
                .AddText(Msg)
                .Show();
        }

        #region 基础（标题栏）模块

        private void 顶栏_MouseMove(object sender,
            MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void 关闭_Click(object sender,
            RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void 最小化_Click(object sender,
            RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region 菜单栏处理

        private int _selectedPage = 1;

        private void 首页_Click()
        {
            if (_selectedPage != 1)
            {
                _selectedPage = 1;
                this.首页_.Visibility = Visibility.Hidden;
                this.首页.Visibility = Visibility.Visible;
                this.下载.Visibility = Visibility.Hidden;
                this.下载_.Visibility = Visibility.Visible;
                this.启动.Visibility = Visibility.Hidden;
                this.启动_.Visibility = Visibility.Visible;
                this.其它.Visibility = Visibility.Hidden;
                this.其它_.Visibility = Visibility.Visible;
                this.设置.Visibility = Visibility.Hidden;
                this.设置_.Visibility = Visibility.Visible;

                this.首页页面.Visibility = Visibility.Visible;
                this.下载页页面.Visibility = Visibility.Hidden;
                this.启动页页面.Visibility = Visibility.Hidden;
                this.其它页页面.Visibility = Visibility.Hidden;
                this.设置页页面.Visibility = Visibility.Hidden;
            }
        }

        private void 首页_Click(object sender,
            RoutedEventArgs e)
        {
            if (_selectedPage != 1)
            {
                _selectedPage = 1;
                this.首页_.Visibility = Visibility.Hidden;
                this.首页.Visibility = Visibility.Visible;
                this.下载.Visibility = Visibility.Hidden;
                this.下载_.Visibility = Visibility.Visible;
                this.启动.Visibility = Visibility.Hidden;
                this.启动_.Visibility = Visibility.Visible;
                this.其它.Visibility = Visibility.Hidden;
                this.其它_.Visibility = Visibility.Visible;
                this.设置.Visibility = Visibility.Hidden;
                this.设置_.Visibility = Visibility.Visible;

                this.首页页面.Visibility = Visibility.Visible;
                this.下载页页面.Visibility = Visibility.Hidden;
                this.启动页页面.Visibility = Visibility.Hidden;
                this.其它页页面.Visibility = Visibility.Hidden;
                this.设置页页面.Visibility = Visibility.Hidden;
            }
        }

        [Obsolete("Obsolete")]
        private void 下载_Click(object sender,
            RoutedEventArgs e)
        {
            if (_selectedPage != 2)
            {
                _selectedPage = 2;
                this.下载_.Visibility = Visibility.Hidden;
                this.下载.Visibility = Visibility.Visible;
                this.首页.Visibility = Visibility.Hidden;
                this.首页_.Visibility = Visibility.Visible;
                this.启动.Visibility = Visibility.Hidden;
                this.启动_.Visibility = Visibility.Visible;
                this.其它.Visibility = Visibility.Hidden;
                this.其它_.Visibility = Visibility.Visible;
                this.设置.Visibility = Visibility.Hidden;
                this.设置_.Visibility = Visibility.Visible;

                this.下载页页面.Visibility = Visibility.Visible;
                this.首页页面.Visibility = Visibility.Hidden;
                this.启动页页面.Visibility = Visibility.Hidden;
                this.其它页页面.Visibility = Visibility.Hidden;
                this.设置页页面.Visibility = Visibility.Hidden;
                下载页处理();
            }
        }

        private void 启动_Click(object sender,
            RoutedEventArgs e)
        {
            if (_selectedPage != 3)
            {
                _selectedPage = 3;
                this.启动_.Visibility = Visibility.Hidden;
                this.启动.Visibility = Visibility.Visible;
                this.下载.Visibility = Visibility.Hidden;
                this.下载_.Visibility = Visibility.Visible;
                this.首页.Visibility = Visibility.Hidden;
                this.首页_.Visibility = Visibility.Visible;
                this.其它.Visibility = Visibility.Hidden;
                this.其它_.Visibility = Visibility.Visible;
                this.设置.Visibility = Visibility.Hidden;
                this.设置_.Visibility = Visibility.Visible;

                this.启动页页面.Visibility = Visibility.Visible;
                启动页处理();
                this.下载页页面.Visibility = Visibility.Hidden;
                this.首页页面.Visibility = Visibility.Hidden;
                this.其它页页面.Visibility = Visibility.Hidden;
                this.设置页页面.Visibility = Visibility.Hidden;
            }
        }

        private void 其它_Click(object sender,
            RoutedEventArgs e)
        {
            if (_selectedPage != 4)
            {
                _selectedPage = 4;
                this.其它_.Visibility = Visibility.Hidden;
                this.其它.Visibility = Visibility.Visible;
                this.下载.Visibility = Visibility.Hidden;
                this.下载_.Visibility = Visibility.Visible;
                this.启动.Visibility = Visibility.Hidden;
                this.启动_.Visibility = Visibility.Visible;
                this.首页.Visibility = Visibility.Hidden;
                this.首页_.Visibility = Visibility.Visible;
                this.设置.Visibility = Visibility.Hidden;
                this.设置_.Visibility = Visibility.Visible;

                this.其它页页面.Visibility = Visibility.Visible;
                this.下载页页面.Visibility = Visibility.Hidden;
                this.启动页页面.Visibility = Visibility.Hidden;
                this.首页页面.Visibility = Visibility.Hidden;
                this.设置页页面.Visibility = Visibility.Hidden;
            }
        }

        private void 设置_Click(object sender,
            RoutedEventArgs e)
        {
            if (_selectedPage != 5)
            {
                _selectedPage = 5;
                this.设置_.Visibility = Visibility.Hidden;
                this.设置.Visibility = Visibility.Visible;
                this.下载.Visibility = Visibility.Hidden;
                this.下载_.Visibility = Visibility.Visible;
                this.启动.Visibility = Visibility.Hidden;
                this.启动_.Visibility = Visibility.Visible;
                this.其它.Visibility = Visibility.Hidden;
                this.其它_.Visibility = Visibility.Visible;
                this.首页.Visibility = Visibility.Hidden;
                this.首页_.Visibility = Visibility.Visible;

                this.设置页页面.Visibility = Visibility.Visible;
                this.下载页页面.Visibility = Visibility.Hidden;
                this.启动页页面.Visibility = Visibility.Hidden;
                this.其它页页面.Visibility = Visibility.Hidden;
                this.首页页面.Visibility = Visibility.Hidden;
                设置页处理();
            }
        }

        #endregion

        private void MournInkSpace(object sender,
            RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = ((Hyperlink)sender).NavigateUri.ToString(),
                UseShellExecute = true
            });
        }

        [Obsolete]
        private void 主页刷新_Click(object sender,
            RoutedEventArgs e)
        {
            this.公告.Text = "";
            bool success = false;
            string a = "";
            try
            {
                a = Http.Get("https://flowersacrifice.github.io/update");
                success = true;
            }
            catch
            {
                this.公告.Text = "公告获取失败！";
            }
            finally
            {
                if (success)
                {
                    this.公告.Text = a;
                }
            }
        }

        [Obsolete]
        private void 下载页处理()
        {
            if (release.Count != 0)
                return;
            JArray versions = new();
            try
            {
                versions =
                    (JArray)JToken.Parse(Http.Get("https://launchermeta.mojang.com/mc/game/version_manifest_v2.json"))[
                        "versions"]!;
            }
            catch (Exception)
            {
                MessageBox.Show("网络请求失败，请检查网络连接状况。");
                return;
            }

            var releases = new List<Link>();
            foreach (var jToken in versions)
            {
                var version = (JObject)jToken;
                if (jToken["type"]
                        ?.ToString() ==
                    "release")
                {
                    release.Add(jToken["id"]
                                    ?.ToString() ??
                                string.Empty,
                        jToken["url"]
                            ?.ToString());
                    releases.Add(new Link()
                    {
                        Version = jToken["id"]
                            ?.ToString(),
                        Url = jToken["url"]
                            ?.ToString()
                    });
                }
            }

            this.版本选择.ItemsSource = releases;
            this.版本选择.DisplayMemberPath = "Version";
            this.版本选择.SelectedValuePath = "Link";
        }

        private void 启动页处理()
        {
            if (servers.Count != 0)
                servers.Clear();
            List<string> a = new List<string>
            {
                "M",
                "G"
            };
            List<string> b = new List<string>();
            最小内存_单位选择.ItemsSource = a;
            最大内存_单位选择.ItemsSource = a;
            JArray jsonObject = (JArray)JToken.ReadFrom(new JsonTextReader(File.OpenText(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\EMCSL\installed_versions.json")));
            foreach (JObject item in jsonObject)
            {
                servers.Add(item["name"]
                                ?.ToString() +
                            " - " +
                            item["path"]
                                ?.ToString(),
                    new List<string?>
                    {
                        item["is17"]
                            ?.ToString(),
                        item["path"]
                            ?.ToString(),
                        item["jarname"]
                            ?.ToString()
                    });
                b.Add(item["name"]
                          ?.ToString() +
                      " - " +
                      item["path"]
                          ?.ToString());
            }

            启动_版本选择.ItemsSource = b;
            最小内存_单位选择.Text = "G";
            最大内存_单位选择.Text = "G";
        }

        private void 设置页处理()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                             @"\EMCSL\settings.ini"))
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  @"\EMCSL\settings.ini",
                    "");
            }
            Ini _ini = new Ini(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               @"\EMCSL\settings.ini");
            J8.Text = _ini.IniReadValue("Java", "8");
            J17.Text = _ini.IniReadValue("Java", "17");
        }

        private void 改J8(object _, RoutedEventArgs __)
        {
            Ookii.Dialogs.Wpf.VistaOpenFileDialog _save = new();
            _save.Filter = "Java (java.exe)|java.exe";
            if (_save.ShowDialog() ?? false)
            {
                if (MessageBox.Show("确定更改？更改造成的不必要的意外将由您自己负责。", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                    MessageBoxResult.No)
                {
                    return;
                }
                J8.Text = _save.FileName;
                Ini _ini = new Ini(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                   @"\EMCSL\settings.ini");
                _ini.IniWriteValue("Java", "8", J8.Text);
            }
        }

        private void 改J17(object _, RoutedEventArgs __)
        {
            Ookii.Dialogs.Wpf.VistaOpenFileDialog _save = new();
            _save.Filter = "Java (java.exe)|java.exe";
            if (_save.ShowDialog() ?? false)
            {
                if (MessageBox.Show("确定更改？更改造成的不必要的意外将由您自己负责。", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                    MessageBoxResult.No)
                {
                    return;
                }
                J17.Text = _save.FileName;
                Ini _ini = new Ini(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                   @"\EMCSL\settings.ini");
                _ini.IniWriteValue("Java", "17", J17.Text);
            }
        }
        
        [Obsolete]
        private void 安装(object sender,
            RoutedEventArgs e)
        {
            if (this.版本选择.Text == "")
            {
                MessageBox.Show("请先选择一个版本！");
                return;
            }

            if (this.下载页_下载路径.Text == "")
            {
                MessageBox.Show("请先选择一个下载路径！");
                return;
            }

            if (((JObject)JToken.Parse(Http.Get(release[this.版本选择.Text])))["downloads"]!.ToString()
                .Contains("server") ==
                false)
            {
                MessageBox.Show("该版本不存在服务端！");
                return;
            }


            StreamReader _streamReader = File.OpenText(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\EMCSL\installed_versions.json");
            JsonTextReader _jsonTextReader = new JsonTextReader(_streamReader);
            JArray _jsonObject = (JArray)JToken.ReadFrom(_jsonTextReader);
            _streamReader.Close();
            _jsonTextReader.Close();
            foreach (JObject item in _jsonObject)
            {
                if (item["jarname"]
                        ?.ToString() ==
                    "server.jar" &&
                    item["path"]
                        ?.ToString() ==
                    下载页_下载路径.Text)
                {
                    MessageBox.Show("该核心已经存在，请更换路径再试！");
                    return;
                }
            }

            if (!Directory.Exists(this.下载页_下载路径.Text))
            {
                Directory.CreateDirectory(this.下载页_下载路径.Text);
            }

            this.下载_进度数字.Text = "";
            下载_进度数字.Visibility = Visibility.Visible;
            下载_进度条.Visibility = Visibility.Visible;
            if (Http.DownLoadFile(
                    ((JObject)JToken.Parse(Http.Get(release[this.版本选择.Text])))["downloads"]!["server"]!["url"]!
                    .ToString(),
                    this.下载页_下载路径.Text + "server.jar",
                    new Action<int, int>((int Maximum,
                        int Value) =>
                    {
                        下载_进度条.Maximum = Maximum;
                        下载_进度条.Value = Value;
                        下载_进度数字.Text =
                            $"{"MC原版核心"} {this.版本选择.Text} 下载进度: {(Value * 100.0 / Maximum).ToString(".##")}%";
                    })))
            {
                WinMsg($"{"MC原版核心"}{this.版本选择.Text}下载成功！");
                下载_进度数字.Visibility = Visibility.Hidden;
                下载_进度条.Visibility = Visibility.Hidden;
                string _ver = this.版本选择.Text;
                string _path = this.下载页_下载路径.Text;
                this.下载页_下载路径.Text = "";
                this.版本选择.Text = "";
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      @"\EMCSL\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                              @"\EMCSL\");
                }

                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                 @"\EMCSL\installed_versions.json"))
                {
                    File.WriteAllText(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        @"\EMCSL\installed_versions.json",
                        "[]");
                }

                StreamReader reader = File.OpenText(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    @"\EMCSL\installed_versions.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JArray jsonObject = (JArray)JToken.ReadFrom(jsonTextReader);
                jsonTextReader.Close();

                jsonObject.Add(JObject.FromObject(new
                {
                    is17 = new Version(_ver) > new Version("1.16.5")
                        ? "1"
                        : "0",
                    name = _ver,
                    path = _path,
                    jarname = "server.jar"
                }));
                try
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      @"\EMCSL\installed_versions.json",
                        jsonObject.ToString());
                }
                catch
                {
                    MessageBox.Show("您的程序出现了一些问题，请重新尝试！");
                    return;
                }
            }
        }

        private void 选择文件夹(object sender,
            RoutedEventArgs e)
        {
            if (this.版本选择.Text == "")
            {
                MessageBox.Show("请先选择一个版本！");
                return;
            }

            save.Description = "请选择服务器安装目录。";
            save.ShowNewFolderButton = true;
            if (save.ShowDialog() == true)
            {
                this.下载页_下载路径.Text = save.SelectedPath +
                                     (save.SelectedPath.EndsWith(@"\")
                                         ? ""
                                         : @"\") +
                                     @"Server\" +
                                     this.版本选择.Text +
                                     @"\";
            }

            return;
        }

        private void 版本选择_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.下载页_下载路径.Text = "";
        }

        private void 检查端口占用(object sender,
            RoutedEventArgs e)
        {
            int num = 0;
            if (!int.TryParse(this.输入端口.Text,
                    out num))
            {
                MessageBox.Show("端口号不合法，请输入一个介于1至65535之间的整数！");
                return;
            }
            else if (int.Parse(this.输入端口.Text) < 1 || int.Parse(this.输入端口.Text) > 65535)
            {
                MessageBox.Show("端口号不合法，请输入一个介于1至65535之间的整数！");
                return;
            }

            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == int.Parse(this.输入端口.Text))
                {
                    inUse = true;
                    break;
                }
            }

            MessageBox.Show((inUse
                ? "端口被占用！"
                : "端口空闲！"));
        }

        private enum FileSizeUnit
        {
            B,
            KB,
            MB,
            GB,
            TB,
            PB
        }

        private static long GetTotalPhysicalMemory()
        {
            long capacity = 0;
            try
            {
                foreach (ManagementObject mo1 in new ManagementClass("Win32_PhysicalMemory").GetInstances())
                    capacity += long.Parse(mo1.Properties["Capacity"]
                                               .Value.ToString() ??
                                           string.Empty);
            }
            catch (Exception)
            {
                return 0;
            }

            return capacity;
        }

        private static double ToFileFormat(long filesize,
            FileSizeUnit targetUnit = FileSizeUnit.MB)
        {
            double size = -1;
            switch (targetUnit)
            {
                case FileSizeUnit.KB:
                    size = filesize / 1024.0;
                    break;
                case FileSizeUnit.MB:
                    size = filesize / 1024.0 / 1024;
                    break;
                case FileSizeUnit.GB:
                    size = filesize / 1024.0 / 1024 / 1024;
                    break;
                case FileSizeUnit.TB:
                    size = filesize / 1024.0 / 1024 / 1024 / 1024;
                    break;
                case FileSizeUnit.PB:
                    size = filesize / 1024.0 / 1024 / 1024 / 1024 / 1024;
                    break;
                default:
                    size = filesize;
                    break;
            }

            return size;
        }

        private void 启动页_启动_OnClick(object sender,
            RoutedEventArgs e)
        {
            if (this.启动_版本选择.Text == "")
            {
                MessageBox.Show("请选择一个要启动的服务端版本！");
                return;
            }

            if (this.最小运存.Text == "")
            {
                MessageBox.Show("请输入一个最小运行内存！");
                return;
            }

            if (this.最大运存.Text == "")
            {
                MessageBox.Show("请输入一个最大运行内存！");
                return;
            }

            if (this.输入端口.Text == "")
            {
                MessageBox.Show("请输入一个端口！");
                return;
            }

            int num = 0;
            if (!int.TryParse(this.输入端口.Text,
                    out num))
            {
                MessageBox.Show("端口号不合法，请输入一个介于1至65535之间的整数！");
                return;
            }

            if (int.Parse(this.输入端口.Text) < 1 || int.Parse(this.输入端口.Text) > 65535)
            {
                MessageBox.Show("端口号不合法，请输入一个介于1至65535之间的整数！");
                return;
            }

            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == int.Parse(this.输入端口.Text))
                {
                    inUse = true;
                    break;
                }
            }

            if (inUse)
            {
                MessageBox.Show("端口被占用，请换一个端口！");
                return;
            }

            if (!int.TryParse(this.最小运存.Text,
                    out num))
            {
                MessageBox.Show("最小运行内存不合法，请输入一个整数！");
                return;
            }

            if (!int.TryParse(this.最大运存.Text,
                    out num))
            {
                MessageBox.Show("最大运行内存不合法，请输入一个整数！");
                return;
            }

            if (long.Parse(this.最小运存.Text) *
                (最小内存_单位选择.Text == "M"
                    ? 1
                    : (最小内存_单位选择.Text == "G"
                        ? 1024
                        : 1048576)) >
                long.Parse(this.最大运存.Text) *
                (最大内存_单位选择.Text == "M"
                    ? 1
                    : (最大内存_单位选择.Text == "G"
                        ? 1024
                        : 1048576)))
            {
                MessageBox.Show("最小运行内存大于最大运行内存！请重新输入！");
                this.最小运存.Text = "";
                this.最大运存.Text = "";
                return;
            }

            double GB = ToFileFormat(GetTotalPhysicalMemory(),
                FileSizeUnit.GB);
            double MB = ToFileFormat(GetTotalPhysicalMemory(),
                FileSizeUnit.MB);
            if (long.Parse(this.最小运存.Text) *
                (最小内存_单位选择.Text == "G"
                    ? 1024
                    : 1) >
                MB ||
                long.Parse(this.最大运存.Text) *
                (最大内存_单位选择.Text == "G"
                    ? 1024
                    : 1) >
                MB)
            {
                MessageBox.Show("所指定最小/最大运行内存超过物理内存上限，请重新指定！");
                this.最小运存.Text = "";
                this.最大运存.Text = "";
                return;
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                             @"\EMCSL\settings.ini"))
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  @"\EMCSL\settings.ini",
                    "");
            }

            Ini _ini = new Ini(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               @"\EMCSL\settings.ini");
            if (_ini.IniReadValue("Java",
                    "8") ==
                "" &&
                Java8.IsChecked == true)
            {
                MessageBox.Show("未指定Java 8路径，请在设置页面指定路径后再启动。");
                return;
            }

            if (_ini.IniReadValue("Java",
                    "17") ==
                "" &&
                Java17.IsChecked == true)
            {
                MessageBox.Show("未指定Java 17路径，请在设置页面指定路径后再启动。");
                return;
            }

            if ((servers[启动_版本选择.Text][0] == "1" && this.Java8.IsChecked == true) ||
                (servers[启动_版本选择.Text][0] == "0" && this.Java17.IsChecked == true))
            {
                if (MessageBox.Show("您选择的Java版本与推荐的并不相同，确认启动？",
                        "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) ==
                    MessageBoxResult.No)
                {
                    return;
                }
            }

            File.WriteAllText(servers[启动_版本选择.Text][1] + "eula.txt",
                "eula=true");

            MessageBox.Show("在开服时遇到的意外可以向您认识的任何大佬求助。");
            string args =
                $"-Xms{this.最小运存.Text}{最小内存_单位选择.Text} -Xmx{this.最大运存.Text}{最大内存_单位选择.Text} -jar \"{servers[启动_版本选择.Text][1]}{servers[启动_版本选择.Text][2]}\"{(IsNoGui.IsChecked == false ? " nogui" : "")} -port {输入端口.Text}";
            string argv = (this.Java17.IsChecked == true
                ? _ini.IniReadValue("Java",
                    "17")
                : _ini.IniReadValue("Java",
                    "8"));
            // new ServerConsole(argv, args, 启动_版本选择.Text.Split(" - ")[0], 输入端口.Text).Show();
            string command =
                $"@echo off & {servers[启动_版本选择.Text][1]?.Split('\\')[0]} & cd \"{servers[启动_版本选择.Text][1]}\" & \"{argv}\" {args}";
            System.Diagnostics.Process.Start("CMD.exe",
                "/K " + command);
        }

        private void 启动_版本选择_OnSelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            启动_版本选择.ToolTip = 启动_版本选择.SelectedItem.ToString();
            this.Java17.IsChecked = (servers[启动_版本选择.SelectedItem.ToString()!][0] == "1");
        }

        private void ButtonBase_OnClick(object sender,
            RoutedEventArgs e)
        {
            Ookii.Dialogs.Wpf.VistaOpenFileDialog _save = new();
            _save.Filter = "服务端核心文件 (*.jar)|*.jar";
            if (_save.ShowDialog() ?? false)
            {
                string str = _save.FileName;
                string __path = "";
                string[] strArr = str.Split('\\');
                for (int i = 0;
                     i < strArr.Length - 1;
                     i++)
                {
                    __path += strArr[i] + @"\";
                }

                StreamReader streamReader = File.OpenText(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    @"\EMCSL\installed_versions.json");
                JsonTextReader jsonTextReader = new JsonTextReader(streamReader);
                JArray jsonObject = (JArray)JToken.ReadFrom(jsonTextReader);
                streamReader.Close();
                jsonTextReader.Close();
                string _path = __path;
                string _jarname = strArr[strArr.Length - 1];
                foreach (JObject item in jsonObject)
                {
                    if (item["jarname"]
                            ?.ToString() ==
                        _jarname &&
                        item["path"]
                            ?.ToString() ==
                        _path)
                    {
                        MessageBox.Show("该核心已被添加！");
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }

                Input _inpt = new Input();
                _inpt.ShowDialog();
                if (_inpt.success)
                {
                    string _is17 = _inpt.is17;
                    string _name = _inpt.name;
                    jsonObject.Add(JObject.FromObject(new
                    {
                        is17 = _is17,
                        name = _name,
                        path = _path,
                        jarname = _jarname
                    }));
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      @"\EMCSL\installed_versions.json",
                        jsonObject.ToString());
                }
                else
                {
                    MessageBox.Show("保存失败！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("选择时失败！");
                return;
            }

            启动页处理();
        }
    }
}