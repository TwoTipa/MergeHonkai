using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ServiceLocator
{
    public class ServiceLocator
    {
        public static ServiceLocator Current { get; private set; }

        private readonly Dictionary<string, IService> _services = new();

        public ServiceLocator()
        {
            
        }
        
        public void Initialization()
        {
            Current = this;
        }

        public void Register<T>(T newService) where T : IService
        {
            string serviceName = typeof(T).Name;
            if (_services.ContainsKey(serviceName))
            {
                Debug.LogError($"Сервис {serviceName} пытается создаться второй раз");
                return;
            }
            Debug.Log(serviceName + "test");
            _services.Add(serviceName, newService);
        }
        
        public T Get<T>() where T : IService
        {
            string serviceName = typeof(T).Name;
            if (!_services.ContainsKey(serviceName))
            {
                throw new InvalidOperationException();
            }

            return (T)_services[serviceName];
        }
        
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
    }
}