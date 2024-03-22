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
    public class ProfesseursController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();

        // GET: Professeurs
        public ActionResult Index()
        {
            return View(db.Professeur.ToList());
        }

        // GET: Professeurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professeur professeur = db.Professeur.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // GET: Professeurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professeurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,name,last_name")] Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                db.Professeur.Add(professeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professeur);
        }

        // GET: Professeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professeur professeur = db.Professeur.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // POST: Professeurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,name,last_name")] Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professeur);
        }

        // GET: Professeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professeur professeur = db.Professeur.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // POST: Professeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professeur professeur = db.Professeur.Find(id);
            db.Professeur.Remove(professeur);
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
