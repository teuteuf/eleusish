﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Rules
{
    public class RuleLoader : MonoBehaviour
    {
        [SerializeField] private string rulesEndpoint = default;

        public LoadedRule[] LoadedRules { get; private set; }

        public bool IsLoading { get; private set; } = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadRules());
        }

        private IEnumerator LoadRules()
        {
            IsLoading = true;

            var request = UnityWebRequest.Get(rulesEndpoint);
            yield return request.SendWebRequest();
            yield return new WaitUntil(() => request.isDone);

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

        private void OnLoadSuccess(LoadedRules loadedRules)
        {
            LoadedRules = loadedRules.rules;
            IsLoading = false;
        }

        private void OnLoadFailed()
        {
            IsLoading = false;
        }
    }
}