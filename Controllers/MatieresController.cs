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
    public class MatieresController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();

        // GET: Matieres
        public ActionResult Index()
        {
            var matiere = db.Matiere.Include(m => m.Professeur);
            return View(matiere.ToList());
        }

        // GET: Matieres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matiere matiere = db.Matiere.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }

        // GET: Matieres/Create
        public ActionResult Create()
        {
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username");
            return View();
        }

        // POST: Matieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,professeur_id")] Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                db.Matiere.Add(matiere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", matiere.professeur_id);
            return View(matiere);
        }

        // GET: Matieres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matiere matiere = db.Matiere.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", matiere.professeur_id);
            return View(matiere);
        }

        // POST: Matieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,professeur_id")] Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matiere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", matiere.professeur_id);
            return View(matiere);
        }

        // GET: Matieres/Delete/5
        public ActionResult Delete(int? id)
        {
            Matiere matiere = db.Matiere.Find(id);
            db.Matiere.Remove(matiere);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Matieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matiere matiere = db.Matiere.Find(id);
            db.Matiere.Remove(matiere);
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
