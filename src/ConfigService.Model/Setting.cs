using Newtonsoft.Json;
using System;

namespace ConfigService.Model
{
    public class Setting
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }
        public int SettingTypeId { get; set; }

        [JsonIgnore]
        public SettingType SettingType { get; set; }
        public string SettingValue { get; set; }
    }
}
