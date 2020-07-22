using System;
using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs.Menu.Canvas
{
    public class SuccessCanvas : MonoBehaviour
    {
        [SerializeField] private GameSave gameSave = default;
        [SerializeField] private Text textNbLastActions = default;
        [SerializeField] private string tokenNbActions = "#nbActions";

        private void Awake()
        {
            var lastNbActions = gameSave.LoadInt(GameSave.SaveKey.LastNbActions);
            textNbLastActions.text = textNbLastActions.text.Replace(tokenNbActions, lastNbActions.ToString());
        }
    }
}
