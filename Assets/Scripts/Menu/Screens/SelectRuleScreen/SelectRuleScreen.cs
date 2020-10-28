using System;
using Game;
using Game.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.SelectRuleScreen
{
    
    public class SelectRuleScreen : MonoBehaviour
    {
        [SerializeField] private SelectRuleButton selectRuleButtonPrefab = default;
        [SerializeField] private LayoutGroup selectRuleButtonList = default;
        [SerializeField] private GameSave gameSave = default;
        [SerializeField] private SceneSwitcher sceneSwitcher = default;
        [SerializeField] private ProgressSave progressSave = default;
        
        private RuleLoader _ruleLoader;

        private void Start()
        {
            _ruleLoader = FindObjectOfType<RuleLoader>();
            foreach (var loadedRule in _ruleLoader.LoadedRules)
            {
                var selectRuleButton = Instantiate(selectRuleButtonPrefab, selectRuleButtonList.transform);
                selectRuleButton.Set(loadedRule, progressSave.GetRuleProgress(loadedRule.id), () =>
                {
                    gameSave.Save(GameSave.SaveKey.SelectedRule, loadedRule.id);
                    sceneSwitcher.SwitchToGame();
                });
            }
        }
    }
}