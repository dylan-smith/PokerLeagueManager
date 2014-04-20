using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class BaseScreen
    {
        public BaseScreen(ApplicationUnderTest app)
        {
            App = app;
        }

        protected ApplicationUnderTest App { get; set; }
    }
}
