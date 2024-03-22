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
    public class FilieresController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();

        // GET: Filieres
        public ActionResult Index()
        {
            var filiere = db.Filiere.Include(f => f.Professeur);
            return View(filiere.ToList());
        }

        // GET: Filieres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filiere filiere = db.Filiere.Find(id);
            if (filiere == null)
            {
                return HttpNotFound();
            }
            return View(filiere);
        }

        // GET: Filieres/Create
        public ActionResult Create()
        {
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username");
            return View();
        }

        // POST: Filieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,professeur_id")] Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                db.Filiere.Add(filiere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", filiere.professeur_id);
            return View(filiere);
        }

        // GET: Filieres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filiere filiere = db.Filiere.Find(id);
            if (filiere == null)
            {
                return HttpNotFound();
            }
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", filiere.professeur_id);
            return View(filiere);
        }

        // POST: Filieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,professeur_id")] Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filiere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.professeur_id = new SelectList(db.Professeur, "id", "username", filiere.professeur_id);
            return View(filiere);
        }

        // GET: Filieres/Delete/5
        public ActionResult Delete(int? id)
        {
            Filiere filiere = db.Filiere.Find(id);
            db.Filiere.Remove(filiere);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Filieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filiere filiere = db.Filiere.Find(id);
            db.Filiere.Remove(filiere);
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
