using Core.Behaviors.Lifecycle;
using Core.Providers;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    /// <summary>
    /// Инсталлер общих (сквозных) сервисов. Здесь находятся синглтон-сервисы
    /// и фабрики, не зависящие от конкретной сцены или префабов.
    /// </summary>
    public class CommonServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindHybridInjectService();
            BindCameraProvider();
            BindTickableService();
        }

        private void BindHybridInjectService()
        {
            Container.Bind<IHybridInjectService>()
                .To<HybridInjectService>()
                .AsSingle();
        }

        private void BindCameraProvider()
        {
            Container.Bind<ICameraProvider>()
                .To<CameraProvider>()
                .FromInstance(new CameraProvider(Camera.main))
                .AsSingle();
        }

        private void BindTickableService()
        {
            Container.BindInterfacesAndSelfTo<TickableService>().AsSingle();
        }
    }
}
