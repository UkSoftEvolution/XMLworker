using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace HandlerXML
{
    /// <summary>
    /// Класс с функциями для обработки XML файлов
    /// </summary>
    public class Handler_XML
    {
        /// <summary>
        /// Функция для обработки файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="documents">Колекция данных</param>
        /// <returns>Колекцию данных</returns>
        public ObservableCollection<DocumentModel> Process(string path, ObservableCollection<DocumentModel> documents)
        {
            if (File.Exists(path))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);

                XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Документ");
                foreach(XmlNode xmlNode in xmlNodeList)
                {
                    DocumentModel documentModel = new DocumentModel();

                    foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
                    {
                        if (xmlChildNode.Name == "СведНП")
                        {
                            documentModel.NameOrg = xmlChildNode.Attributes.GetNamedItem("НаимОрг").Value;
                            documentModel.Inn = xmlChildNode.Attributes.GetNamedItem("ИННЮЛ").Value;
                        }
                        else
                            documentModel.Count = Convert.ToInt32(xmlChildNode.Attributes.GetNamedItem("КолРаб").Value);
                    }

                    documents.Add(documentModel);
                }
                return documents;
            }
            else
                return documents;
        }
    }
}