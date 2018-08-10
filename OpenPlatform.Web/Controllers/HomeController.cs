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
using Model;
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




        public ActionResult Index()
        {
            //第一步：获取开放平台授权地址
            string authorizeUrl = oauthClient.GetAuthorizeUrl(ResponseType.Code);
            ViewData["AuthorizeUrl"] = authorizeUrl;
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
            var db = DBContext.CreateContext();
            var user = db.QQUser_info.Where(x => x.openId.Contains(oauthClient.Token.OAuthId)).FirstOrDefault();
            if (user == null)
            {
                user = new QQUser_info
                {
                    nickname = oauthClient.Token.User.Nickname,
                    openId = oauthClient.Token.OAuthId,
                    figureurl = oauthClient.Token.User.AvatarUrl,
                    gender = oauthClient.Token.User.Sex,
                };
                db.QQUser_info.Add(user);
                db.SaveChanges();
                StringBuilder str = new StringBuilder();
                str.AppendLine("<h2>恭喜您,QQ认证成功 </h2><br />");
                str.AppendLine("****************获取信息begin**************** <br />");
                str.AppendLine("获取到您的昵称:" + nickName + "大智障 <br />");
                str.AppendLine("获取到您的openId:" + user.openId + "  <br />");
                str.AppendLine("获取到您的头像: <img src=" + user.figureurl + "  />  <br /> ");
                str.AppendLine("****************获取信息end**************** <br /> ");

            }
            ViewBag.user = user;
            return View();
        }



    }
}
