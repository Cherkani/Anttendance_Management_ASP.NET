using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using iiteAttendance.Models;

namespace attendancesystem.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "data source=aymen\\MSSQLSERVER2023;database=iiteAttendance;integrated security=SSPI;";
        }

        [HttpPost]
        public ActionResult Verify(Account account)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT username, password FROM Professeur WHERE username=@username";
            com.Parameters.AddWithValue("@username", account.username);

            dr = com.ExecuteReader();

            if (dr.Read())
            {
                string dbPassword = dr["password"].ToString();
                if (dbPassword == account.password)
                {
                    con.Close();
                    return RedirectToAction("Index","Home");
                }
            }
            con.Close();

            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT username, password ,id ,name FROM eleve WHERE username=@usernames";
            com.Parameters.AddWithValue("@usernames", account.username);

            dr = com.ExecuteReader();

            if (dr.Read())
            {
                string dbPassword = dr["password"].ToString();
                int eleveId = Convert.ToInt32(dr["id"]);
                string studentName = dr["name"].ToString();
                ViewBag.StudentName = studentName;

                if (dbPassword == account.password)
                {
                    con.Close();
                    Dictionary<string, int> absenceData;
                    int totalAbsences = FetchStudentAbsenceCountBySubject(eleveId, out absenceData);
                    ViewBag.AbsenceData = absenceData;
                    ViewBag.TotalAbsences = totalAbsences;
                    return View("Student");
                }
            }

            con.Close();
            ViewBag.ErrorMessage = "Invalid credentials.";
            return View("Login");
        }

        private int FetchStudentAbsenceCountBySubject(int eleveId, out Dictionary<string, int> absenceData)
        {
            absenceData = new Dictionary<string, int>();
            int totalAbsences = 0;

            string query = @"
       SELECT m.name AS matiere_name, COUNT(a.id) AS absence_count
FROM matiere m
LEFT JOIN Absence a ON m.id = a.matiere_id AND a.eleve_id = @eleveId
GROUP BY m.name;
";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@eleveId", eleveId);
                connectionString();

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    // No absences found
                }
                else
                {
                    while (reader.Read())
                    {
                        string matiereName = reader["matiere_name"].ToString();
                        int absenceCount = Convert.ToInt32(reader["absence_count"]);
                        totalAbsences += absenceCount;
                        absenceData.Add(matiereName, absenceCount);
                    }
                }
                con.Close();
            }

            return totalAbsences;
        }
    }
}
