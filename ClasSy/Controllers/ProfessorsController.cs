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
        // entity framework database instance
        private ApplicationDbContext _context;

        // Instantiating context
        public ProfessorsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Professors
        public ActionResult Index()
        {
            // view model which is sent to the view
            var viewModel = new ProfessorViewModel()
            {
                Professors = _context.Professors.ToList()
            };

            return View(viewModel);
        }

        // GET: Professors/Details/5
        public ActionResult Details(string id)
        {
            // find professor with specified id
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
        public ActionResult Create(ProfessorViewModel professorViewModel)
        {
            // checking if validation passes, if not, refresh page with validation errors displayed
            if (!ModelState.IsValid)
            {
                var viewModel = new ProfessorViewModel()
                {
                    Courses = _context.Courses.ToList()
                };

                return View(viewModel);
            }

            // create new professor model prepared for database insertion
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

            // The UserStore<T> object is injected into authentication manager which is used to identify and authenticate the UserStore<T> identity
            // The UserManager<T> reference acts as the authenticator for the UserStore<T> identity.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roleHelper = new RoleHelper(_context);
            roleHelper.CreateRoleIfDoesntExist(RoleName.Professor); // RoleName is a class for defining user roles

            var createUser = userManager.Create(professor, professorViewModel.Password); // password is being hashed

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

            // Collection which is going to be populated in a loop with courses that professor selected previously
            ICollection<int> CourseList = new List<int>();

            foreach (var course in professor.Courses)
            {
                CourseList.Add(course.Id);
            }

            // data to be displayed in a view
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
            // check is validation passes, if not redirect back with validation errors
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

            // professor object populated with newly created professor object
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

            _context.SaveChanges(); // saving changes in database

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
