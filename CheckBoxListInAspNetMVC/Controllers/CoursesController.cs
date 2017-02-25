using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CheckBoxListInAspNetMVC.Models;

namespace CheckBoxListInAspNetMVC.Controllers
{
    public class CoursesController : Controller
    {
        private CheckBoxListDbConetxt db = new CheckBoxListDbConetxt();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
       

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.AllDepartments = db.Departments.ToList();
            return View();
        }

        // POST: Courses/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Course course, List<int> selectedDepartments)
        {

            if (ModelState.IsValid)
            {
                if (selectedDepartments != null)
                {
                    foreach (var departmentId in selectedDepartments)
                    {
                        Department department = db.Departments.Find(departmentId);
                        course.Departments.Add(department);

                    }
                }
                
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Courses");
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
               return HttpNotFound();
            }
            ViewBag.AllDepartments = db.Departments.ToList();
            return View(course);
        }

        // POST: Courses/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Course course, List<int> selectedDepartments)
        {
            if (ModelState.IsValid)
            {
                Course courseToBeUpdated = db.Courses.FirstOrDefault(x => x.Id == course.Id);
                courseToBeUpdated.Name = course.Name;
                courseToBeUpdated.Departments.Clear();
                if (selectedDepartments != null)
                {
                    foreach (var departmentId in selectedDepartments)
                    {
                        Department department = db.Departments.Find(departmentId);
                        courseToBeUpdated.Departments.Add(department);
                    }
                }

                db.Entry(courseToBeUpdated).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
