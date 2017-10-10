using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigService.Model
{
    public class SettingType
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
        public int? SequenceNumber { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
