using CVDTestApp.Data.Models;

namespace CVDTestApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public string SalaryWithManagers { get; set; }
        public string SalaryWithoutManagers { get; set; }
        public string MaxSalaryDepartment { get; set; }
        public string ManagersSalary { get; set; }
    }
}
