using Mc2.CrudTest.Domain.Abstracts.Application;
using Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Presentation.Shared;

namespace Mc2.CrudTest.Domain.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public CustomerService(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<CustomerDto?> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerReadRepository.FindByIdAsync(id);
                return new CustomerDto
                {
                    Id = customer.Id,
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                    DateOfBirth = customer.DateOfBirth,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber.ToString(),
                    BankAccountNumber = customer.BankAccountNumber
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            try
            {
                var customers = await _customerReadRepository.GetAllAsync();
                return customers.Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber.ToString(),
                    BankAccountNumber = x.BankAccountNumber
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertCustomer(CustomerDto customerDto)
        {
            try
            {
                if (!_customerReadRepository.IsEmailUnique(customerDto.Email))
                {
                    throw new Exception("Email is not unique");
                }

                var customer = new Customer
                {
                    Id = customerDto.Id,
                    Firstname = customerDto.Firstname,
                    Lastname = customerDto.Lastname,
                    DateOfBirth = customerDto.DateOfBirth,
                    Email = customerDto.Email,
                    PhoneNumber = Convert.ToUInt64(customerDto.PhoneNumber),
                    BankAccountNumber = customerDto.BankAccountNumber
                };
                await _customerWriteRepository.AddAsync(customer);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    Id = customerDto.Id,
                    Firstname = customerDto.Firstname,
                    Lastname = customerDto.Lastname,
                    DateOfBirth = customerDto.DateOfBirth,
                    Email = customerDto.Email,
                    PhoneNumber = Convert.ToUInt64(customerDto.PhoneNumber),
                    BankAccountNumber = customerDto.BankAccountNumber
                };
                await _customerWriteRepository.UpdateAsync(customer);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerReadRepository.FindByIdAsync(id);
                if (customer != null)
                {
                    await _customerWriteRepository.RemoveAsync(customer);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
