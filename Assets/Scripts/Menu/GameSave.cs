using UnityEngine;

namespace Menu
{
    public class GameSave : MonoBehaviour
    {
        public enum SaveKey
        {
            LastNbActions
        }

        public void Save(SaveKey saveKey, int value) => PlayerPrefs.SetInt(saveKey.ToString(), value);
        public void Save(SaveKey saveKey, string value) => PlayerPrefs.SetString(saveKey.ToString(), value);
        public void Save(SaveKey saveKey, float value) => PlayerPrefs.SetFloat(saveKey.ToString(), value);

        public int LoadInt(SaveKey saveKey) => PlayerPrefs.GetInt(saveKey.ToString());
        public string LoadString(SaveKey saveKey) => PlayerPrefs.GetString(saveKey.ToString());
        public float LoadFloat(SaveKey saveKey) => PlayerPrefs.GetFloat(saveKey.ToString());
    }
}
