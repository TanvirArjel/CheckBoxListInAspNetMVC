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
    public class DepartmentsController : Controller
    {
        private CheckBoxListDbConetxt db = new CheckBoxListDbConetxt();

        // GET: Departments
        public ActionResult Index()
        {
            var departmentsList = db.Departments.ToList();
            return View(departmentsList);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.AllCourses = db.Courses.ToList();
            return View();
        }

        // POST: Departments/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department, List<int> selectedCourses)
        {
            
            if (ModelState.IsValid)
            {
                if (selectedCourses != null)
                {
                    foreach (var item in selectedCourses)
                    {
                        var course = db.Courses.Find(item);
                        department.Courses.Add(course);
                    }
                }
                
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);

            ViewBag.AllCourses = db.Courses.ToList();
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department, List<int> selectedCourses)
        {
         
            if (ModelState.IsValid)
            {
                var departmentToBeUpdated = db.Departments.FirstOrDefault(x => x.Id == department.Id);
                departmentToBeUpdated.Name = department.Name;
                departmentToBeUpdated.Courses.Clear();

                if (selectedCourses != null)
                {
                    foreach (var item in selectedCourses)
                    {
                        var course = db.Courses.Find(item);
                        departmentToBeUpdated.Courses.Add(course);
                    }
                }

                db.Entry(departmentToBeUpdated).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Include(s => s.Students).FirstOrDefault(x => x.Id == id);
            department.Courses.Clear();
            List<Student> students = db.Students.Where(x => x.DepartmentId == id).ToList();
            
            foreach (Student student in students)
            {
                student.Courses.Clear();
            }

            db.Departments.Remove(department);
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
