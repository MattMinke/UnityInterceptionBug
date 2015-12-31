using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_Interception.Caching;

namespace Unity_Interception.Behaviors
{
    public class CachingBehavior : IInterceptionBehavior
    {
        private readonly ICacheKeyGenerator _cacheKeyGenerator;
        private readonly ICacheProvider _cacheProvider;
        private readonly ICacheRegionNameGenerator _cacheRegionNameGenerator;

        public CachingBehavior(
            ICacheProvider cacheProvider,
            ICacheKeyGenerator cacheKeyGenerator,
            ICacheRegionNameGenerator cacheRegionNameGenerator)
        {
            _cacheProvider = cacheProvider;
            _cacheKeyGenerator = cacheKeyGenerator;
            _cacheRegionNameGenerator = cacheRegionNameGenerator;
        }


        public bool WillExecute { get { return true; } }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (IsMethodCachable(input))
            {
                string regionName = _cacheRegionNameGenerator.CreateRegionName(
                    string.Format("{0}.{1}",input.Target.GetType().FullName, input.MethodBase.Name));

                string cacheKey = _cacheKeyGenerator.CreateCacheKey(input.Inputs);
                
                ICacheRegion cacheRegion = _cacheProvider.GetRegion(regionName);

                return cacheRegion.GetOrAdd(cacheKey,
                    () =>  getNext.Invoke().Invoke(input, getNext)  
                );
            }

            return getNext()
                .Invoke(input, getNext);
        }

        private bool IsMethodCachable(IMethodInvocation input)
        {
            // In our actual code we have logic that figures out if this particular method should be cached.
            return true;
        }
    }
}
