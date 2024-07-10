using CustomerWebApi.Data;
using CustomerWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerDbContext _CustomerDbContext;

        public CustomerController(CustomerDbContext customerDbContext)
        {
            _CustomerDbContext = customerDbContext;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Customer>> GetAllCustomer()
        {
            return _CustomerDbContext.Customers;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _CustomerDbContext.Customers.FindAsync(customerId);
            return customer;
        }

        // POST api/<CustomerController>
        [HttpPost]
        [Authorize(Roles = "Administration")]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            await _CustomerDbContext.Customers.AddAsync(customer);
            await _CustomerDbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        [Authorize(Roles = "Administration, User")]
        public async Task<ActionResult> Update(Customer customer)
        {
            _CustomerDbContext.Customers.Update(customer);
            await _CustomerDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var customer = await _CustomerDbContext.Customers.FindAsync(customerId);
            _CustomerDbContext.Customers.Remove(customer);
            await _CustomerDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
