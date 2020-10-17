using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.SelectRuleScreen
{
    public class SelectRuleButton : MonoBehaviour
    {
        [SerializeField] private Text buttonText = default;
        
        private Action _onClick;

        public void Set(string ruleId, Action onClick)
        {
            _onClick = onClick;
            buttonText.text = $"Rule: {ruleId}";
        }

        public void OnClick()
        {
            _onClick();
        }
    }
}