using System.Collections;
using Game.Rules;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Menu.Screens
{
    public class ValidateScreen : MonoBehaviour
    {
        [SerializeField] private string rulesEndpoint = default;
        [SerializeField] private string tokenNbErrors = "#nbErrors";

        [SerializeField] private Text textNbErrors = default;
        [SerializeField] private Button validateButton = default;
        [SerializeField] private Text validateText = default;

        [SerializeField] private GameSave gameSave = default;

        private RuleLoader _ruleLoader;

        private void Awake()
        {
            _ruleLoader = FindObjectOfType<RuleLoader>();

            var lastNbErrors = gameSave.LoadInt(GameSave.SaveKey.LastNbErrors);
            textNbErrors.text = textNbErrors.text.Replace(tokenNbErrors, lastNbErrors.ToString());
            validateButton.interactable = lastNbErrors == 0;
        }

        public void ValidateRule()
        {
            StartCoroutine(StartValidatingRule());
        }

        private IEnumerator StartValidatingRule()
        {
            var originalText = validateText.text;
            validateText.text = "VALIDATING...";

            var ruleToValidate = _ruleLoader.RuleToValidate;

            validateButton.interactable = false;

            var body = new ValidateRuleBody
            {
                validated = true
            };

            var request = UnityWebRequest.Put(
                $"{rulesEndpoint}/{ruleToValidate.id}",
                JsonUtility.ToJson(body)
            );
            request.method = "PATCH";

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Player-ID", gameSave.LoadString(GameSave.SaveKey.PlayerId));
            request.SetRequestHeader("API-Key", Env.APIKey);

            yield return request.SendWebRequest();

            yield return new WaitUntil(() => request.isDone);

            if (request.responseCode == 200)
            {
                validateText.text = "VALIDATED";
            }
            else
            {
                Debug.LogError("Failed to validate rule...");
                validateText.text = originalText;
                validateButton.interactable = true;
            }
        }

        private class ValidateRuleBody
        {
            // ReSharper disable once InconsistentNaming
            public bool validated;
        }
    }
}