using INN_Parser;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace HandlerXML.xml
{
    /// <summary>
    /// СведНП
    /// </summary>
    [XmlRoot(ElementName = "СведНП")]
    public class SwedNP
    {
        /// <summary>
        /// НаимОрг
        /// </summary>
        [XmlAttribute(AttributeName = "НаимОрг")]
        public string NaimOrg { get; set; }
        /// <summary>
        /// ИННЮЛ
        /// </summary>
        [XmlAttribute(AttributeName = "ИННЮЛ")]
        public string INNUL { get; set; }

        #region Commands
        /// <summary>
        /// Команда клика по ИНН
        /// </summary>
        public Command INN_Click => new Command(obj =>
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var element = obj as Document;
                    Parser parser = new Parser();
                    var url = parser.Search(element.SwedNP.INNUL);

                    Process.Start(url);
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть сайт", "Открытие сайта", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        });
        #endregion
    }
}