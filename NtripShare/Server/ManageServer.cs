using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPServerLib;
using System.IO;
using NtripShare.Model;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Converters;

namespace NtripShare
{
    public class ManageServer : HTTPServerLib.HttpServer
    {
        public static string UUID = "";
        System.Timers.Timer t = new System.Timers.Timer( 30*60* 1000);
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">端口号</param>
        public ManageServer( int port)
            : base ("0.0.0.0", port)
        {
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout); //到达时间的时候执行事件；   
            t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；   
        }

      //实例化Timer类，设置间隔时间为10000毫秒；   
  
    
    public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            UUID = "";
        }  


        public override void OnPost(HttpRequest request, HttpResponse response)
        {
           
         
        }

        private void getData() { 
        
        }

        private string getDataResult(HttpRequest request) {
            try {
                string requestURL = request.URL.Split('?')[0];
                requestURL = requestURL.Replace("/", @"\").Replace("\\..", "").TrimStart('\\');
                if (requestURL.Contains("login.go"))
                {
                    string username = request.Params["username"];
                    string password = request.Params["password"];
                    if (username == NtripShare.Model.DocumentSetting.Default.UserName && password == NtripShare.Model.DocumentSetting.Default.PassWord) {
                        UUID = Guid.NewGuid().ToString();
                        //设置返回信息
                        string content = UUID;
                        t.Start();
                        return "1," + UUID;
                    }
                    return "0,";
                }
                if (requestURL.Contains("logout.go"))
                {
                    UUID = "";
                    return "1,";
                }
                if (requestURL.Contains("addUser.go"))
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.Username = request.Params["username"];
                    userAccount.Password = request.Params["password"];
                    userAccount.Des = request.Params["des"];
                    userAccount.DeadLineTime = DateTime.Parse(request.Params["time"]);
                    userAccount.MaxConnectCount = 1;
                    DocumentSetting.Default.addOrUpdateUserAccount(userAccount);
                    return "1,";
                }
                if (requestURL.Contains("removeUser.go"))
                {
                    DocumentSetting.Default.removeUserAccount(request.Params["id"]);
                    return "1,";
                }
                if (requestURL.Contains("getUser.go"))
                {
                    var iso = new IsoDateTimeConverter();
                    iso.DateTimeFormat = "yyyy-MM-ddThh:mm";

                    return JsonConvert.SerializeObject(DocumentSetting.Default.UserAccounts[request.Params["id"]], iso);
                }
                if (requestURL.Contains("listUser.go"))
                {
                    var iso = new IsoDateTimeConverter();
                    iso.DateTimeFormat = "yy-MM-dd hh:mm:ss";
                    return JsonConvert.SerializeObject(DocumentSetting.Default.UserAccounts.Values.ToArray(), iso) ;
                }
                if (requestURL.Contains("updateUser.go"))
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.Username = request.Params["username"];
                    userAccount.Password = request.Params["password"];
                    userAccount.Des = request.Params["des"];
                    userAccount.DeadLineTime = DateTime.ParseExact(request.Params["time"], "yyyy-MM-dd hh:mm:ss", new System.Globalization.CultureInfo("en-us"));
                    userAccount.MaxConnectCount = 1;
                    DocumentSetting.Default.addOrUpdateUserAccount(userAccount);
                    return "1,";
                }
                if (requestURL.Contains("status.go"))
                {
                    var iso = new IsoDateTimeConverter();
                    iso.DateTimeFormat = "MM-dd hh:mm";
                    SystemStatus.Default.MaxConnectCount = DocumentSetting.Default.MaxConnectionCount;
                    return JsonConvert.SerializeObject(SystemStatus.Default, iso);
                }
            } catch (Exception e) {
                return "500";
            }
            return "500";
        }

        public override void OnGet(HttpRequest request, HttpResponse response)
        {
            if (request.URL.Contains(".go"))
            {
                try
                {
                    //获取客户端传递的参数
                    string data = request.Params == null ? "" : string.Join(";", request.Params.Select(x => x.Key + "=" + x.Value).ToArray());
                    if (!request.URL.Contains("login.go"))
                    {
                        if (request.Params == null || !request.Params.ContainsKey("uuid") || !request.Params["uuid"].Equals(UUID) || UUID == "")
                        {
                            response.SetContent("非法登录");
                            response.Content_Encoding = "utf-8";
                            response.StatusCode = "200";
                            response.Content_Type = "text/html; charset=UTF-8";
                            response.Headers["Server"] = "Server";
                            response.Send();
                            return;
                        }
                    }

                    string content = getDataResult(request);
                    //构造响应报文
                    response.SetContent(content);
                    response.Content_Encoding = "utf-8";
                    response.StatusCode = "200";
                    response.Content_Type = "text/html; charset=UTF-8";
                    response.Headers["Server"] = "Server";
                    //发送响应
                    response.Send();
                    t.Stop();
                    t.Start();
                }
                catch (Exception e)
                {
                    response.SetContent("500");
                    response.Content_Encoding = "utf-8";
                    response.StatusCode = "200";
                    response.Content_Type = "text/html; charset=UTF-8";
                    response.Headers["Server"] = "Server";
                    //发送响应
                    response.Send();
                }
            }
            else {
                if (request.URL.Contains(".html") && !request.URL.Contains("index.html"))
                {
                    if (request.Params == null || !request.Params.ContainsKey("uuid") || !request.Params["uuid"].Equals(UUID) || UUID == "")
                    {
                        response.SetContent("非法登录");
                        response.Content_Encoding = "utf-8";
                        response.StatusCode = "200";
                        response.Content_Type = "text/html; charset=UTF-8";
                        response.Headers["Server"] = "Server";
                        response.Send();
                    }
                }
                t.Stop();
                t.Start();
                //当文件不存在时应返回404状态码
                string requestURL = request.URL.Split('?')[0];
                requestURL = requestURL.Replace("/", @"\").Replace("\\..", "").TrimStart('\\');
                string requestFile = Path.Combine(ServerRoot, requestURL);

                //判断地址中是否存在扩展名
                string extension = Path.GetExtension(requestFile);

                //根据有无扩展名按照两种不同链接进行处
                if (extension != "")
                {
                    //从文件中返回HTTP响应
                    response = response.FromFile(requestFile);
                }
                else
                {
                    //目录存在且不存在index页面时时列举目录
                    if (Directory.Exists(requestFile) && !File.Exists(requestFile + "\\index.html"))
                    {
                        requestFile = Path.Combine(ServerRoot, requestFile);
                        var content = ListDirectory(requestFile, requestURL);
                        response = response.SetContent(content, Encoding.UTF8);
                        response.Content_Type = "text/html; charset=UTF-8";
                    }
                    else
                    {
                        //加载静态HTML页面
                        requestFile = Path.Combine(requestFile, "index.html");
                        response = response.FromFile(requestFile);
                        response.Content_Type = "text/html; charset=UTF-8";
                    }
                }
                //发送HTTP响应
                response.Send();
            }
        }

        public override void OnDefault(HttpRequest request, HttpResponse response)
        {

        }

        private string ConvertPath(string[] urls)
        {
            string html = string.Empty;
            int length = ServerRoot.Length;
            foreach (var url in urls)
            {
                var s = url.StartsWith("..") ? url : url.Substring(length).TrimEnd('\\');
                html += String.Format("<li><a href=\"{0}\">{0}</a></li>", s);
            }

            return html;
        }

        private string ListDirectory(string requestDirectory, string requestURL)
        {
            //列举子目录
            var folders = requestURL.Length > 1 ? new string[] { "../" } : new string[] { };
            folders = folders.Concat(Directory.GetDirectories(requestDirectory)).ToArray();
            var foldersList = ConvertPath(folders);

            //列举文件
            var files = Directory.GetFiles(requestDirectory);
            var filesList = ConvertPath(files);

            //构造HTML
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<html><head><title>{0}</title></head>", requestDirectory));
            builder.Append(string.Format("<body><h1>{0}</h1><br/><ul>{1}{2}</ul></body></html>",
                 requestURL, filesList, foldersList));

            return builder.ToString();
        }
    }
}
