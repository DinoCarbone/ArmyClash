using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "DamageModifier", menuName = "ScriptableObjects/Characteristics/DamageModifier")]
    public class DamageModifierConfig : BaseEventConfig
    {
        public override IEvent GetConfig(int value)
        {
            return new DamageModifierData(value);
        }
    }
}