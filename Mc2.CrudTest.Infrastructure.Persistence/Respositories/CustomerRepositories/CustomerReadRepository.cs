using Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Persistence.Respositories.CustomerRepositories
{

    public class CustomerReadRepository : ICustomerReadRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerReadRepository(ApplicationDbContext context) => _context = context;

        public async Task<Customer?> FindByIdAsync(int id)
        {
            return await _context.Set<Customer>().FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(params string[] IncludeProperties)
        {
            var query = Include(IncludeProperties);
            if (query != null)
            {
                return await query.ToListAsync();
            }
            return await _context.Set<Customer>().ToListAsync();
        }

        public bool IsEmailUnique(string email)
        {
            if(email is not null){
                return  !_context.Customers.Any(x => x.Email.Equals(email));
            }
            return false;

        }

        private IQueryable<Customer>? Include(params string[] IncludeProperties)
        {
            if (IncludeProperties != null && IncludeProperties.Length > 0)
            {
                IQueryable<Customer>? query = null;
                foreach (var prop in IncludeProperties)
                {
                    query = _context.Set<Customer>().Include(prop);
                }
                return query;
            }
            return null;
        }
    }
}
