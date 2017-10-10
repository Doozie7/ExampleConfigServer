// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.InMemory
{
    public class CustomersRepository : IRepository<Customer>
    {
        private readonly ILogger<CustomersRepository> _logger;
        private static IList<Customer> _customers;

        public CustomersRepository(ILogger<CustomersRepository> logger)
        {
            _logger = logger;

            if (_customers == null)
            {
                _customers = GetInitialCustomersList();
            }
        }

        /// <summary>
        /// Get a list of Customer using a filter, order and by default take 1000 and skip 0
        /// </summary>
        /// <param name="filter">The where clase in the query e.g. c =&gt; c.Enabled == true</param>
        /// <param name="order">The order by clause in the query e.g. c =&gt; c.Name</param>
        /// <param name="take">by default take 1000</param>
        /// <param name="skip">by default skip 0</param>
        /// <returns></returns>
        public IList<Customer> GetListOf(Func<Customer, bool> filter = null, Func<Customer, object> order = null, int take = 1000, int skip = 0)
        {
            _logger.LogWarning($"Using the InMemory {typeof(CustomersRepository).AssemblyQualifiedName}!");

            if (filter == null)
            {
                filter = c => c != null;
            }

            if (order == null)
            {
                order = c => c.Id;
            }

            return _customers.Where(filter).OrderBy(order).Take(take).Skip(skip).ToList();
        }

        /// <inheritdoc />
        public Customer Add(Customer itemToAdd)
        {
            _customers.Add(itemToAdd);
            return itemToAdd;
        }


        /// <inheritdoc />
        public void Delete(Func<Customer, bool> filter = null)
        {
            var toDelete = new List<Customer>();

            filter = filter ?? (c => c != null);
            foreach (var customer in _customers.Where(filter))
            {
                toDelete.Add(customer);
            }

            foreach (var customerToDelete in toDelete)
            {
                _customers.Remove(customerToDelete);
            }
        }

        private static IList<Customer> GetInitialCustomersList()
        {
            var list = new List<Customer>
            {
                new Customer
                {
                    Id = new Guid("65e51c54-21c5-41e8-8e22-21500379b275"),
                    Name = "Globex Corporation",
                    Description =
                        "Nothing says, “nonspecific international company serving the needs of consumers through service and synergy” like Globex.",
                    Enabled = true,
                    CreatedDate = DateTime.Now,
                },
                new Customer
                {
                    Id = new Guid("450c0a80-cbe5-4dbb-bd57-2d069da88959"),
                    Name = "Vehement Capital Partners",
                    Description =
                        "Vehement says intense, passionate, insistent; it’s a word for describing an argument or an artistic platform.",
                    Enabled = true,
                    CreatedDate = DateTime.Now,
                },
            };

            return list;
        }
    }
}
