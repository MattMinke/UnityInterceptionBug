using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_Interception.Caching
{ 
    
    [Serializable]
    public class MethodCacheValue
    {
        public object ReturnValue { get; set; }
        
        public List<OutParameter> OutParameters { get; set; }
    }

    [Serializable]
    public class OutParameter
    {
        public int Position { get; set; }
        
        public object Value { get; set; }
    }
}
