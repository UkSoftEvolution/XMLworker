using System;
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
        /// <summary>
        /// Вид деятельности
        /// </summary>
        public string okved_descr { get; set; }
        /// <summary>
        /// Дата регистрации
        /// </summary>
        public string reg_date { get; set; }
        /// <summary>
        /// Ссылка на ИНН
        /// </summary>
        public string linkINN { get; set; }

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
                    Process.Start(linkINN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть сайт: {ex.Message}", $"Открытие сайта: {ex.Source}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        });
        #endregion
    }
}