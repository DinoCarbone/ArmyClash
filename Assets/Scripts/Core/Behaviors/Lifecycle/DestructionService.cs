using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public class DestructionService : IDestructionService
    {
        public void Destruct(GameObject coreObject)
        {
            if (coreObject != null) Object.Destroy(coreObject);
            else Debug.LogWarning("Attempted to destruct a null GameObject.");
        }
    }
}