// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.Sql
{
    public class CustomersRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        public CustomersRepository(IDbContext dbContext, ILogger<CustomersRepository> logger) : base(dbContext, logger)
        { }

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
            filter = filter ?? (c => c != null);
            order = order ?? (c => c.Id);

            return DbContext.Customers.Where(filter).OrderBy(order).Take(take).Skip(skip).ToList();
        }

        /// <summary>
        /// Add a new customer tor the Customers Repository
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        public Customer Add(Customer itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<Customer, bool> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}