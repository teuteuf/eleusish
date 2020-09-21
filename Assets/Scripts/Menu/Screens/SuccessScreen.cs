using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens
{
    public class SuccessScreen : MonoBehaviour
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