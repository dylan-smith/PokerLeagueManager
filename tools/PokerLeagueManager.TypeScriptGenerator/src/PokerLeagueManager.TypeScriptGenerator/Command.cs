using System.Collections.Generic;

namespace PokerLeagueManager.TypeScriptGenerator
{
    public class Command
    {
        public string Name { get; set; }
        public IList<Prop> Properties { get; set; }

        public string CommandAction()
        {
            return Name.Substring(0, Name.Length - "Command".Length);
        }

        public Command()
        {
            Properties = new List<Prop>();
        }

    }
}
