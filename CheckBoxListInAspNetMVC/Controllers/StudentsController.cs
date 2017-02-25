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
    public class StudentsController : Controller
    {
        private CheckBoxListDbConetxt db = new CheckBoxListDbConetxt();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpGet]
        public ActionResult SendCourseById(int id)
        {
            var courses = db.Departments.Find(id).Courses.Select(x => new
            {
                x.Id,
                x.Name
            }).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        // GET: Students/Create
        public ActionResult Create()
        {

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DepartmentId,Email")] Student student, List<int> selectedCourses)
        {
            if (ModelState.IsValid)
            {
                if (selectedCourses != null)
                {
                    foreach (var item in selectedCourses)
                    {
                        Course course = db.Courses.Find(item);
                        student.Courses.Add(course);
                    }
                }
                
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            var departmentId = student.DepartmentId;
            if (departmentId != null)
            {
                ViewBag.DepartmentCourses = db.Departments.Find(departmentId).Courses.ToList();
            }
           
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student,  List<int> selectedCourses)
        {
            
            if (ModelState.IsValid)
            {
                var studentToBeUpdated = db.Students.FirstOrDefault(x => x.Id == student.Id);
                studentToBeUpdated.Name = student.Name;
                studentToBeUpdated.DepartmentId = student.DepartmentId;
                studentToBeUpdated.Email = student.Email;
                studentToBeUpdated.Courses.Clear();

                if (selectedCourses != null)
                {
                    foreach (var item in selectedCourses)
                    {
                        var course = db.Courses.Find(item);
                        studentToBeUpdated.Courses.Add(course);
                    }
                }
                

                db.Entry(studentToBeUpdated).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentCourses = db.Departments.Find(student.DepartmentId).Courses.ToList();

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            student.Courses.Clear();
            db.Students.Remove(student);
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
