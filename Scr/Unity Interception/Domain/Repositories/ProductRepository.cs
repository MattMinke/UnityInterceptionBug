using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_Interception.Domain.Entities;

namespace Unity_Interception.Domain.Repositories
{
    public interface IProductRepository
    {
        Product LoadById(long id);

        IEnumerable<Product> LoadPage(int page, int pagesize);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<long, Product> _lookup;

        public ProductRepository(params Product[] products)
        {
            _lookup = products.ToDictionary(o=> o.Id);
        }


        public Product LoadById(long id)
        {
            Product product;
            _lookup.TryGetValue(id, out product);
            return product;
        }

        public IEnumerable<Product> LoadPage(int page, int pagesize)
        {
            return _lookup.Values.Skip(page * pagesize)
                .Take(pagesize);
        }
    }
}
