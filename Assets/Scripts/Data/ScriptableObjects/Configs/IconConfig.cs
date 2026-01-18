using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "IconConfig", menuName = "ScriptableObjects/Configs/Icon")]
    public class IconConfig : BaseMultiConfig
    {
        [SerializeField] private Sprite iconSprite;
        public override List<IEvent> GetConfigs()
        {
            List<IEvent> configs = GetConfigsInternal();
            configs.Add(new IconChangeData(iconSprite));
            return configs;
        }
    }
}