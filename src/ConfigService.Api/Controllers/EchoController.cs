using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using ConfigService.Model;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ConfigService.Api.Controllers
{
    /// <summary>
    /// The echo controller provides a basic api call allowing consumers to get some additional information about the state of the controller
    /// </summary>
    [Route("api/[controller]")]
    public class EchoController : Controller
    {
        private readonly IRepository<Customer> _repository;
        private readonly ILogger<CustomersController> _logger;

        /// <summary>
        /// The constructor requires the logging framework
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public EchoController(IRepository<Customer> repository, ILogger<CustomersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Get basic information about the api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [Produces("application/json")]
        public IActionResult Get()
        {
            _logger.LogDebug("Writing to the log for Echo controller.");

            var info = new {
                ServerTimeUTC = $"{DateTime.UtcNow}",
                ServerName = System.Environment.MachineName,
                ExecutingAssembly = Assembly.GetExecutingAssembly().FullName,
                UserName = User.Identity.Name ?? "<NotSet>",
                CustomerRepository = _repository.GetType().AssemblyQualifiedName
            };            

            return Ok(info);
        }
    }
}
