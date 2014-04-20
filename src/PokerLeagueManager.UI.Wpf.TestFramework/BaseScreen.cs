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

        public void TakeScreenshot()
        {
            Keyboard.SendKeys(" ");
        }

        public virtual void VerifyScreen()
        {
            TakeScreenshot();
        }

        protected ApplicationUnderTest App { get; set; }
    }
}
