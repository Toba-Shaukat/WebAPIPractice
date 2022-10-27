using Contracts;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {

        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            var companies = GetAll(trackChanges).ToList();
            return companies;
        }

        public Company GetCompany(int companyId, bool trackChanges)
        {
            return FindByCondition(c => c.Id == companyId, trackChanges).SingleOrDefault();

        }
        public void CreateCompany(Company company)
        {
            Add(company);
        }
        public void DeleteCompany(Company company)
        {
            Remove(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                         .OrderBy(x => x.Name)
                         .ToListAsync();
        }
        public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == companyId, trackChanges).SingleOrDefaultAsync();
        }
    }
}
