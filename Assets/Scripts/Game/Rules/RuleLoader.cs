using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Rules
{
    public class RuleLoader : MonoBehaviour
    {
        public LoadedRule[] LoadedRules { get; private set; }

        [SerializeField]
        private string rulesEndpoint = default;

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
            LoadedRules = loadedRules.rules;
        }

        private void OnLoadFailed()
        {
        }
    }
}
