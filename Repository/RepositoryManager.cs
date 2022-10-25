using Contracts;
using Entities.Context;

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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
