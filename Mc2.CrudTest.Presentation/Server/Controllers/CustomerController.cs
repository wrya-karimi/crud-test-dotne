using Mc2.CrudTest.Domain.Abstracts.Application;
using Mc2.CrudTest.Presentation.Client.Pages;
using Mc2.CrudTest.Presentation.Shared;
using Microsoft.AspNetCore.Mvc;
using PhoneNumbers;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerServicee;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerServicee)
        {
            _logger = logger;
            _customerServicee = customerServicee;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerServicee.GetCustomerById(id);
                if (customer is null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Get => {@ex}", ex.Message);
                return Problem();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            try
            {
                var customers = await _customerServicee.GetAll();
                if (customers is null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer GetAllByCustomer => {@ex}", ex.Message);
                return Problem();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> InsertCustomer(CustomerDto customerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (ValidatePhoneNumber(customerDto.PhoneNumber) is null)
                {
                    return BadRequest("Phone Number is not valid");
                }
                await _customerServicee.InsertCustomer(customerDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Create => {@ex}", ex.Message);
                return Problem();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(CustomerDto customerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if(ValidatePhoneNumber(customerDto.PhoneNumber) is null)
                {
                    return BadRequest("Phone Number is not valid");
                }

                await _customerServicee.UpdateCustomer(customerDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Create => {@ex}", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerServicee.GetCustomerById(id);
                if (customer is null)
                {
                    return NotFound();
                }
                else
                {
                    await _customerServicee.DeleteCustomer(id);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Delete => {@ex}", ex.Message);
                return Problem();
            }
        }

        private string? ValidatePhoneNumber(string phoneNumber)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                PhoneNumber _number = phoneUtil.Parse(phoneNumber, "IR");
                var queryPhoneNumber = phoneUtil.IsValidNumberForRegion(_number, "IR");
                if (queryPhoneNumber)
                { 
                    return _number.ToString();
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
