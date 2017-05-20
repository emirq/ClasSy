using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClasSy.Helpers;
using ClasSy.Models;
using ClasSy.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClasSy.Controllers
{
    // Made by: Lejla Hodžić
    public class StudentsController : Controller
    {
        private ApplicationDbContext _context; // database instance

        public StudentsController()
        {
            _context = new ApplicationDbContext(); // creating new database instance
        }

        // GET: Students
        public ActionResult Index()
        {
            var viewModel = new StudentViewModel()
            {
                Students = _context.Students.Include(s => s.SchoolClass).ToList(),
            };

            return View(viewModel);
        }

        // GET: Students/Details/5
        // Showing Student profile, with all details
        public ActionResult Details(string id)
        {
            var student = _context.Students.Include(s => s.SchoolClass).SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            var viewModel = new StudentViewModel()
            {
                Student = student, // student model with modified data
                Courses = _context.Courses.ToList() // courses which student attend
            };

            return View(viewModel);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var viewModel = new StudentViewModel()
            {
                SchoolClasses = _context.SchoolClasses.ToList()
            };

            return View(viewModel);
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            // checking if validation passes, if not, refresh page
            if (!ModelState.IsValid)
            {
                var viewModel = new StudentViewModel()
                {
                    SchoolClasses = _context.SchoolClasses.ToList()
                };

                return View(viewModel);
            }

            var student = new Student()
            {
                FirstName = studentViewModel.FirstName,
                LastName = studentViewModel.LastName,
                BirthDate = studentViewModel.BirthDate,
                Address = studentViewModel.Address,
                BirthPlace = studentViewModel.BirthPlace,
                Email = studentViewModel.Email,
                PhoneNumber = studentViewModel.PhoneNumber,
                UserName = studentViewModel.Email,
                SchoolClassId = studentViewModel.SchoolClassId
            };

            // The UserStore<T> object is injected into authentication manager which is used to identify and authenticate the UserStore<T> identity
            // The UserManager<T> reference acts as the authenticator for the UserStore<T> identity.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roleHelper = new RoleHelper(_context);
            roleHelper.CreateRoleIfDoesntExist(RoleName.Student);

            var createUser = userManager.Create(student, studentViewModel.Password);

            if (createUser.Succeeded)
                userManager.AddToRole(student.Id, RoleName.Student);

            return RedirectToAction("Index");
            
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
