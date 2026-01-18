using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ShapeConfig", menuName = "ScriptableObjects/Configs/Shape")]
    public class ShapeConfig : BaseMultiConfig
    {
        [SerializeField, Tooltip("Visual representation model prefab (3D mesh)")]
        private GameObject prefab;

        [SerializeField, Tooltip("Set collider size, (Set to zero to ignore)")]
        private Vector3 sizeCollider;

        public override List<IEvent> GetConfigs()
        {
            List<IEvent> configs = GetConfigsInternal();
            configs.Add(new ShapeData(prefab, sizeCollider));
            return configs;
        }
    }
}