using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Utilities
{
    public static class UnityHelper
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();
                }

                return _container;
            }
        }
    }
}
