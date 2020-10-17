using System.Collections;
using Game.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens
{
    public class HomeScreen : MonoBehaviour
    {
        [SerializeField] private Button playButton = default;
        [SerializeField] private Text playText = default;
        [SerializeField] private Button validateButton = default;
        [SerializeField] private Text validateText = default;

        [SerializeField] private SceneSwitcher sceneSwitcher = default;
        [SerializeField] private GameSave gameSave = default;

        private RuleLoader _ruleLoader;

        private void Start()
        {
            _ruleLoader = FindObjectOfType<RuleLoader>();

            _ruleLoader.StartLoadingRules(
                gameSave.HasKey(GameSave.SaveKey.PlayerId)
                    ? gameSave.LoadString(GameSave.SaveKey.PlayerId)
                    : null
            );

            StartCoroutine(ManagePlayButton());
            StartCoroutine(ManageValidateButton());
        }

        public void StartPlay()
        {
            gameSave.Save(GameSave.SaveKey.ValidationRun, false);
            sceneSwitcher.SwitchToSelectRule();
        }

        public void StartValidation()
        {
            gameSave.Save(GameSave.SaveKey.ValidationRun, true);
            sceneSwitcher.SwitchToGame();
        }

        private IEnumerator ManagePlayButton()
        {
            var originalPlayTextValue = playText.text;

            playButton.interactable = false;
            playText.text = "Loading...";

            yield return new WaitWhile(() => _ruleLoader.IsLoadingRules);

            playButton.interactable = true;
            playText.text = originalPlayTextValue;
        }

        private IEnumerator ManageValidateButton()
        {
            var originalValidateTextValue = validateText.text;

            validateButton.interactable = false;
            validateText.text = "Loading...";

            yield return new WaitWhile(() => _ruleLoader.IsLoadingRuleToValidate);

            validateButton.interactable = _ruleLoader.RuleToValidate != null;
            validateText.text = originalValidateTextValue;
        }
    }
}