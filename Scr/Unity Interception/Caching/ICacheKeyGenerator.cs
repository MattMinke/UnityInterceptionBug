using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Unity_Interception.Caching
{
    public interface ICacheKeyGenerator
    {
        string CreateCacheKey(object o);
    }

    public class SimpleCacheKeyGenerator : ICacheKeyGenerator
    {
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public string CreateCacheKey(object o)
        {
            return serializer.Serialize(o);
        }
    }
}
