using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClasSy.Helpers;
using ClasSy.Models;
using ClasSy.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClasSy.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext _context;

        public StudentsController()
        {
            _context = new ApplicationDbContext();
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
        public ActionResult Details(string id)
        {
            var student = _context.Students.Include(s => s.SchoolClass).SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            var viewModel = new StudentViewModel()
            {
                SchoolClasses = _context.SchoolClasses.ToList(),
                Student = student,
                Courses = _context.Courses.ToList()
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

            //_context.Students.Add(student);
            //_context.SaveChanges();

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
        // GET: Students1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var studentViewModel = new StudentViewModel
            {
                LastName = student.LastName,
                FirstName = student.FirstName,
                BirthDate = student.BirthDate,
                Address = student.Address,
                BirthPlace = student.BirthPlace,
                ClassPresident = student.ClassPresident,
                Email = student.Email,
                SchoolClass = student.SchoolClass,
                PhoneNumber = student.PhoneNumber,
                Password = student.PasswordHash,
                SchoolClassId = student.SchoolClassId

            };
            return View(studentViewModel);
        }
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            var student = _context.Students.Find(id);
            var grades = _context.Grades.Where(g => g.StudentId == student.Id);
            var courses = _context.Courses.Where(c => c.Id == grades.FirstOrDefault().CourseId);
            _context.Courses.RemoveRange(courses);
            _context.Grades.RemoveRange(grades);
            _context.Students.Remove(student);
            
            
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
