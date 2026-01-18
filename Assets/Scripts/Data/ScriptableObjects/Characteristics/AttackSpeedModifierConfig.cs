using Data.Dto;
using Data.ScriptableObjects.Configs;
using UnityEngine;

namespace Data.ScriptableObjects.Characteristics
{
    [CreateAssetMenu(fileName = "AttackSpeedModifier", menuName = "ScriptableObjects/Characteristics/AttackSpeedModifier")]
    public class AttackSpeedModifierConfig : BaseEventConfig
    {
        public override IEvent GetConfig(int value)
        {
            return new AttackSpeedModifierData(value);
        }
    }
}