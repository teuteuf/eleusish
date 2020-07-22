using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public enum AvailableScene
    {
        Home,
        Game,
        Success,
        Fail
    }
    
    public class SceneSwitcher : MonoBehaviour
    {
        public void SwitchToHome() => Switch(AvailableScene.Home);
        public void SwitchToGame() => Switch(AvailableScene.Game);
        public void SwitchToSuccess() => Switch(AvailableScene.Success);
        public void SwitchToFail() => Switch(AvailableScene.Fail);

        public void Switch(AvailableScene scene)
        {
            switch (scene)
            {
                case AvailableScene.Home:
                    SceneManager.LoadScene("HomeScene");
                    break;
                case AvailableScene.Game:
                    SceneManager.LoadScene("GameScene");
                    break;
                case AvailableScene.Success:
                    SceneManager.LoadScene("SuccessScene");
                    break;
                case AvailableScene.Fail:
                    SceneManager.LoadScene("FailScene");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
        }
    }
}
