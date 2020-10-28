using System;
using Game;
using Game.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.SelectRuleScreen
{
    public class SelectRuleButton : MonoBehaviour
    {
        [SerializeField] private Text buttonText = default;
        [SerializeField] private Image progressIcon = default;
        
        [SerializeField] private Color successWithErrorColor = Color.grey;
        [SerializeField] private Color perfectSuccessColor = Color.yellow;
        
        private Action _onClick;

        public void Set(LoadedRule loadedRule, RuleProgress ruleProgress, Action onClick)
        {
            _onClick = onClick;
            buttonText.text = 
                $"Rule {new string('I', loadedRule.ruleName.number)} of {loadedRule.ruleName.godName}\n" +
                $"- transcribed by {loadedRule.author.pseudo} -";

            if (ruleProgress == RuleProgress.NoSuccess)
            {
                progressIcon.enabled = false;
            }
            else
            {
                progressIcon.color = ruleProgress == RuleProgress.SuccessWithError
                    ? successWithErrorColor
                    : perfectSuccessColor;
            }
        }

        public void OnClick()
        {
            _onClick();
        }
    }
}