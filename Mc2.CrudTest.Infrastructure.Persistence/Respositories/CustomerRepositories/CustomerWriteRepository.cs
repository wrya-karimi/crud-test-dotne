using Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.Persistence.Respositories.CustomerRepositories
{

    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerWriteRepository(ApplicationDbContext context) => _context = context;

        public async Task<Customer?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Customer>().FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task AddAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer));

                await _context.Set<Customer>().AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task RemoveAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer));

                _context.Set<Customer>().Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                if (customer is null)
                    throw new ArgumentNullException(nameof(customer));

                var customerOld = await FindByIdAsync(customer.Id);
                if (customerOld is not null)
                {
                    customerOld.Firstname = customer.Firstname;
                    customerOld.Lastname = customer.Lastname;
                    customerOld.DateOfBirth = customer.DateOfBirth;
                    customerOld.Email = customer.Email;
                    customerOld.PhoneNumber = customer.PhoneNumber;
                    customerOld.BankAccountNumber = customer.BankAccountNumber;

                    _context.Set<Customer>().Update(customerOld);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
