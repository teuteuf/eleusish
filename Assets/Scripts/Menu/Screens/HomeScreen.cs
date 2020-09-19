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
        
        [SerializeField] private RuleLoader ruleLoader = default;

        [SerializeField] private SceneSwitcher sceneSwitcher = default;
        [SerializeField] private GameSave gameSave = default;

        private void Start()
        {
            StartCoroutine(ManagePlayButton());
            StartCoroutine(ManageValidateButton());
        }

        public void StartPlay()
        {
            gameSave.Save(GameSave.SaveKey.ValidationRun, false);
            sceneSwitcher.SwitchToGame();
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
            
            yield return new WaitWhile(() => ruleLoader.IsLoadingRules);

            playButton.interactable = true;
            playText.text = originalPlayTextValue;
        }

        private IEnumerator ManageValidateButton()
        {
            var originalValidateTextValue = validateText.text;
            
            validateButton.interactable = false;
            validateText.text = "Loading...";
            
            yield return new WaitWhile(() => ruleLoader.IsLoadingRuleToValidate);

            validateButton.interactable = ruleLoader.RuleToValidate != null;
            validateText.text = originalValidateTextValue;
        }
    }
}
