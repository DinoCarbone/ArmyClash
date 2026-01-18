using System;
using Core.Providers;
using Data.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace Data.ScriptableObjects.Providers.UI
{
    [CreateAssetMenu(fileName = "IconSwitcher",
      menuName = "ScriptableObjects/Providers/UI/IconSwitcher")]
    public class IconSwitcherSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Image icon = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                icon = dependencies[0] as Image ?? (dependencies[0] as GameObject)?.GetComponent<Image>();
            }
            if(icon == null)
                throw new Exception("IconSwitcherSO: Image is empty");

            return new Core.Behaviors.UI.IconSwitcher(icon);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Icon",
                    typeName = "UnityEngine.UI.Image, UnityEngine.UI",
                    optional = false
                }
            };
        }
    }
}