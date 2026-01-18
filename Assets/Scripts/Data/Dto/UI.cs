using UnityEngine;

namespace Data.Dto
{
    public class IconChangeData : IIconChangeData
    {
        private readonly Sprite icon;
        public Sprite Icon => icon;
        public IconChangeData(Sprite icon)
        {
            this.icon = icon;
        }
    }
}