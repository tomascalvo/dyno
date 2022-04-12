﻿using DevPath.Models;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public HomeController()
        {
            // the context is a disposable object that must be properly disposed of
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View(_context.Users.Count());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}