using System;
using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    public abstract class BaseMultiConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Characteristics")]
        protected List<ValueConfig> valueConfigs;
        public abstract List<IEvent> GetConfigs();
        protected List<IEvent> GetConfigsInternal()
        {
            List<IEvent> configs = new List<IEvent>();
            foreach (ValueConfig config in valueConfigs)
            {
                configs.Add(config.Config.GetConfig(config.Value));
            }
            return configs;
        }
    }

    [Serializable]
    public class ValueConfig
    {
        [SerializeField] private  int value;
        [SerializeField] private BaseEventConfig config;

        public int Value => value;
        public BaseEventConfig Config => config;
    }
}