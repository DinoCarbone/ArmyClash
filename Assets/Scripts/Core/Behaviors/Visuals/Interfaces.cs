using System;
using UnityEngine;

namespace Core.Behaviors.Visuals
{
    public interface IModelSwitchNotifyer
    {
        event Action<GameObject> OnModelSwitch;
    }
}