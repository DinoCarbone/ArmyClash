using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class GameFlowService : IStartGameService, IEndGameService
    {
        // не использую индексы сцен из билда, чтобы избежать проблем при изменении порядка сцен
        private const string MenuSceneName = "Menu";
        private const string MainSceneName = "Main";
        public void EndGame()
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(MainSceneName);
        }
    }
    public interface IStartGameService
    {
        void StartGame();
    }
    public interface IEndGameService
    {
        void EndGame();
    }
}