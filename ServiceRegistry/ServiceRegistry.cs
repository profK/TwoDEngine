using System;
using System.Collections.Generic;

namespace ServiceRegistry
{
    public static class Registry
    {
        static Dictionary<Type,List<IService>> _registry = new Dictionary<Type, List<IService>>();
        
        public static void RegisterService(IService service)
        {
            if (!service.IsSupported())
            {
                Log("Service " + service.GetType().FullName + " not supported.");
            }
            foreach (Type iface in service.GetType().GetInterfaces())
            {
                if (!_registry.ContainsKey(iface))
                {
                    _registry.Add(iface,new List<IService>());
                }
                _registry[iface].Add(service);
            }
        }

        public static void Log(string message)
        {
            Console.WriteLine("Registry: " + message);
        }

        public static T GetService<T>()
        {
            if (_registry.ContainsKey(typeof(T)))
            {
                return (T)_registry[typeof(T)][0];
            }
            else
            {
                return default(T);
            }
        }
        
        public static T[] GetServices<T>()
        {
            if (_registry.ContainsKey(typeof(T)))
            {
                return Array.ConvertAll<IService, T>(_registry[typeof(T)].ToArray(),
                    new Converter<IService, T>(pre => { return (T) pre; }));
            }
            else
            {
                return new T[0];
            }
        }

        public static void Log(object message)
        {
            throw new NotImplementedException();
        }
    }
}