// ReSharper disable All

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MICore
{
    namespace Ini
    {
        /// <summary>
        /// 为了读写 ini 文件而实现的类
        /// </summary>
        public class Ini
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="iniPath">要读写的 ini 文件目录</param>
            public Ini(string iniPath)
            {
                _path = iniPath;
            }

            private readonly string _path;

            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section,
                string key,
                string val,
                string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section,
                string key,
                string def,
                StringBuilder retVal,
                int size,
                string filePath);

            /// <summary>
            /// 向 ini 文件写入一对键值
            /// </summary>
            /// <param name="section">段落</param>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            public void IniWriteValue(string section,
                string key,
                string value)
            {
                WritePrivateProfileString(section,
                    key,
                    value,
                    _path);
            }

            /// <summary>
            /// 从 ini 文件读取一对键值
            /// </summary>
            /// <param name="section">段落</param>
            /// <param name="key">键</param>
            /// <returns>键对应的值</returns>
            public string IniReadValue(string section,
                string key)
            {
                var temp = new StringBuilder(255);
                GetPrivateProfileString(section,
                    key,
                    "",
                    temp,
                    255,
                    _path);
                return temp.ToString();
            }
        }
    }

    namespace Json
    {
        public class Json
        {
            private readonly string _path;

            public Json(string path)
            {
                _path = path;
            }

            /// <summary>
            /// 写入 Json 文件
            /// </summary>
            /// <param name="file">要写入的 JObject</param>
            public void Write(JObject file)
            {
                File.WriteAllText(_path,
                    JsonConvert.SerializeObject(file,
                        Formatting.Indented));
            }

            /// <summary>
            /// 写入 Json 文件
            /// </summary>
            /// <param name="file">要写入的 JArray</param>
            public void Write(JArray file)
            {
                File.WriteAllText(_path,
                    JsonConvert.SerializeObject(file,
                        Formatting.Indented));
            }

            /// <summary>
            /// 获取 Json 文件内容
            /// </summary>
            /// <returns>Json 文件内容</returns>
            public JToken Read()
            {
                return JToken.ReadFrom(new JsonTextReader(File.OpenText(_path)));
            }
        }
    }

    namespace Properties
    {
        /// <summary>
        /// 为了读写 properties 文件而实现的类
        /// </summary>
        public class Properties : Hashtable
        {
            private readonly string _fileName;

            private ArrayList List { get; set; } = new();

            public override ICollection Keys =>
                List;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="fileName">要读写的 properties 文件目录</param>
            public Properties(string fileName)
            {
                this._fileName = fileName;
                this.Load(fileName);
            }

            /// <summary>
            /// 添加 1 对键值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            public override void Add(object key,
                object? value)
            {
                base.Add(key,
                    value);
                List.Add(key);
            }

            /// <summary>
            /// 更新/修改 1 对键值
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            public void Update(object key,
                object value)
            {
                base.Remove(key);
                List.Remove(key);
                this.Add(key,
                    value);
            }

            /// <summary>
            /// 加载外部的 properties 文件
            /// </summary>
            /// <param name="filePath">外部 properties 文件存储路径</param>
            private void Load(string filePath)
            {
                using var sr = new StreamReader(filePath);
                while (sr.Peek() >= 0)
                {
                    var bufLine = sr.ReadLine();
                    if (bufLine == null)
                        continue;
                    var limit = bufLine.Length;
                    var keyLen = 0;
                    var valueStart = limit;
                    var hasSep = false;
                    var precedingBackslash = false;
                    if (bufLine.StartsWith("#"))
                    {
                        keyLen = bufLine.Length;
                    }

                    char c;
                    while (keyLen < limit)
                    {
                        c = bufLine[keyLen];
                        if ((c == '=' || c == ':') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            hasSep = true;
                            break;
                        }
                        else if ((c == ' ' || c == '\t' || c == '\f') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            break;
                        }

                        if (c == '\\')
                        {
                            precedingBackslash = !precedingBackslash;
                        }
                        else
                        {
                            precedingBackslash = false;
                        }

                        keyLen++;
                    }

                    while (valueStart < limit)
                    {
                        c = bufLine[valueStart];
                        if (c != ' ' && c != '\t' && c != '\f')
                        {
                            if (!hasSep && (c == '=' || c == ':'))
                            {
                                hasSep = true;
                            }
                            else
                            {
                                break;
                            }
                        }

                        valueStart++;
                    }

                    string key = bufLine.Substring(0,
                        keyLen);
                    string values = bufLine.Substring(valueStart,
                        limit - valueStart);
                    if (key == "")
                    {
                        key += "#";
                    }

                    while (key.StartsWith("#") & this.Contains(key))
                    {
                        key += "#";
                    }

                    this.Add(key,
                        values);
                }
            }

            /// <summary>
            /// 保存修改过的 properties 文件到外部文件，外部文件路径为原来的路径。
            /// </summary>
            public void Save()
            {
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }

                var fileStream = File.Create(_fileName);
                var sw = new StreamWriter(fileStream);
                foreach (var item in List)
                {
                    var key = (string)item;
                    var val = (string)this[key]!;
                    if (key.StartsWith("#"))
                    {
                        sw.WriteLine(val == ""
                            ? key
                            : val);
                    }
                    else
                    {
                        sw.WriteLine(key + "=" + val);
                    }
                }

                sw.Close();
                fileStream.Close();
            }
        }
    }

    namespace Web
    {
        internal class Ftp
        {
        }

        /// <summary>
        /// 基于 HTTP 协议的网络请求类
        /// </summary>
        internal abstract class Http
        {
            /// <summary>
            /// 基于 HTTP 协议发送 GET 请求。
            /// </summary>
            /// <param name="webUrl">请求的URL（统一资源定位器）</param>
            /// <returns>请求返回文本/内容</returns>
            [Obsolete("Obsolete")]
            public static string Get(string? webUrl)
            {
                if (webUrl != null)
                    return new StreamReader(WebRequest.Create(webUrl)
                        .GetResponse()
                        .GetResponseStream()).ReadToEnd();
                return "";
            }

            /// <summary>
            /// 下载带进度条代码（普通进度条）  
            /// </summary>  
            /// <param name="URL">网址</param>  
            /// <param name="Filename">下载后文件名为</param>  
            /// <param name="updateProgress">?</param>
            /// <returns>True/False是否下载成功</returns>  
            [Obsolete("Obsolete")]
            public static bool DownLoadFile(string URL,
                string Filename,
                Action<int, int>? updateProgress = null)
            {
                Stream? st = null;
                Stream? so = null;
                HttpWebRequest? Myrq = null;
                HttpWebResponse? myrp = null;
                bool flag = false;
                try
                {
                    Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                    myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                    long totalBytes = myrp.ContentLength;
                    if (updateProgress != null)
                    {
                        updateProgress((int)totalBytes,
                            0);
                    }

                    st = myrp.GetResponseStream();
                    so = new System.IO.FileStream(Filename,
                        System.IO.FileMode.Create);
                    long totalDownloadedByte = 0;
                    byte[] by = new byte[1024];
                    int osize = st.Read(by,
                        0,
                        (int)by.Length);
                    while (osize > 0)
                    {
                        totalDownloadedByte = osize + totalDownloadedByte;
                        Application.DoEvents();
                        so.Write(by,
                            0,
                            osize);
                        if (updateProgress != null)
                        {
                            updateProgress((int)totalBytes,
                                (int)totalDownloadedByte);
                        }

                        osize = st.Read(by,
                            0,
                            (int)by.Length);
                    }

                    if (updateProgress != null)
                    {
                        updateProgress((int)totalBytes,
                            (int)totalBytes);
                    }

                    flag = true;
                }
                catch (Exception)
                {
                    flag = false;
                    throw;
                }
                finally
                {
                    if (Myrq != null)
                    {
                        Myrq.Abort();
                    }

                    if (myrp != null)
                    {
                        myrp.Close();
                    }

                    if (so != null)
                    {
                        so.Close();
                    }

                    if (st != null)
                    {
                        st.Close();
                    }
                }

                return flag;
            }

        }

        internal class Https
        {
        }

        internal class Socket
        {
        }

        internal class Tcp
        {
        }

        internal class Udp
        {
        }
    }
}