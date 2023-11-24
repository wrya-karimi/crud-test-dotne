using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories
{
    public interface ICustomerWriteRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(Customer customer);
    }
}
