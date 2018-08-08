using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using OpenPlatform.Tool;
using OpenPlatform.Tool.Commom;
using OpenPlatform.Tool.OAuthClient.TencentQQ;
using OpenPlatform.Tool.Utils;
using OpenPlatform.Web.Common;
using OpenPlatform.Web.Models;


namespace OpenPlatform.Web.Controllers
{
    public class HomeController : Controller
    {
        private string PlatformCode = "qq";
        static readonly string ClientId = ConfigurationManager.AppSettings["clientId"];
        static readonly string ClientSecret = ConfigurationManager.AppSettings["clientSecret"];
        static readonly string CallbackUrl = ConfigurationManager.AppSettings["callbackUrl"];

        private static string nickName = "";
        private IOAuthClient oauthClient;
        public HomeController()
        {

            oauthClient = new TencentQQClient(ClientId, ClientSecret, CallbackUrl);
            oauthClient.Option.State = PlatformCode;
        }




        public ActionResult IndexA()
        {
            //第一步：获取开放平台授权地址
            string authorizeUrl = oauthClient.GetAuthorizeUrl(ResponseType.Code);
            ViewData["AuthorizeUrl"] = authorizeUrl;

            if (!string.IsNullOrEmpty(nickName))
            {
                ViewData["NickName"] = nickName;
                ViewData["result"] = true;
            }
            return View();
        }


        public ActionResult JSQQ(string name = "")
        {
            //第一步：获取开放平台授权地址
            string authorizeUrl = oauthClient.GetAuthorizeUrl(ResponseType.Code);
            //ViewData["AuthorizeUrl"] = authorizeUrl;
            ViewData["name"] = name;
            return View();
        }
        public ActionResult JSQQCallback(string name, string openid, string otype, string token)
        {
            return View();
        }


        public ActionResult Labe()
        {


            return View();
        }
       

        /// <summary>
        /// 回调地址
        /// </summary>
        /// <returns></returns>
        public ActionResult QQLoginCallback()
        {
            string xmlDataPath = Server.MapPath("~/DataXML/AuthorizeXML.xml");
            string serverCallBackCode = Request["code"];
            //第二步：认证成功获取Code
            Dictionary<String, IOAuthClient> m_oauthClients = new Dictionary<string, IOAuthClient>();
            AuthToken accessToken = oauthClient.GetAccessTokenByAuthorizationCode(serverCallBackCode);
            if (accessToken == null)
            {
                return Content("认证失败");
            
            }
            nickName = oauthClient.Token.User.Nickname;
            string openId = oauthClient.Token.OAuthId;
            string AvatarUrl = oauthClient.Token.User.AvatarUrl;
            string responseResult = oauthClient.Token.TraceInfo;


            dynamic userInfo = DynamicHelper.FromJSON(responseResult);
            var info = XMLSerializeHelper.XmlDeserializeFromFile<List<UserInfo>>(xmlDataPath);
            StringBuilder str = new StringBuilder();
            if (info != null && !info.Where(s => s.OpenId == openId).Any())
            {
                #region 保存数据
                ////提取xml文档
                //XmlDocument xd = new XmlDocument();
                //xd.Load(xmlDataPath);
                ////获取根节点
                //XmlNode root = xd.DocumentElement;
                ////创建元素
                //XmlElement newItem = xd.CreateElement("UserInfo");
                //newItem.AppendChild(newItem);
                //XmlElement newOpenId = xd.CreateElement("OpenId");
                //XmlElement newNickName = xd.CreateElement("Nickname");
                //XmlElement figureurl_2 = xd.CreateElement("figureurl_2");
                ////设置内容
                //newOpenId.InnerText = openId;
                //newNickName.InnerText = nickName;
                //figureurl_2.InnerText = userInfo.figureurl_2;
                ////添加节点
                //root.AppendChild(newItem);
                //newItem.AppendChild(newOpenId);
                //newItem.AppendChild(newNickName);
                //newItem.AppendChild(figureurl_2);
                ////保存xml文档
                //xd.Save(Server.MapPath(xmlDataPath));

                #endregion
                str.AppendLine("<h2>恭喜您,QQ认证成功 </h2><br />");
                str.AppendLine("****************获取信息begin**************** <br />");
                str.AppendLine("获取到您的昵称:" + nickName + "大智障 <br />");
                str.AppendLine("获取到您的openId:" + openId + "  <br />");
                str.AppendLine("获取到您的头像: <img src=" + AvatarUrl + "  />  <br /> ");
                str.AppendLine("****************获取信息end**************** <br /> ");
            }
            ViewBag.user = oauthClient.Token.User;
            return View();
        }

        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region  添加Post参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
