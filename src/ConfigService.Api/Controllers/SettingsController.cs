using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Api.ViewModels;
using ConfigService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            return Ok(_repository.GetListOf(c => c != null, c => c.CustomerId));
        }

        /// <summary>
        /// Get a setting by Id
        /// </summary>
        /// <remarks>
        /// Returns a setting.
        /// </remarks>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetSetting")]
        [SwaggerResponse(200, typeof(Setting), "Get a setting by Id")]
        [SwaggerResponse(404, null, "A setting with the provided Id was not found")]
        public IActionResult GetById(int id)
        {
            var result = _repository.GetListOf(c => c.Id == id, c => c.Id).SingleOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get the settings for a customer
        /// </summary>
        /// <remarks>
        /// Returns a setting.
        /// </remarks>
        /// <returns></returns>
        /// <param name="customerId"></param>
        [HttpGet("{customerid}", Name = "GetSettingsForCustomer")]
        [SwaggerResponse(200, typeof(List<Setting>), "Get settings by customer")]
        [SwaggerResponse(404, null, "No settings found for customer")]
        public IActionResult GetSettingsForCustomer(Guid customerId)
        {
            var settings = _repository.GetListOf(c => c.CustomerId == customerId, c => c.Id);
            if (settings == null)
            {
                _logger.LogError($"There are not settings for the customer with id of {customerId}");
                return NotFound();
            }

            return Ok(settings);
        }

        /// <summary>
        /// Create a new setting
        /// </summary>
        /// <param name="settingFromPost"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(201, typeof(Setting), "Add a setting")]
        public IActionResult Post([FromBody] SettingFromPost settingFromPost)
        {
            if (settingFromPost == null)
            {
                return BadRequest();
            }

            var maxSettinId = _repository.GetListOf().OrderByDescending(item => item.Id).First().Id;

            var setting = new Setting()
            {
                Id = maxSettinId + 1,
                CustomerId = settingFromPost.CustomerId,
                SettingTypeId = settingFromPost.SettingTypeId,
                SettingValue = settingFromPost.SettingValue,
            };

            _repository.Add(setting);

            return CreatedAtRoute("GetSetting", new { id = setting.Id }, setting);
        }

        /// <summary>
        /// Delete a setting by Id
        /// </summary>
        /// <remarks>
        /// Returns no content response
        /// </remarks>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpDelete("{id}", Name = "DeleteSetting")]
        [SwaggerResponse(204, null, "Delete a setting by Id")]
        public IActionResult DeleteById(int id)
        {
            var customer = _repository.GetListOf(c => c.Id == id).SingleOrDefault();
            if (customer == null)
            {
                return NotFound();
            }

            _repository.Delete(c => c.Id == id);
            return new NoContentResult();
        }
    }
}
