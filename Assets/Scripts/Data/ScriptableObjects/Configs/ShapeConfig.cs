using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ShapeConfig", menuName = "ScriptableObjects/Configs/Shape")]
    public class ShapeConfig : BaseEventConfigSO
    {
        [SerializeField, Tooltip("Visual representation model prefab (3D mesh)")]
        private GameObject prefab;
        
        [SerializeField, Tooltip("Set collider size, (Set to zero to ignore)")]
        private Vector3 sizeCollider;

        [SerializeField, Tooltip("Damage bonus applied to the unit")]
        private int increaseDamage;

        [SerializeField, Tooltip("Health bonus applied to the unit")]
        private int increaseHealth;

        public override IEvent GetConfig()
        {
            return new ShapeData(prefab, sizeCollider, increaseDamage, increaseHealth);
        }
    }
}