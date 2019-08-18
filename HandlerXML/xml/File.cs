using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace HandlerXML.xml
{
    /// <summary>
    /// Файл
    /// </summary>
    [XmlRoot(ElementName="Файл")]
    public class File
    {
        /// <summary>
        /// Документ
        /// </summary>
        [XmlElement(ElementName = "Документ")]
        public ObservableCollection<Document> Document { get; set; }
    }
}