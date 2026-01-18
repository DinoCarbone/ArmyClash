using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ColorConfig", menuName = "ScriptableObjects/Configs/Color")]
    public class MaterialConfig : BaseMultiConfig
    {
        [SerializeField, Tooltip("Material to apply to the model")]
        private Material material;
        public override List<IEvent> GetConfigs()
        {
            List<IEvent> configs = GetConfigsInternal();
            configs.Add(new MaterialData(material));
            return configs;
        }
    }
}