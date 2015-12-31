using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity_Interception.Caching
{
    public interface ICacheRegionNameGenerator
    {
        string CreateRegionName(string name);
    }

    public class SimpleCacheRegionNameGenerator : ICacheRegionNameGenerator
    {
        public string CreateRegionName(string name)
        {
            return name;
        }
    }
}
