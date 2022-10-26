using Contracts;
using Entities.Context;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {

        }
        public Employee GetEmployee(int companyId, int id, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId == companyId && e.Id == id, trackChanges).SingleOrDefault();
        }
        public void CreateEmployeeForCompany(int companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Remove(employee);
        }
        public IEnumerable<Employee> GetEmployees(int companyId, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId == companyId, trackChanges).OrderBy(e => e.Name).ToList();
        }
    }
}
