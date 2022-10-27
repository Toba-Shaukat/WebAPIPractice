using Contracts;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {

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
        public async Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges)
        {
            return await FindByCondition(x => x.CompanyId == companyId && x.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges)
        {
            return await FindByCondition(x => x.CompanyId == companyId, trackChanges)
                         .OrderBy(x => x.Name)
                         .ToListAsync();
        }
    }
}
