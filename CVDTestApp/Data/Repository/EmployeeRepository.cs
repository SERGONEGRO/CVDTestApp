using CVDTestApp.Data.Interfaces;
using CVDTestApp.Data.Models;

namespace CVDTestApp.Data.Repository
{
    /// <summary>
    /// For work with DB
    /// </summary>
    public class EmployeeRepository : IAllEmployees
    {
        private readonly AppDbContent appDbContent;
        public EmployeeRepository(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<Employee> Employees => appDbContent.Employees;
    }
}
