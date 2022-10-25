using Contracts;
using Entities.Context;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}
