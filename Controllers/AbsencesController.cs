using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using attendancesystem.Models;

namespace attendancesystem.Controllers
{
    public class AbsencesController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();

        // GET: Absences
        public ActionResult Index()
        {
            var absence = db.Absence.Include(a => a.Eleve).Include(a => a.Matiere);
            return View(absence.ToList());
        }

        // GET: Absences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absence.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // GET: Absences/Create
        public ActionResult Create()
        {
            ViewBag.eleve_id = new SelectList(db.Eleve, "id", "username");
            ViewBag.matiere_id = new SelectList(db.Matiere, "id", "name");
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dateheure,matiere_id,eleve_id")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Absence.Add(absence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.eleve_id = new SelectList(db.Eleve, "id", "username", absence.eleve_id);
            ViewBag.matiere_id = new SelectList(db.Matiere, "id", "name", absence.matiere_id);
            return View(absence);
        }

        // GET: Absences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absence.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            ViewBag.eleve_id = new SelectList(db.Eleve, "id", "username", absence.eleve_id);
            ViewBag.matiere_id = new SelectList(db.Matiere, "id", "name", absence.matiere_id);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dateheure,matiere_id,eleve_id")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(absence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.eleve_id = new SelectList(db.Eleve, "id", "username", absence.eleve_id);
            ViewBag.matiere_id = new SelectList(db.Matiere, "id", "name", absence.matiere_id);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public ActionResult Delete(int? id)
        {
            Absence absence = db.Absence.Find(id);
            db.Absence.Remove(absence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Absence absence = db.Absence.Find(id);
            db.Absence.Remove(absence);
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
