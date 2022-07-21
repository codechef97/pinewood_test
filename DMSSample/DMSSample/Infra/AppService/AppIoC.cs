using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.Business.Infra.AppService
{
    public interface IAppIoC
    {
        AppIoC Register<T>() where T : class;

        T Resolve<T>();
    }
    class AppIoC : IAppIoC
    {

        private static IDictionary<object, object> __obj;

        public AppIoC()
        {
            if (__obj == null)
            {
                __obj = new Dictionary<object, object>();
            }
        }

        public IAppIoC Register<T>() where T : class
        {
            if (!__obj.ContainsKey(typeof(T)))
            {
                var instance = Activator.CreateInstance<T>();
                __obj.Add(typeof(T), instance);
            }

            return this;
        }

        public T Resolve<T>()
        {
            try
            {
                return (T)__obj[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Service not registered");
            }
        }
    }
}
