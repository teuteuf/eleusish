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
        public bool validated;
        public Author author;
        public RuleName ruleName;

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(code)}: {code}, {nameof(validated)}: {validated}, {nameof(author)}: {author}, {nameof(ruleName)}: {ruleName}";
        }
    }

    [System.Serializable]
    public class Author
    {
        public string pseudo;

        public override string ToString()
        {
            return $"{nameof(pseudo)}: {pseudo}";
        }
    }

    [System.Serializable]
    public class RuleName
    {
        public string godName;
        public int number;

        public override string ToString()
        {
            return $"{nameof(godName)}: {godName}, {nameof(number)}: {number}";
        }
    }
}