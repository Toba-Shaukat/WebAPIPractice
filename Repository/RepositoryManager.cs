using Contracts;
using Entities.Context;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        public ICompanyRepository CompanyRepository => new CompanyRepository(_context);

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
