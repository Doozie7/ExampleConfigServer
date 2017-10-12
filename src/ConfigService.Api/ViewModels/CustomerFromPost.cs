namespace ConfigService.Api.ViewModels
{
    /// <summary>
    /// The required information to create a new customer
    /// </summary>
    public class CustomerFromPost
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the customer
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is the customer enabled at firt creation time
        /// </summary>
        public bool Enabled { get; set; }
    }
}
