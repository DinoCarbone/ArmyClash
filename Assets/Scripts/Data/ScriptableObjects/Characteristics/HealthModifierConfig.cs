using Data.Dto;
using Data.ScriptableObjects.Configs;
using UnityEngine;

namespace Data.ScriptableObjects.Characteristics
{
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "ScriptableObjects/Characteristics/HealthModifier")]
    public class HealthModifierConfig : BaseEventConfig
    {
        public override IEvent GetConfig(int value)
        {
            return new HealthModifierData(value);
        }
    }
}