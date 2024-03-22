using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using attendancesystem.Models;

namespace attendancesystem.Controllers
{
    public class HomeController : Controller
    {
        private iiteAttendanceEntities db = new iiteAttendanceEntities();
        public ActionResult Index()

        {
            ViewBag.NombreEleves = db.Eleve.Count();
            ViewBag.NombreAbsences = db.Absence.Count();
            ViewBag.NombreMatieres = db.Matiere.Count();
            ViewBag.NombreFilieres = db.Filiere.Count();
            ViewBag.Students = db.Eleve.ToList();
            ViewBag.Matieres = db.Matiere.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetAbsenceCount(int studentId)
        {
            var absenceCount = db.Absence.Count(a => a.eleve_id == studentId);
            return Content(absenceCount.ToString(), "text/plain");
        }
        public ActionResult GetAbsencesForSubject(int subjectId)
        {
            // Query absences for the selected subject
            var absences = db.Absence.Where(a => a.matiere_id == subjectId).ToList();

            // Count the absences and get the names of absent students
            var absenceCount = absences.Count;
            var absentStudents = absences.Select(a => new { name = a.Eleve.name, last_name = a.Eleve.last_name }).ToList();

            // Return the result as JSON
            return Json(new { absenceCount, absentStudents }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAbsencesForSubjectchart(int studentId)
        {
            var absences = db.Absence.Where(a => a.eleve_id == studentId).ToList();
            var subjects = db.Matiere.ToList();
            var absenceCounts = new Dictionary<string, int>();

            foreach (var subject in subjects)
            {
                var count = absences.Count(a => a.matiere_id == subject.id);
                absenceCounts.Add(subject.name, count);
            }

            return Json(absenceCounts, JsonRequestBehavior.AllowGet);
        }


    }
}
