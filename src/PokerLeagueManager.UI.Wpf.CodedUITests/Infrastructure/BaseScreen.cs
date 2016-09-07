using Microsoft.VisualStudio.TestTools.UITesting;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.Infrastructure
{
    public class BaseScreen
    {
        public BaseScreen(ApplicationUnderTest app)
        {
            App = app;
        }

        protected ApplicationUnderTest App { get; set; }

        public void TakeScreenshot()
        {
            Keyboard.SendKeys("{DOWN}");
        }

        public virtual void VerifyScreen()
        {
            TakeScreenshot();
        }
    }
}
