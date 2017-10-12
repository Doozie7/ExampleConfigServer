using ConfigService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConfigService.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class SettingTypesController : Controller
    {
        private readonly IRepository<SettingType> _repository;
        private readonly ILogger<SettingTypesController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SettingTypesController(IRepository<SettingType> repository, ILogger<SettingTypesController> logger)
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
            return Ok(_repository.GetListOf(c => c.Enabled, c => c.Id));
        }
    }
}
