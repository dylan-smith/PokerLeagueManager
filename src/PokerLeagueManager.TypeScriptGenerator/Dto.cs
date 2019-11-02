using System.Collections.Generic;

namespace PokerLeagueManager.TypeScriptGenerator
{
    public class Dto
    {
        public string Name { get; set; }
        public IList<Prop> Properties { get; set; }

        public Dto()
        {
            Properties = new List<Prop>();
        }
    }
}
