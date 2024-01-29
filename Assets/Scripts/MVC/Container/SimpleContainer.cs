using System;
using System.Collections.Generic;

namespace MVC
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public T Resolve<T>()
        {
            if (_instances.TryGetValue(typeof(T), out object instance))
            {
                return (T)instance;
            }
            return default(T);
        }

        public void Register<TImplementation>(object implementation)
        {
            if (!_instances.ContainsKey(typeof(TImplementation)))
            {
                _instances[typeof(TImplementation)] = implementation;
            }
        }
    }
}