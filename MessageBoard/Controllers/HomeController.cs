﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Data;
using MessageBoard.Models;
using MessageBoard.Services;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mail;
        private readonly IMessageBoardRepository _repo;

        public HomeController(IMailService mail, IMessageBoardRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var topics = _repo.GetTopics().OrderByDescending(t => t.Created).Take(25).ToList();
            return View(topics);
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
            if(_mail.SendMail("rob@prouse.org", "rob@prouse.org", "Web Contact", msg))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        [Authorize]
        public ActionResult MyMessages()
        {
            return View();
        }
    }
}
