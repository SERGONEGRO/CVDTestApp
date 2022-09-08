using CVDTestApp.Data.Models;

namespace CVDTestApp.Data
{
    public class DBObjects
    {
        /// <summary>
        /// Add items into DB
        /// </summary>
        /// <param name="content"></param>
        public static void Initial (AppDbContent content)
        {
            if (!content.Departments.Any())
            {
                content.AddRange(
                     new Department { Name = "D1" },
                     new Department { Name = "D2" },
                     new Department { Name = "D3" }
                     );
            }

            if (!content.Employees.Any())
                content.AddRange(
                     new Employee { Department_id = 1, Chief_id = 5, Name = "John", Salary = 100 },
                     new Employee { Department_id = 1, Chief_id = 5, Name = "Misha", Salary = 600 },
                     new Employee { Department_id = 2, Chief_id = 6, Name = "Eugen", Salary = 300 },
                     new Employee { Department_id = 2, Chief_id = 6, Name = "Tolya", Salary = 400 },
                     new Employee { Department_id = 3, Chief_id = 7, Name = "Stepan", Salary = 500 },
                     new Employee { Department_id = 3, Chief_id = 7, Name = "Alex", Salary = 1000 },
                     new Employee { Department_id = 3, Name = "Ivan", Salary = 1100 }
                     );

            content.SaveChanges();
        }
    }
}
