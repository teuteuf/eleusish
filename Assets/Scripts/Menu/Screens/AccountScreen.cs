using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Menu.Screens
{
    public class AccountScreen : MonoBehaviour
    {
        [SerializeField] private string playerEndpoint = default;
        
        [SerializeField] private Button sendButton = default;
        
        private string _pseudo;

        public void SetPseudo(string pseudo)
        {
            _pseudo = pseudo;
        }

        public void CreatePlayer()
        {
            StartCoroutine(SendPlayer());
        }

        private IEnumerator SendPlayer()
        {
            sendButton.interactable = false;
            
            var body = new NewPlayerBody
            {
                pseudo = _pseudo
            };
            
            var request = UnityWebRequest.Put(playerEndpoint, JsonUtility.ToJson(body));
            request.method = "POST";
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            yield return new WaitUntil(() => request.isDone);

            if (request.responseCode == 201)
            {
                var result = request.downloadHandler.text;
                Debug.Log("player created");
                Debug.Log(result);
            }
            else
            {
                Debug.Log("player creation failed");
                Debug.Log(request.error);
            }
            
            sendButton.interactable = true;
        }

        private class NewPlayerBody
        {
            public string pseudo;
        }
    }
}
