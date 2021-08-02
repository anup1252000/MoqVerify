using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EmployeeAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private static List<Employee> employees;
        static EmployeeService()
        {
            employees = new List<Employee>
            {
                new Employee
                {
                    Id=1,
                    Name="xyz"
                },
                 new Employee
                {
                    Id=2,
                    Name="abc"
                },
            };
        }

        public Employee GetEmployee(int id)
        {
            Thread.Sleep(1000);
            return employees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }
    }
}
