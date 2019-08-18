using System.Xml.Serialization;

namespace HandlerXML.xml
{
    /// <summary>
    /// СведССЧР
    /// </summary>
    [XmlRoot(ElementName = "СведССЧР")]
    public class SwedSSHR
    {
        /// <summary>
        /// КолРаб
        /// </summary>
        [XmlAttribute(AttributeName = "КолРаб")]
        public int ColRab { get; set; }
    }
}