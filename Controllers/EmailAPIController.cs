using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Configuration;

namespace Hocgido2T.Controllers
{
    public class EmailAPIController : ApiController
    {
        [HttpGet]
        [Route("api/xac_nhan_qua_mail")]
        public IHttpActionResult Xac_nhan_qua_mail(string MaND, string Gmail)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                msg.Subject = "Xác nhận tài khoản Hocgido2T của bạn";
                msg.Body = "Http://"+ConfigurationManager.AppSettings["Hostname"].ToString()+ "/User/Information?MaND="+MaND;
                msg.From = new MailAddress("trongthoai001@gmail.com");
                msg.To.Add(Gmail);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("trongthoai001@gmail.com", "18008198");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                return Json(new 
                {
                    msg="ok"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    msg = "error"
                });
            }
        }
    }
}