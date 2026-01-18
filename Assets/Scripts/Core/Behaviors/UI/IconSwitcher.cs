using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine.UI;

namespace Core.Behaviors.UI
{
    public class IconSwitcher : IProvider, IInternalEventReceiver
    {
        private readonly Image iconImage;

        public IconSwitcher(Image iconImage)
        {
            this.iconImage = iconImage;
        }

        public void ReceiveEvent(IEvent @event)
        {
            if (@event is IIconChangeData iconChangeData)
            {
                iconImage.sprite = iconChangeData.Icon;
            }
        }
    }
}