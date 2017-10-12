using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigService.Model;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;

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
        [ProducesResponseType(typeof(string), 200)]
        [Produces("application/json")]
        public IActionResult Get()
        {
            _logger.LogDebug("Writing to the log for Echo controller.");

            var routes = GetRouteDetails(RouteData.Routers.OfType<RouteCollection>().FirstOrDefault());

            var info = new
            {
                ServerTimeUTC = $"{DateTime.UtcNow}",
                ServerName = Environment.MachineName,
                ExecutingAssembly = Assembly.GetExecutingAssembly().FullName,
                UserName = User.Identity.Name ?? "<NotSet>",
                CustomerRepository = _repository.GetType().AssemblyQualifiedName,
                Routes = routes
            };

            return Ok(info);
        }

        private static IList<string> GetRouteDetails(RouteCollection routeCollection)
        {
            // There be dragons here (reflection and numberd array's
            var results = new List<string>();
            var type = routeCollection.GetType();

            if (type.Name == "RouteCollection")
            {
                var routes = (IList)routeCollection.GetType()
                    .GetField("_routes", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(routeCollection);

                var ar = routes[0];

                var actions = (ActionDescriptorCollectionProvider)ar.GetType()
                    .GetField("_actionDescriptorCollectionProvider", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(ar);

                foreach (var action in actions.ActionDescriptors.Items)
                {
                    results.Add($"{action.DisplayName}, Uri: {action.AttributeRouteInfo.Template}");
                }
            }

            return results;
        }
    }
}
