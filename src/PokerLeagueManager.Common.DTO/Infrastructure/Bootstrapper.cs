using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Common.DTO.Infrastructure
{
    public class Bootstrapper : BaseBootstrapper
    {
        private static bool _hasBootstrapped = false;

        public static void Bootstrap()
        {
            if (!_hasBootstrapped)
            {
                Container.RegisterType<IDTOFactory, DTOFactory>();

                PokerLeagueManager.Common.Utilities.Bootstrapper.Bootstrap();

                _hasBootstrapped = true;
            }
        }
    }
}