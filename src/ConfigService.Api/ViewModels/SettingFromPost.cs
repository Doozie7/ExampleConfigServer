using System;

namespace ConfigService.Api.ViewModels
{
    /// <summary>
    /// The required information to create a new Setting
    /// </summary>
    public class SettingFromPost
    {
        /// <summary>
        /// The customerid
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The setting type id
        /// </summary>
        public int SettingTypeId { get; set; }

        /// <summary>
        /// The setting value
        /// </summary>
        public string SettingValue { get; set; }
    }
}
