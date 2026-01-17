using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "SizeConfig", menuName = "ScriptableObjects/Configs/Size")]
    public class SizeConfig : BaseEventConfigSO
    {
        [SerializeField, Tooltip("Health bonus applied to the unit")]
        private int changeHealth;
        [SerializeField, Tooltip("Size view")]
        private float normalizeScale;

        public override IEvent GetConfig()
        {
            return new SizeData(changeHealth, normalizeScale);
        }
    }
}