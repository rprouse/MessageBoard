using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Models;
using MessageBoard.Services;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mail;

        public HomeController(IMailService mail)
        {
            _mail = mail;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email: {2}{0}Website: {3}{0}Comment: {4}", Environment.NewLine, model.Name, model.Email, model.Website, model.Comment);
            if ( _mail.SendMail( "rob@prouse.org", "rob@prouse.org", "Web Contact", msg ) )
            {
                ViewBag.MailSent = true;
            }
            return View();
        }
    }
}
