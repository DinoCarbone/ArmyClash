using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Configs
{
    public abstract class BaseEventConfig : ScriptableObject
    {
        public abstract IEvent GetConfig(int value);
    }
}