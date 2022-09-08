namespace CVDTestApp.Data.Models
{
    /// <summary>
    /// Employee model
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public int Department_id { get; set; }
        public int? Chief_id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }
}
