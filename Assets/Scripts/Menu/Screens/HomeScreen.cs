using System;
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
        [SerializeField] private RuleLoader ruleLoader = default;

        private void Start()
        {
            StartCoroutine(ManagePlayButton());
        }

        private IEnumerator ManagePlayButton()
        {
            var originalPlayTextValue = playText.text;
            
            playButton.interactable = false;
            playText.text = "Loading...";
            
            yield return new WaitWhile(() => ruleLoader.IsLoading);

            playButton.interactable = true;
            playText.text = originalPlayTextValue;
        }
    }
}
