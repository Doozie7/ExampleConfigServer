using ConfigService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConfigService.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IRepository<Setting> _repository;
        private readonly ILogger<SettingsController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SettingsController(IRepository<Setting> repository, ILogger<SettingsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetListOf(c => c != null == true, c => c.CustomerId));
        }
    }
}
