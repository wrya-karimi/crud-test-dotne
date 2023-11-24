using Mc2.CrudTest.Presentation.Shared;

namespace Mc2.CrudTest.Presentation.Client.Services.CustomerService
{
    public interface ICustomerService
    {
        List<CustomerDto> Customers { get; set; }

        Task GetCustomers();
        Task<CustomerDto> GetCustomer(int id);
        Task CreateCustomer(CustomerDto customer);
        Task UpdateCustomer(CustomerDto customer);
        Task DeleteCustomer(int id);
    }
}
