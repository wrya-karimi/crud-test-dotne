using Mc2.CrudTest.Presentation.Shared;

namespace Mc2.CrudTest.Domain.Abstracts.Application
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAll();
        Task<CustomerDto?> GetCustomerById(int id);
        Task InsertCustomer(CustomerDto customer);
        Task DeleteCustomer(int id);
        Task UpdateCustomer(CustomerDto customer);
    }
}
