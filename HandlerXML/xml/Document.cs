using System.Xml.Serialization;

namespace HandlerXML.xml
{
    /// <summary>
    /// Документ
    /// </summary>
    [XmlRoot(ElementName = "Документ")]
    public class Document
    {
        /// <summary>
        /// СведНП
        /// </summary>
        [XmlElement(ElementName = "СведНП")]
        public SwedNP SwedNP { get; set; }
        /// <summary>
        /// СведССЧР
        /// </summary>
        [XmlElement(ElementName = "СведССЧР")]
        public SwedSSHR SwedSSHR { get; set; }
    }
}