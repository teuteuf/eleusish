namespace Game.Rules
{
    [System.Serializable]
    public class LoadedRules
    {
        public LoadedRule[] rules;
    }

    [System.Serializable]
    public class LoadedRule
    {
        public string id;
        public string code;
    }
}