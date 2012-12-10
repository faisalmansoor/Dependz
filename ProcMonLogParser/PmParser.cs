using System.Collections.Generic;
using System.Xml;

namespace ProcMonLogParser
{
    public class PmParser
    {
        private readonly string _logPath;

        public static IEnumerable<PmEvent> GetEvents(string logPath)
        {
            var parser = new PmParser(logPath);
            return parser.GetEvents();
        }

        public PmParser(string logPath)
        {
            _logPath = logPath;
        }

        public IEnumerable<PmEvent> GetEvents()
        {
            XmlReader reader = XmlReader.Create(_logPath);
            reader.MoveToContent();
            if (reader.ReadToFollowing("event"))
            {
                while(!reader.EOF)
                {
                    XmlReader eventReader = reader.ReadSubtree();
                    
                    var pmEvent = PmEvent.Create(eventReader);

                    yield return pmEvent;
                    
                    if( !reader.ReadToNextSibling("event") )
                    {
                        break;
                    }
                }
            }
        }
    }
}