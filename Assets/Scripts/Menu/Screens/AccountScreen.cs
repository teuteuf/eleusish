using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Menu.Screens
{
    public class AccountScreen : MonoBehaviour
    {
        [SerializeField] private string playerEndpoint = default;

        [SerializeField] private GameSave gameSave = default;
        
        [SerializeField] private GameObject inputPseudoLayer = default;
        [SerializeField] private Button sendButton = default;
        
        [SerializeField] private GameObject displayPseudoLayer = default;
        [SerializeField] private Text pseudoText = default;
        [SerializeField] private Text idText = default;
        
        private string _pseudo;

        private void Start()
        {
            UpdateScreen();
        }

        private void UpdateScreen()
        {

            if (gameSave.HasKey(GameSave.SaveKey.PlayerPseudo) && gameSave.HasKey(GameSave.SaveKey.PlayerId))
            {
                inputPseudoLayer.SetActive(false);
                displayPseudoLayer.SetActive(true);
                
                var pseudo = gameSave.LoadString(GameSave.SaveKey.PlayerPseudo);
                var id = gameSave.LoadString(GameSave.SaveKey.PlayerId);
                pseudoText.text = pseudo;
                idText.text = $"ID: {id}";
            }
            else
            {
                inputPseudoLayer.SetActive(true);
                displayPseudoLayer.SetActive(false);
            }
            
        }

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
                var createdPlayer = JsonUtility.FromJson<CreatedPlayer>(request.downloadHandler.text);
                gameSave.Save(GameSave.SaveKey.PlayerPseudo, createdPlayer.pseudo);
                gameSave.Save(GameSave.SaveKey.PlayerId, createdPlayer.id);
                
                UpdateScreen();
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

        private class CreatedPlayer
        {
            public string id;
            public string pseudo;
        }
    }
}
