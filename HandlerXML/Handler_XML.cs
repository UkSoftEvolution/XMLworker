using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace HandlerXML
{
    /// <summary>
    /// Класс с функциями для обработки XML файлов
    /// </summary>
    public class Handler_XML
    {
        /// <summary>
        /// Функция для десириализации XML файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="documents">Коллекция данных</param>
        /// <returns>Коллекция данных</returns>
        public ObservableCollection<xml.Document> DeserializeXML(string path, ObservableCollection<xml.Document> documents)
        {
            if (File.Exists(path))
            {
                XmlSerializer xmlSerializer  = new XmlSerializer(typeof(xml.File));
                xml.File file;

                using (Stream reader = new FileStream(path, FileMode.Open))
                {
                    file = (xml.File)xmlSerializer.Deserialize(reader);
                }

                foreach (var item in file.Document)
                {
                    documents.Add(item);
                }

                return documents;
            }
            else
                return documents;
        }
    }
}