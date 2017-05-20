using System;
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
    public class ParentsController : Controller
    {
        private ApplicationDbContext _context;

        public ParentsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Parents
        public ActionResult Index()
        {
            var viewModel = new ParentViewModel()
            {
                Parents = _context.Parents.ToList(),
                Students = _context.Students.ToList()
            };

            return View(viewModel);
        }

        // GET: Parents/Details/5
        public ActionResult Details(string id)
        {
            var parent = _context.Parents.Include(s => s.Students).SingleOrDefault(p => p.Id == id);

            if (parent == null)
                return HttpNotFound();

            var viewModel = new ParentViewModel()
            {
                Parent = parent
            };

            return View(viewModel);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            var viewModel = new ParentViewModel()
            {
                Students = _context.Students.ToList()
            };

            return View(viewModel);
        }

        // POST: Parents/Create
        [HttpPost]
        public ActionResult Create(ParentViewModel parentViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ParentViewModel()
                {
                    Students = _context.Students.ToList()
                };

                return View(viewModel);
            }

            var parent = new Parent()
            {
                FirstName = parentViewModel.FirstName,
                LastName = parentViewModel.LastName,
                BirthDate = parentViewModel.BirthDate,
                Address = parentViewModel.Address,
                BirthPlace = parentViewModel.BirthPlace,
                Email = parentViewModel.Email,
                PhoneNumber = parentViewModel.PhoneNumber,
                UserName = parentViewModel.Email,
            };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roleHelper = new RoleHelper(_context);
            roleHelper.CreateRoleIfDoesntExist(RoleName.Parent);

            var createUser = userManager.Create(parent, parentViewModel.Password);

            if (createUser.Succeeded)
                userManager.AddToRole(parent.Id, RoleName.Parent);

            foreach (var studentId in parentViewModel.SelectedStudentList)
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO ParentStudents(Parent_Id, Student_Id) VALUES('" + parent.Id + "', '" + studentId + "')");
            }


            return RedirectToAction("Index");
        }

        // GET: Parents/Edit/5
        public ActionResult Edit(string id)
        {
            var parent = _context.Parents.SingleOrDefault(p => p.Id == id);

            if (parent == null)
                return HttpNotFound();

            ICollection<string> studentsList = new List<string>();

            foreach (var student in parent.Students)
            {
                studentsList.Add(student.Id);
            }

            var viewModel = new ParentViewModel()
            {
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                Email = parent.Email,
                BirthDate = parent.BirthDate,
                PhoneNumber = parent.PhoneNumber,
                Students = _context.Students.ToList(),
                SelectedStudentList = studentsList
            };

            return View(viewModel);
        }

        // POST: Parents/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ParentViewModel parentViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ParentViewModel()
                {
                    Students = _context.Students.ToList()
                };

                return View(viewModel);
            }

            var parent = _context.Parents.SingleOrDefault(p => p.Id == id);

            if (parent == null)
                return HttpNotFound();


            parent.FirstName = parentViewModel.FirstName;
            parent.LastName = parentViewModel.LastName;
            parent.BirthDate = parentViewModel.BirthDate;
            parent.Address = parentViewModel.Address;
            parent.BirthPlace = parentViewModel.BirthPlace;
            parent.Email = parentViewModel.Email;
            parent.PhoneNumber = parentViewModel.PhoneNumber;
            parent.UserName = parentViewModel.Email;
            


            parent.Students.Clear();

            _context.SaveChanges();

            if (parentViewModel.SelectedStudentList != null)
            {
                foreach (var studentId in parentViewModel.SelectedStudentList)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO ParentStudents(Parent_Id, Student_Id) VALUES('" + parent.Id + "', '" + studentId + "')");
                }
            }
            


            return RedirectToAction("Index");
        }

        // GET: Parents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parents/Delete/5
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
