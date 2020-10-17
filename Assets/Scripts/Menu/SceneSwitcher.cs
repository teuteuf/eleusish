using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public enum AvailableScene
    {
        Home,
        SelectRule,
        Game,
        Success,
        Fail,
        Account,
        Validate
    }

    public class SceneSwitcher : MonoBehaviour
    {
        public void SwitchToHome() => Switch(AvailableScene.Home);
        public void SwitchToSelectRule() => Switch(AvailableScene.SelectRule);
        public void SwitchToGame() => Switch(AvailableScene.Game);
        public void SwitchToSuccess() => Switch(AvailableScene.Success);
        public void SwitchToFail() => Switch(AvailableScene.Fail);
        public void SwitchToAccount() => Switch(AvailableScene.Account);
        public void SwitchToValidate() => Switch(AvailableScene.Validate);

        public void Switch(AvailableScene scene)
        {
            switch (scene)
            {
                case AvailableScene.Home:
                    SceneManager.LoadScene("HomeScene");
                    break;
                case AvailableScene.SelectRule:
                    SceneManager.LoadScene("SelectRuleScene");
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
                case AvailableScene.Account:
                    SceneManager.LoadScene("AccountScene");
                    break;
                case AvailableScene.Validate:
                    SceneManager.LoadScene("ValidateScene");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
        }
    }
}