using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClasSy.Models;
using ClasSy.ViewModels;

namespace ClasSy.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Courses
        public ActionResult Index()
        {
            var courses = _context.Courses.ToList();

            var viewModel = new CourseViewModel() { Courses = courses };

            return View(viewModel);
        }

        // GET: Create course
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Courses.Add(new Course() {Name = course.Name});
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Courses/Edit/1
        public ActionResult Edit(int id)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (course == null)
                return HttpNotFound();

            var viewModel = new CourseViewModel() { Name = course.Name };

            return View(viewModel);
        }

        // POST: Courses/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, CourseViewModel courseViewModel)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (course == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                var viewModel = new CourseViewModel() { Name = course.Name };
                return View(viewModel);
            }

            course.Name = courseViewModel.Name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Courses/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CourseViewModel courseViewModel)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (course == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }
    }
}