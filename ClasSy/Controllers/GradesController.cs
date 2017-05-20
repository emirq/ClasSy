using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClasSy.Models;
using ClasSy.ViewModels;
using Microsoft.AspNet.Identity;

namespace ClasSy.Controllers
{
    public class GradesController : Controller
    {
        private ApplicationDbContext _context;

        public GradesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Grades
        public ActionResult Index()
        {
            return View();
        }

        // GET: Grades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId<string>();
            var professor = _context.Professors.FirstOrDefault(p => p.Id == userId);

            if (professor == null)
                return HttpNotFound();
            
            var viewModel = new GradeViewModel()
            {
                ProfessorCourses = professor.Courses.ToList()
            };

            return View(viewModel);
        }

        // POST: Grades/Create
        [HttpPost]
        public ActionResult Create(GradeViewModel gradeViewModel)
        {
            

            return View();
        }

        public ActionResult Assess()
        {
            string userId = User.Identity.GetUserId<string>();
            var professor = _context.Professors.FirstOrDefault(p => p.Id == userId);

            if (professor == null)
                return HttpNotFound();

            var viewModel = new GradeViewModel()
            {
                ProfessorCourses = professor.Courses.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Assess(string studentId, GradeViewModel gradeViewModel)
        {
            string userId = User.Identity.GetUserId<string>();
            if (!ModelState.IsValid)
            {

                var professor = _context.Professors.FirstOrDefault(p => p.Id == userId);

                if (professor == null)
                    return HttpNotFound();

                var viewModel = new GradeViewModel()
                {
                    ProfessorCourses = professor.Courses.ToList()
                };

                return View(viewModel);
            }

            var grade = new Grade()
            {
                Value = gradeViewModel.Value,
                CourseId = gradeViewModel.CourseId,
                ProfessorId = userId,
                StudentId = studentId
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return RedirectToAction("Details", "Students", new { id = studentId });
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Grades/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GradeViewModel gradeViewModel)
        {
            return View();
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Grades/Delete/5
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
