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
    [Authorize(Roles = RoleName.Professor)]
    public class GradesController : Controller
    {
        private ApplicationDbContext _context;

        public GradesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Grades
        // Made by: Muhammed Yasin Yildirim
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
            return View();
        }

        // POST: Grades/Create
        [HttpPost]
        public ActionResult Create(GradeViewModel gradeViewModel)
        {
            return View();
        }

        // GET: Assess the student
        // Made by: Muhammed Yasin Yildirim
        public ActionResult Assess()
        {
            // Getting the authenticated user id
            string userId = User.Identity.GetUserId<string>();

            // Professors which assesses the student
            var professor = _context.Professors.FirstOrDefault(p => p.Id == userId);

            if (professor == null)
                return HttpNotFound();

            // send data to view, in this case, professor courses
            var viewModel = new GradeViewModel()
            {
                ProfessorCourses = professor.Courses.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        // POST: /Grades/Assess/1
        // Made by: Lejla Hodžić
        public ActionResult Assess(string studentId, GradeViewModel gradeViewModel)
        {
            string userId = User.Identity.GetUserId<string>(); // getting authenticated user (professor id)
            if (!ModelState.IsValid)
            {
                var professor = _context.Professors.FirstOrDefault(p => p.Id == userId); // professor which assesses the student

                if (professor == null)
                    return HttpNotFound();

                var viewModel = new GradeViewModel()
                {
                    ProfessorCourses = professor.Courses.ToList()
                };

                return View(viewModel);
            }

            // Inserting grade with the details like specified
            var grade = new Grade()
            {
                Value = gradeViewModel.Value, // grade value
                CourseId = gradeViewModel.CourseId,
                ProfessorId = userId, // professor which is assessing
                StudentId = studentId // student which is assessed
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
