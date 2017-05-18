using System.Runtime.Serialization;

namespace SVE.Models
{
    public class XmlProjectFormat : IProjectFormat
    {
        public string Name { get { return "xml"; } }

        public XmlProjectFormat()
        {
            //new DataContractSerializer()
        }
    }
}