using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_Interception.Caching
{
    public interface ICacheRegion
    {
        TValue GetOrAdd<TValue>(string key, Func<TValue> valueFactory);
    }

}
