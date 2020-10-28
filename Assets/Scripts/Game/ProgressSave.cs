using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game
{
    public class ProgressSave : MonoBehaviour
    {
        [SerializeField] private string saveFileName = "/progress.save";
        
        private Progress _progress;
        private BinaryFormatter _binaryFormatter;

        private void Awake()
        {
            _binaryFormatter = new BinaryFormatter();
            
            if (File.Exists(Application.persistentDataPath + saveFileName))
            {
                var fileStream = File.Open(Application.persistentDataPath + saveFileName, FileMode.Open);
                _progress = (Progress)_binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            else
            {
                _progress = new Progress();
            }
        }

        private void Save()
        {
            var fileStream = File.Create(Application.persistentDataPath + saveFileName);
            _binaryFormatter.Serialize(fileStream, _progress);
            fileStream.Close();
        }

        public void UpdateRuleProgress(string ruleId, RuleProgress progress)
        {
            if (_progress.RulesProgress.ContainsKey(ruleId))
            {
                var currentRuleProgress = _progress.RulesProgress[ruleId];
                if (_progress.RulesProgress[ruleId] < progress)
                {
                    _progress.RulesProgress[ruleId] = progress;
                }
            }
            else
            {
                _progress.RulesProgress.Add(ruleId, progress);
            }
            Save();
        }

        public RuleProgress GetRuleProgress(string ruleId)
        {
            return _progress.RulesProgress.ContainsKey(ruleId)
                ? _progress.RulesProgress[ruleId]
                : RuleProgress.NoSuccess;
        }
    }

    public enum RuleProgress
    {
        NoSuccess,
        SuccessWithError,
        PerfectSuccess
    }

    [System.Serializable]
    public class Progress
    {
        public Dictionary<string, RuleProgress> RulesProgress = new Dictionary<string, RuleProgress>();
    }
}