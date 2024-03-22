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
    public class ElevesController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();

        // GET: Eleves
        public ActionResult Index()
        {
            var eleve = db.Eleve.Include(e => e.Filiere);
            return View(eleve.ToList());
        }

        // GET: Eleves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleve eleve = db.Eleve.Find(id);
            if (eleve == null)
            {
                return HttpNotFound();
            }
            return View(eleve);
        }

        // GET: Eleves/Create
        public ActionResult Create()
        {
            ViewBag.filiere_id = new SelectList(db.Filiere, "id", "name");
            return View();
        }

        // POST: Eleves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,name,last_name,filiere_id")] Eleve eleve)
        {
            if (ModelState.IsValid)
            {
                db.Eleve.Add(eleve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.filiere_id = new SelectList(db.Filiere, "id", "name", eleve.filiere_id);
            return View(eleve);
        }

        // GET: Eleves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleve eleve = db.Eleve.Find(id);
            if (eleve == null)
            {
                return HttpNotFound();
            }
            ViewBag.filiere_id = new SelectList(db.Filiere, "id", "name", eleve.filiere_id);
            return View(eleve);
        }

        // POST: Eleves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,name,last_name,filiere_id")] Eleve eleve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eleve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.filiere_id = new SelectList(db.Filiere, "id", "name", eleve.filiere_id);
            return View(eleve);
        }

        // GET: Eleves/Delete/5
        public ActionResult Delete(int? id)
        {
            Eleve eleve = db.Eleve.Find(id);
            db.Eleve.Remove(eleve);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Eleves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eleve eleve = db.Eleve.Find(id);
            db.Eleve.Remove(eleve);
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
