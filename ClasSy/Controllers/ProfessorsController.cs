using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
    // Made by: Emir Kurtanović
    public class ProfessorsController : Controller
    {
        private ApplicationDbContext _context;
        
        public ProfessorsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Professors
        public ActionResult Index()
        {
            var viewModel = new ProfessorViewModel()
            {
                Professors = _context.Professors.ToList()
            };

            return View(viewModel);
        }

        // GET: Professors/Details/5
        public ActionResult Details(string id)
        {
            var professor = _context.Professors.Include(c => c.Courses).SingleOrDefault(p => p.Id == id);

            if (professor == null)
                return HttpNotFound();

            var viewModel = new ProfessorViewModel()
            {
                Professor = professor
            };

            return View(viewModel);
        }

        // GET: Professors/Create
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            var viewModel = new ProfessorViewModel()
            {
                Courses = _context.Courses.ToList()
            };

            return View(viewModel);
        }

        // POST: Professors/Create
        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(ProfessorViewModel professorViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProfessorViewModel()
                {
                    Courses = _context.Courses.ToList()
                };

                return View(viewModel);
            }
            
            var professor = new Professor()
            {
                FirstName = professorViewModel.FirstName,
                LastName = professorViewModel.LastName,
                BirthDate = professorViewModel.BirthDate,
                Address = professorViewModel.Address,
                BirthPlace = professorViewModel.BirthPlace,
                Email = professorViewModel.Email,
                PhoneNumber = professorViewModel.PhoneNumber,
                UserName = professorViewModel.Email,
            };
            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roleHelper = new RoleHelper(_context);
            roleHelper.CreateRoleIfDoesntExist(RoleName.Professor);

            var createUser = userManager.Create(professor, professorViewModel.Password);

            if (createUser.Succeeded)
                userManager.AddToRole(professor.Id, RoleName.Professor);

            // insert courses which professor has selected
            foreach (var courseId in professorViewModel.SelectedCourseList)
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO ProfessorCourses(Professor_Id, Course_Id) VALUES('" + professor.Id + "', '" + courseId + "')");
            }
            
            return RedirectToAction("Index");
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(string id)
        {
            var professor = _context.Professors.SingleOrDefault(p => p.Id == id);

            if (professor == null)
                return HttpNotFound();
            
            ICollection<int> CourseList = new List<int>();

            foreach (var course in professor.Courses)
            {
                CourseList.Add(course.Id);
            }
            
            var viewModel = new ProfessorViewModel()
            {
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Email = professor.Email,
                BirthDate = professor.BirthDate,
                PhoneNumber = professor.PhoneNumber,    
                ClassTeacher = professor.ClassTeacher,
                Courses = _context.Courses.ToList(),
                SelectedCourseList = CourseList
            };  

            return View(viewModel);
        }

        // POST: Professors/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ProfessorViewModel professorViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProfessorViewModel()
                {
                    Courses = _context.Courses.ToList()
                };

                return View(viewModel);
            }

            var professor = _context.Professors.SingleOrDefault(p => p.Id == id);

            if (professor == null)
                return HttpNotFound();
            
            professor.FirstName = professorViewModel.FirstName;
            professor.LastName = professorViewModel.LastName;
            professor.BirthDate = professorViewModel.BirthDate;
            professor.Address = professorViewModel.Address;
            professor.BirthPlace = professorViewModel.BirthPlace;
            professor.Email = professorViewModel.Email;
            professor.PhoneNumber = professorViewModel.PhoneNumber;
            professor.UserName = professorViewModel.Email;
            professor.ClassTeacher = professorViewModel.ClassTeacher;

            professor.Courses.Clear();

            _context.SaveChanges();

            if (professorViewModel.SelectedCourseList != null)
            {
                foreach (var courseId in professorViewModel.SelectedCourseList)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO ProfessorCourses(Professor_Id, Course_Id) VALUES('" + professor.Id + "', '" + courseId + "')");
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Professors/Delete/5
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
