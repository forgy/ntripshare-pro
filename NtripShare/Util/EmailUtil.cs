using System;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
namespace NtripClient.Util
{
    public class EmailUtil
    {
        //EmailUtil
        /// <summary>
        /// 服务邮箱
        /// </summary>
        private string _STEPNAME = "smtp.qq.com";
        /// <summary>
        /// 服务邮箱端口
        /// </summary>
        private int _STEPPORT = 587;
        /// <summary>
        /// 发送方邮箱
        /// </summary>
        private string _USEREMAIL;
        /// <summary>
        /// 发送方邮箱Smtp授权码
        /// </summary>
        private string _PASSWORD;
        /// <summary>
        /// 发送方邮箱归属人，昵称
        /// </summary>
        private string _EMAILBLONGER;
        private string email;
        private string emailBlonger;
        private string smtp;

        /// <summary>
        /// 邮箱配置
        /// </summary>
        /// <param name="email"></param>
        /// <param name="smtp"></param>
        public EmailUtil(string smtpServer,int port, string email, string password)
        {
            this._STEPNAME = smtpServer;
            this._STEPPORT = port;
            this._USEREMAIL = email;
            this._PASSWORD = password;
        }

        /// <summary>
        /// 邮箱发送类
        /// </summary>
        /// <param name="toEmaill">发送方邮箱</param>
        /// <param name="toEmailBlonger">发送方名称</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="text">发送的文字内容</param>
        /// <param name="html">发送的html内容</param>
        /// <param name="path">发送的附件,找不到的就自动过滤</param>
        /// <returns></returns>
        public string SendEmail(string toEmaill, string toEmailBlonger, string subject, string text, string html, string path)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                //发送方
                message.From.Add(new MailboxAddress(this._EMAILBLONGER, this._USEREMAIL));
                //接受方
                message.To.Add(new MailboxAddress(toEmailBlonger, toEmaill));
                //标题
                message.Subject = subject;
                //创建附件
                var multipart = new Multipart("mixed");
                //文字内容
                if (!string.IsNullOrEmpty(text))
                {
                    var plain = new TextPart(TextFormat.Plain)
                    {
                        Text = text
                    };
                    multipart.Add(plain);
                }
                //html内容
                if (!string.IsNullOrEmpty(html))
                {
                    var Html = new TextPart(TextFormat.Html)
                    {
                        Text = html
                    };
                    multipart.Add(Html);
                }
                if (!string.IsNullOrEmpty(path))
                {
                    var pathList = path.Split(';');
                    foreach (var p in pathList)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(p.Trim()))
                            {
                                var attimg = new MimePart()
                                {//"image", "png"方法里带参数的话
                                    ContentObject = new ContentObject(File.OpenRead(p), ContentEncoding.Default),
                                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                    ContentTransferEncoding = ContentEncoding.Base64,
                                    FileName = Path.GetFileName(path)
                                };
                                multipart.Add(attimg);
                            }
                        }
                        catch (FileNotFoundException ex)
                        {
                            //找不到文件就不提交附件了
                        }
                    }
                }
                //赋值邮件内容
                message.Body = multipart;
                //开始发送
                using (var client = new SmtpClient())
                {
                    client.Connect(this._STEPNAME, this._STEPPORT, false);
                    client.Authenticate(this._USEREMAIL, this._PASSWORD);
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "邮件发送成功";
            }
            catch (Exception)
            {
                return "邮箱发送失败";
            }
        }
        /// <summary>
        /// 邮箱发送类，不用输入用户昵称的
        /// </summary>
        /// <param name="toEmaill">发送方邮箱</param>
        /// <param name="toEmailBlonger">发送方名称</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="text">发送的文字内容</param>
        /// <param name="html">发送的html内容</param>
        /// <param name="path">发送的附件，多附件用;隔开</param>
        /// <returns></returns>
        public void SendEmail(string toEmaill, string subject, string text, string html, string path)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                //发送方
                message.From.Add(new MailboxAddress(this._USEREMAIL));
                //接受方
                message.To.Add(new MailboxAddress(toEmaill));
                //标题
                message.Subject = subject;
                //创建附件
                var multipart = new Multipart("mixed");
                //文字内容
                if (!string.IsNullOrEmpty(text))
                {
                    var plain = new TextPart(TextFormat.Plain)
                    {
                        Text = text
                    };
                    multipart.Add(plain);
                }
                //html内容
                if (!string.IsNullOrEmpty(html))
                {
                    var Html = new TextPart(TextFormat.Html)
                    {
                        Text = html
                    };
                    multipart.Add(Html);
                }
                if (!string.IsNullOrEmpty(path))
                {//修改为多附件，
                    var pathList = path.Split(';');
                    foreach (var p in pathList)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(p.Trim()))
                            {
                                var attimg = new MimePart()
                                {//"image", "png"方法里带参数的话
                                    ContentObject = new ContentObject(File.OpenRead(p), ContentEncoding.Default),
                                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                    ContentTransferEncoding = ContentEncoding.Base64,
                                    FileName = Path.GetFileName(path)
                                };
                                multipart.Add(attimg);
                            }
                        }
                        catch (FileNotFoundException ex)
                        {
                            //找不到文件就不提交附件了
                        }
                    }
                }
                //赋值邮件内容
                message.Body = multipart;
                //开始发送
                using (var client = new SmtpClient())
                {
                    client.Connect(this._STEPNAME, this._STEPPORT, false);
                    client.Authenticate(this._USEREMAIL, this._PASSWORD);
                    client.Send(message);
                    client.Disconnect(true);
                }
                // return "邮件发送成功";
            }
            catch (Exception ex)
            {
                // return "邮箱发送失败";
            }
        }
    }
}