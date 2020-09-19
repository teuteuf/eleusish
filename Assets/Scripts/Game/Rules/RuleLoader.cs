using System.Collections;
using Menu;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Rules
{
    public class RuleLoader : MonoBehaviour
    {
        [SerializeField] private string rulesEndpoint = default;

        [SerializeField] private GameSave gameSave = default;

        private static RuleLoader _instance;
        
        public LoadedRule[] LoadedRules { get; private set; }
        public LoadedRule RuleToValidate { get; private set; }

        public bool IsLoadingRules { get; private set; } = false;
        public bool IsLoadingRuleToValidate { get; private set; } = false;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                
                StartCoroutine(LoadRules());

                if (gameSave.HasKey(GameSave.SaveKey.PlayerId))
                {
                    StartCoroutine(LoadRuleToValidate(gameSave.LoadString(GameSave.SaveKey.PlayerId)));
                }
            }
            else
            {
                Destroy(this);
            }
        }

        private IEnumerator LoadRules()
        {
            IsLoadingRules = true;

            var request = UnityWebRequest.Get($"{rulesEndpoint}?validated=true");
            yield return request.SendWebRequest();
            yield return new WaitUntil(() => request.isDone);

            if (request.responseCode == 200)
            {
                var result = request.downloadHandler.text;
                var loadedRules = JsonUtility.FromJson<LoadedRules>($"{{\"rules\": {result}}}");
                
                Debug.Log($"Rules loaded. Nb rules: {loadedRules.rules.Length}");
                
                LoadedRules = loadedRules.rules;
                IsLoadingRules = false;
            }
            else
            {
                Debug.LogError("Failed to load rules.");
                IsLoadingRules = false;
            }
        }

        private IEnumerator LoadRuleToValidate(string playerId)
        {
            IsLoadingRuleToValidate = true;

            var request = UnityWebRequest.Get($"{rulesEndpoint}?validated=false&authorId={playerId}");
            yield return request.SendWebRequest();
            yield return new WaitUntil(() => request.isDone);

            if (request.responseCode == 200)
            {
                var result = request.downloadHandler.text;
                var loadedRules = JsonUtility.FromJson<LoadedRules>($"{{\"rules\": {result}}}");
                
                Debug.Log($"Rules to validate loaded. Nb rules: {loadedRules.rules.Length}");
                
                RuleToValidate = loadedRules.rules.Length > 0 ? loadedRules.rules[0] : null;
                IsLoadingRuleToValidate = false;
            }
            else
            {
                Debug.LogError("Failed to load rules to validate.");
                IsLoadingRuleToValidate = false;
            }
        }
    }
}