using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(int companyId, bool trackChanges);
        Employee GetEmployee(int companyId, int id, bool trackChanges);
        void CreateEmployeeForCompany(int companyId, Employee employee);
    }
}
