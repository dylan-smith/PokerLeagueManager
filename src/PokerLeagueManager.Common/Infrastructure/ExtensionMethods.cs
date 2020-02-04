using System.IO;

namespace PokerLeagueManager.Common.Infrastructure
{
    public static class ExtensionMethods
    {
        public static string ReadString(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
