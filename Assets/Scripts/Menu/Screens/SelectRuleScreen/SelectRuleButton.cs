using System;
using Game.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.SelectRuleScreen
{
    public class SelectRuleButton : MonoBehaviour
    {
        [SerializeField] private Text buttonText = default;
        
        private Action _onClick;

        public void Set(LoadedRule loadedRule, Action onClick)
        {
            _onClick = onClick;
            buttonText.text = 
                $"Rule of {loadedRule.ruleName.godName}, {new String('I', loadedRule.ruleName.number)}\n" +
                $"Transcribed by {loadedRule.author.pseudo}";
        }

        public void OnClick()
        {
            _onClick();
        }
    }
}