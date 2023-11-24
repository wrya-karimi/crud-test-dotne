using Mc2.CrudTest.Presentation.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Mc2.CrudTest.Presentation.Client.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public CustomerService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();

        public async Task<CustomerDto> GetCustomer(int id)
        {


            var result = await _http.GetFromJsonAsync<CustomerDto>($"api/customer/{id}");
            if (result is not null)
            {
                return result;
            }
            throw new Exception("Customer does not exist");
        }

        public async Task GetCustomers()
        {
            var result = await _http.GetFromJsonAsync<List<CustomerDto>>("api/customer");
            if (result is not null)
            {
                Customers = result;
            }
        }

        public async Task CreateCustomer(CustomerDto customerDto)
        {
            await _http.PostAsJsonAsync("api/customer", customerDto);
            await GetCustomers();
            _navigationManager.NavigateTo("customers");
        }

        public async Task UpdateCustomer(CustomerDto customerDto)
        {
            await _http.PutAsJsonAsync("api/customer", customerDto);
            await GetCustomers();
            _navigationManager.NavigateTo("customers");

        }

        public async Task DeleteCustomer(int id)
        {
            await _http.DeleteAsync($"api/customer/{id}");
            await GetCustomers();
            _navigationManager.NavigateTo("customers");

        }
    }
}
