using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClasSy.Models;
using ClasSy.ViewModels;
using Microsoft.AspNet.Identity;

namespace ClasSy.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        
        // Made by: Lejla Hodžić
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Professor))
                return View("ProfessorDashboard");

            if (User.IsInRole(RoleName.Parent))
            {
                string userId = User.Identity.GetUserId();
                var parent = _context.Parents.Include(s => s.Students).SingleOrDefault(p => p.Id == userId);

                if (parent == null)
                    return HttpNotFound();

                var viewModel = new ParentViewModel()
                {
                    Parent = parent
                };
                return View("ParentDashboard", viewModel);
            }
                

            if (User.IsInRole(RoleName.Student))
                return View("StudentDashboard");

            if (User.IsInRole(RoleName.Admin))
                return View("AdminDashboard");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "School Management System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}