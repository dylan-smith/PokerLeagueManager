using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using GenFu;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var action = args[0];

            switch (action)
            {
                case "CreateEventSubscriber":
                    CreateEventSubscriber.Create(args);
                    break;
                case "GenerateSampleData":
                    GenerateSampleData.Generate(args);
                    break;
                case "PublishEvents":
                    PublishUnpublishedEvents.Publish();
                    break;
                default:
                    throw new ArgumentException("Unrecognized Action");
            }
        }
    }
}
