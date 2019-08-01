using INN_Parser;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace HandlerXML
{
    /// <summary>
    /// Модель для узла Документ
    /// </summary>
    public class DocumentModel
    {
        #region Fields
        /// <summary>
        /// Наименование организации
        /// </summary>
        public string NameOrg { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        /// Количество работников
        /// </summary>
        public int Count { get; set; }
        #endregion

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
                    var element = obj as DocumentModel;
                    Parser parser = new Parser();
                    var url = parser.Search(element.Inn);

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