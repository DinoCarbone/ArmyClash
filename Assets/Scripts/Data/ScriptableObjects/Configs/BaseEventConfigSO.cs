using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    public abstract class BaseEventConfigSO : ScriptableObject
    {
        public abstract IEvent GetConfig();
    }
}