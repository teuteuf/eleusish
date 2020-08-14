using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Rules
{
    public class RuleLoader : MonoBehaviour
    {
        [SerializeField]
        private string rulesEndpoint = default;

        private LoadedRule[] _loadedRules;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadRules());
        }

        IEnumerator LoadRules()
        {
            using (var request = UnityWebRequest.Get(rulesEndpoint))
            {
                yield return request.SendWebRequest();
                while (!request.isDone)
                {
                    yield return null;
                }

                if (request.responseCode == 200)
                {
                    var result = request.downloadHandler.text;
                    var loadedRules = JsonUtility.FromJson<LoadedRules>($"{{\"rules\": {result}}}");
                    OnLoadSuccess(loadedRules);
                }
                else
                {
                    OnLoadFailed();
                }
            }
        }

        private void OnLoadSuccess(LoadedRules loadedRules)
        {
            _loadedRules = loadedRules.rules;
        }

        private void OnLoadFailed()
        {
        }
    }
}
