using Contracts;
using Entities.Context;
using Entities.Models;
using Entities.RequestFeatures;
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

        public async Task<PagedList<Employee>> GetEmployeesAsync(int companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(x => x.CompanyId == companyId && (x.Age >= employeeParameters.MinAge && x.Age <= employeeParameters.MaxAge), trackChanges)
                                 .OrderBy(x => x.Name)
                                 .ToListAsync();

            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }
    }
}
