using System;
using UniRx;
using UnityEngine;

namespace GameResources
{
    public abstract class GameResource
    {
        public string Name { get; private set; }
        public ReactiveProperty<int> Count = new();

        private const int ResourceLimit = Int32.MaxValue;

        protected GameResource(string name)
        {
            Name = name;
            Count.Value = PlayerPrefs.GetInt(Name);
        }

        public virtual bool TrySpend(int value)
        {
            var ret = Count.Value >= value;
            if(ret) Remove(value);
            return ret;
        }

        public virtual void Remove(int value)
        {
            Count.Value -= value;
            if (Count.Value < 0)
            {
                Count.Value = 0;
            }
            Save();
        }

        public virtual void Add(int value)
        {
            if ((long)Count.Value + value > ResourceLimit) Count.Value = ResourceLimit - value;
            Count.Value += value;
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt(Name, Count.Value);
        }
    }
}