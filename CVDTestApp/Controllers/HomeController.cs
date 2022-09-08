using Microsoft.AspNetCore.Mvc;
using CVDTestApp.Data.Interfaces;
using CVDTestApp.ViewModels;
using Microsoft.Data.SqlClient;

namespace CVDTestApp.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    public class HomeController : Controller
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CVSTestDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        string sqlExpressionTask1_all = "SELECT d.Name, SUM(Salary) AS SUM FROM Employees e " +
                                        "JOIN Departments d ON e.Department_id = d.Id GROUP BY d.Name";

        string sqlExpressionTask1_noManagers = "SELECT d.Name, SUM(Salary) AS SUM FROM Employees e " +
                                               "JOIN Departments d ON e.Department_id = d.Id " +
                                               "WHERE e.Id NOT IN " +
                                               "(SELECT DISTINCT e1.Id FROM Employees e1 JOIN Employees e2 " +
                                               "ON e1.Id = e2.Chief_id) GROUP BY d.Name";

        string sqlExpressionTask2 = "SELECT TOP 1 d.Name, e.Salary FROM Employees e JOIN Departments d " +
                                    "ON e.Department_id = d.Id where e.Department_id > 0 " +
                                    "ORDER BY e.Salary DESC";

        string sqlExpressionTask3 = "SELECT DISTINCT e1.Name, e1.Salary FROM Employees e1 JOIN Employees e2 " +
                                    "ON e1.Id = e2.Chief_id ORDER BY e1.Salary DESC";

        public string salaryWithManagers;
        public string salaryWithoutManagers;


        private IAllEmployees _employeeRep;
        public HomeController(IAllEmployees employeeRep)
        {
            _employeeRep = employeeRep;
        }

        public ViewResult Index()
        {
            DoTask1();
            var homeEmployee = new HomeViewModel
            {
                Employees = _employeeRep.Employees,
                SalaryWithoutManagers = salaryWithoutManagers,
                SalaryWithManagers = salaryWithManagers,
                MaxSalaryDepartment = DoTask2(),
                ManagersSalary = DoTask3()
            };
            return View(homeEmployee);
        }

        /// <summary>
        /// do task 1
        /// </summary>
        public void DoTask1()
        {
            salaryWithoutManagers = "Salary without managers - ";
            salaryWithManagers = "Salary with managers - ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                SqlCommand command1 = new SqlCommand(sqlExpressionTask1_noManagers, connection);
                SqlCommand command2 = new SqlCommand(sqlExpressionTask1_all, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                SqlDataReader reader2 = command2.ExecuteReader();

                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        salaryWithoutManagers += "Department: " + reader1.GetValue(0).ToString() + ",";
                        salaryWithoutManagers += "Salary: " + reader1.GetValue(1).ToString() + "; ";
                    }
                }
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        salaryWithManagers += "Department: " + reader2.GetValue(0).ToString() + ",";
                        salaryWithManagers += "Salary: " + reader2.GetValue(1).ToString() + "; ";
                    }
                }
                reader1.Close();
                reader2.Close();
            }
        }

        /// <summary>
        /// do task 2
        /// </summary>
        /// <returns></returns>
        public string DoTask2()
        {
            string result = "Department Name: ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpressionTask2, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result += reader.GetValue(0).ToString() + ", Max Salary: ";
                        result += reader.GetValue(1).ToString();
                    }
                }
                reader.Close();
            }
            return (result);
        }

        /// <summary>
        /// do task 3
        /// </summary>
        /// <returns></returns>
        public string DoTask3()
        {
            string result = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpressionTask3, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result += "Manager name: " + reader.GetValue(0).ToString();
                        result += ", Salary: " + reader.GetValue(1).ToString() + "; ";
                    }
                }
                reader.Close();
            }
            return (result);
        }
    }
}
