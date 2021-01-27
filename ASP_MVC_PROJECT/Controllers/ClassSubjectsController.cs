using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_PROJECT.Models;

namespace ASP_MVC_PROJECT.Controllers
{
    public class ClassSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassSubjects
        public ActionResult Index()
        {
            var classSubjects = db.ClassSubjects.Include(c => c.Class).Include(c => c.Subject);
            return View(classSubjects.ToList());
        }

        // GET: ClassSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            return View(classSubject);
        }

        // GET: ClassSubjects/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            return View();
        }

        // POST: ClassSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassSubjectID,ClassID,SubjectID")] ClassSubject classSubject)
        {
            var classSubjectObject = db.ClassSubjects.FirstOrDefault(item => item.ClassID == classSubject.ClassID && item.SubjectID == classSubject.SubjectID);
            if (ModelState.IsValid && classSubjectObject == null)
            {
                db.ClassSubjects.Add(classSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.ErrorMessage = "Przedmiot isniteje już w danej klasie!";
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            
            return View(classSubject);
        }

        // GET: ClassSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            return View(classSubject);
        }

        // POST: ClassSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassSubjectID,ClassID,SubjectID")] ClassSubject classSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", classSubject.SubjectID);
            return View(classSubject);
        }

        // GET: ClassSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            return View(classSubject);
        }

        // POST: ClassSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            db.ClassSubjects.Remove(classSubject);
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
