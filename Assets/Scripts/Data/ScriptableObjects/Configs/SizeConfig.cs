using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "SizeConfig", menuName = "ScriptableObjects/Configs/Size")]
    public class SizeConfig : BaseMultiConfig
    {
        [SerializeField, Tooltip("Size view")]
        private float normalizeScale;

        public override List<IEvent> GetConfigs()
        {
            List<IEvent> configs = GetConfigsInternal();
            configs.Add(new SizeData(normalizeScale));
            return configs;
        }
    }
}