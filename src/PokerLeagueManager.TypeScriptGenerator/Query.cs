using System.Collections.Generic;

namespace PokerLeagueManager.TypeScriptGenerator
{
    public enum QueryReturnType
    {
        Primitive,
        Dto,
        Array
    }
    public class Query
    {
        public string Name { get; set; }
        public QueryReturnType ReturnType { get; set; }
        public string Returns { get; set; }
        public IList<Prop> Properties { get; set; }

        public string QueryAction()
        {
            return Name.Substring(0, Name.Length - "Query".Length);
        }

        public Query()
        {
            Properties = new List<Prop>();
        }

    }
}
