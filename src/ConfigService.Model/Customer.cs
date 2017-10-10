using System;

namespace ConfigService.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
