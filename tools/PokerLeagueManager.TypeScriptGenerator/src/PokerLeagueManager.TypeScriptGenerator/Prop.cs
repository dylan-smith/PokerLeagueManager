using System;

namespace PokerLeagueManager.TypeScriptGenerator
{
    public class Prop
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string TypeScriptType()
        {
            switch (Type)
            {
                case "int":
                    return "number";
                case "string":
                    return "string";
                case "DateTime":
                    return "string";
                case "double":
                    return "number";
                case "Guid":
                    return "string";
                case "bool":
                    return "boolean";
                default:
                    throw new ArgumentException("Unrecognized type", nameof(Type));
            }
        }
    }
}
