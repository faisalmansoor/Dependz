using System;
using System.Xml.Linq;

namespace ProcMonLogParser
{
    public static class Extensions
    {
        public static T Value<T>(this XElement element, XName name)
        {
            XElement child = element.Element(name);
            if( child == null )
            {
                return default(T);
            }

            if(typeof(T) == typeof(Version))
            {
                object version = new Version(child.Value);
                return (T) version;
            }

            return (T) Convert.ChangeType(child.Value, typeof (T));
        }
    }
}
