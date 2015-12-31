using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_Interception.Domain.Entities;

namespace Unity_Interception.Domain.Repositories
{

    public interface ICustomerRepository
    {
        Customer Load(long id);

        IEnumerable<Customer> LoadAll();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly Dictionary<long, Customer> _lookup;

        public CustomerRepository(params Customer[] customers)
        {
            _lookup = customers.ToDictionary(o => o.Id);
        }


        public Customer Load(long id)
        {
            Customer customer;
            _lookup.TryGetValue(id, out customer);
            return customer;
        }

        public IEnumerable<Customer> LoadAll()
        {
            return _lookup.Values;
        }
    }
}
