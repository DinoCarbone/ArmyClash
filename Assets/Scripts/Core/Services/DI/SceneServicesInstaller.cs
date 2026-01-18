using Core.Behaviors.Lifecycle;
using Core.Behaviors.UI;
using Core.Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Services.DI
{
    /// <summary>
    /// Сцено-зависимый инсталлер — бинды, привязанные к объектам и ресурсам текущей сцены.
    /// </summary>
    public class SceneServicesInstaller : MonoInstaller
    {
        [Header("Scene References")]

        [SerializeField, Tooltip("UI Image used to display player's health.")]
        private Image playerHealthBar;

        [SerializeField, Tooltip("Transform where enemies will be spawned.")]
        private Transform enemySpawnPoint;

        [SerializeField, Tooltip("Transform where player will be spawned.")]
        private Transform playerSpawnPoint;

        [SerializeField, Tooltip("Enemy prefab GameObject used by the enemy factory.")]
        private GameObject enemyPrefub;

        [SerializeField, Tooltip("Text element used to display the score.")]
        private TextMeshProUGUI scoreDisplayText;

        public override void InstallBindings()
        {
            BindLifecycleAndGameOver();
            LockCursor();
        }
        

        private void BindLifecycleAndGameOver()
        {
            Container.Bind<GameOverHandler>().AsSingle().NonLazy();
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
