using System;
using UnityEngine;

namespace ReactiveProperty
{
    [DefaultExecutionOrder(150)]
    public class MyReactiveProperty<T>
    {
        public MyReactiveProperty(T startValue)
        {
            _value = startValue;
        }
        public event Action<T> OnChanged;
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }
    }
}