using System;
using System.IO;
using System.Xml;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class XmlReaderFacade : IDisposable
    {
        private StringReader _reader;

        public XmlReaderFacade(string data)
        {
            _reader = new StringReader(data);

            try
            {
                XmlReader = XmlReader.Create(_reader);
            }
            catch
            {
                _reader.Dispose();
                throw;
            }
        }

        ~XmlReaderFacade()
        {
            Dispose(false);
        }

        public XmlReader XmlReader { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (XmlReader != null)
            {
                XmlReader.Dispose();
                _reader = null;
            }

            if (_reader != null)
            {
                _reader.Dispose();
            }
        }
    }
}
