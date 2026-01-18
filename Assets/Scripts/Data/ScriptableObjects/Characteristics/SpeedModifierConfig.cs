using Data.Dto;
using Data.ScriptableObjects.Configs;
using UnityEngine;

namespace Data.ScriptableObjects.Characteristics
{
    [CreateAssetMenu(fileName = "SpeedModifier", menuName = "ScriptableObjects/Characteristics/SpeedModifier")]
    public class SpeedModifierConfig : BaseEventConfig
    {
        public override IEvent GetConfig(int value)
        {
            return new SpeedModifierData(value);
        }
    }
}