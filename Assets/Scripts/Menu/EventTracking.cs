using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Menu
{
    [RequireComponent(typeof(GameSave))]
    public class EventTracking : MonoBehaviour
    {
        private GameSave _gameSave;

        private void Awake()
        {
            _gameSave = GetComponent<GameSave>();
        }

        public void TrackChangeScene(AvailableScene scene)
        {
            Analytics.CustomEvent("CHANGE_SCENE", new Dictionary<string, object>
            {
                {"scene", scene.ToString()},
                {"playerId", _gameSave.LoadString(GameSave.SaveKey.PlayerId)},
                {"playerPseudo", _gameSave.LoadString(GameSave.SaveKey.PlayerPseudo)},
            });
        }

        public void TrackStartRun()
        {
            Analytics.CustomEvent("START_RUN", new Dictionary<string, object>
            {
                {"playerId", _gameSave.LoadString(GameSave.SaveKey.PlayerId)},
                {"playerPseudo", _gameSave.LoadString(GameSave.SaveKey.PlayerPseudo)},
                {"selectedRule", _gameSave.LoadString(GameSave.SaveKey.SelectedRule)},
            });
        }

        public void TrackFail()
        {
            Analytics.CustomEvent("FAIL", GetGameInfos());
        }

        public void TrackSuccess()
        {
            Analytics.CustomEvent("SUCCESS", GetGameInfos());
        }

        public void TrackValidation()
        {
            Analytics.CustomEvent("VALIDATION", GetGameInfos());
        }

        private Dictionary<string, object> GetGameInfos()
        {
            return new Dictionary<string, object>
            {
                {"playerId", _gameSave.LoadString(GameSave.SaveKey.PlayerId)},
                {"playerPseudo", _gameSave.LoadString(GameSave.SaveKey.PlayerPseudo)},
                {"selectedRule", _gameSave.LoadString(GameSave.SaveKey.SelectedRule)},
                {"lastNbActions", _gameSave.LoadString(GameSave.SaveKey.LastNbActions)},
                {"lastNbErrors", _gameSave.LoadString(GameSave.SaveKey.LastNbErrors)},
            };
        }
    }
}