using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SummaryAttribute : Attribute
    {
        public SummaryAttribute(string summary)
        {
            Summary = summary;
        }

        public string Summary { get; private set; }
    }
}
