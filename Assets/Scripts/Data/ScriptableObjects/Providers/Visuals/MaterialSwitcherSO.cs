using Core.Behaviors.Visuals;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Visuals
{
    [CreateAssetMenu(fileName = "MaterialSwitcher",
      menuName = "ScriptableObjects/Providers/Visuals/MaterialSwitcher")]
    public class MaterialSwitcherSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            MeshRenderer render = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                render = dependencies[0] as MeshRenderer ?? (dependencies[0] as GameObject)?.GetComponent<MeshRenderer>();
            }
            if(render == null)
            Debug.LogWarning($"MaterialSwitcherSO: MeshRenderer is empty.");

            return new MaterialSwitcher(render);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return new ContextRequirement[]
            {
        new ContextRequirement
        {
            displayName = "Mesh Renderer",
            typeName = "UnityEngine.MeshRenderer, UnityEngine",
            optional = false
        }
            };
        }
    }
}