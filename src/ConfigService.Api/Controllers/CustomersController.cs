using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Api.ViewModels;
using ConfigService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ConfigService.Api.Controllers
{
    /// <summary>
    /// The customerFromPost controller provides REST methods onto the customers domain object.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;
        private readonly ILogger<CustomersController> _logger;

        /// <summary>
        /// The constructor requireds a Customer repository and the logging framework on construction
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public CustomersController(IRepository<Customer> repository, ILogger<CustomersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Get a list customers
        /// </summary>
        /// <remarks>
        /// Returns a list of enabled customers.
        /// </remarks>
        /// <returns></returns>
        [HttpGet(Name = "GetCustomers")]
        [SwaggerResponse(200, typeof(List<Customer>), "List of enabled customers")]
        public IActionResult Get()
        {
            return Ok(_repository.GetListOf(c => c.Enabled == true, c => c.Name));
        }

        /// <summary>
        /// Get a customer
        /// </summary>
        /// <remarks>
        /// Returns a customer.
        /// </remarks>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetCustomer")]
        [SwaggerResponse(200, typeof(Customer), "Get a customer by Id")]
        [SwaggerResponse(404, null, "A customer with the provided Id was not found")]
        public IActionResult GetById(Guid id)
        {
            var result = _repository.GetListOf(c => c.Id == id, c => c.Name).SingleOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get a customers settings
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerid}/settings", Name = "GetCustomersSettings")]
        [SwaggerResponse(200, typeof(List<Setting>), "Get a customers settings")]
        [SwaggerResponse(404, null, "A customer with the provided Id was not found")]
        public IActionResult GetCustomerSettings(Guid customerId)
        {
            var customer = _repository.GetListOf(c => c.Id == customerId, c => c.Name).SingleOrDefault();
            if (customer == null)
            {
                _logger.LogError($"No customer found with the id of {customerId}");
                return NotFound();
            }

            return RedirectToRoute("GetSettingsForCustomer", customerId);
        }

        /// <summary>
        /// Add a customer
        /// </summary>
        /// <param name="customerFromPost"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(201, typeof(Customer), "Add a customer")]
        [SwaggerResponse(400, typeof(string), "Bad request, data validation error or possible duplicate")]
        public IActionResult Post([FromBody] CustomerFromPost customerFromPost)
        {
            if (customerFromPost == null)
            {
                _logger.LogError("The customer from a Post was null.");
                return BadRequest();
            }

            var customer = new Customer()
            {
                CreatedDate = DateTime.UtcNow,
                Description = customerFromPost.Description,
                Enabled = customerFromPost.Enabled,
                Id = Guid.NewGuid(),
                Name = customerFromPost.Name,
            };

            var count = _repository.GetListOf(c => c.Name == customer.Name).Count;
            if (count == 0)
            {
                _repository.Add(customer);
                return CreatedAtRoute("GetCustomer", new {id = customer.Id}, customer);
            }
            else
            {
                return BadRequest("Duplicate found.");
            }
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <remarks>
        /// Returns no content response
        /// </remarks>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [SwaggerResponse(204, null, "Delete a customer by Id")]
        public IActionResult DeleteById(Guid id)
        {
            var customer = _repository.GetListOf(c => c.Id == id).SingleOrDefault();
            if (customer == null)
            {
                _logger.LogError("Could not delete customer with the id {id} as it does not exist in the repository.");
                return NotFound();
            }

            _repository.Delete(c => c.Id == id);
            return new NoContentResult();
        }
    }
}
