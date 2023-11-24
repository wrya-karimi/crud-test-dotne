using Mc2.CrudTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories
{
    public interface ICustomerReadRepository
    {
        bool IsEmailUnique(string email);

        Task<Customer?> FindByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync(params string[] IncludeProperties);
    }
}
