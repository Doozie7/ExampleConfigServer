using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConfigService.Model
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get a list of Types using a filter, order and take 1000 and skip 0 by default
        /// </summary>
        /// <param name="filter">The where clase in the query e.g. c =&gt; c.Enabled == true</param>
        /// <param name="order">The order by clause in the query e.g. c =&gt; c.Name</param>
        /// <param name="take">by default take 1000</param>
        /// <param name="skip">by default skip 0</param>
        /// <returns></returns>
        IList<T> GetListOf(Func<T, bool> filter = null, Func<T, object> order = null, int take = 1000, int skip = 0);

        /// <summary>
        /// Adds a new Type to the repository
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        T Add(T itemToAdd);


        /// <summary>
        /// Delete customers based on the provided filter
        /// </summary>
        /// <param name="filter">The where clase in the query e.g. c =&gt; c.Enabled == true</param>
        void Delete(Func<T, bool> filter = null);
    }
}