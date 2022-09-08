using CVDTestApp.Data.Models;

namespace CVDTestApp.Data.Interfaces
{
    /// <summary>
    /// Get all about employees
    /// </summary>
    public interface IAllEmployees
    {
        IEnumerable<Employee> Employees { get; }
    }
}
