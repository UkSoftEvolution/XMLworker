using INN_Parser.Rusprofile;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace INN_Parser
{
    /// <summary>
    /// Класс с функциями для парсинга
    /// </summary>
    public class Parser
    {
        #region Function
        /// <summary>
        /// Функция для поиска по ИНН
        /// </summary>
        /// <param name="inn">ИНН компании</param>
        /// <returns>URL на карточку компании</returns>
        public string Search(string inn)
        {
            string json = GET_Query(inn);

            if (json.Length == 0)
                return "https://www.rusprofile.ru/";
            else
            {
                information information = JSON_Deserialize(json);
                if (information.ul.Count == 0)
                    return "https://www.rusprofile.ru/";
                else
                {
                    var id = information.ul[0].url.Replace(@"\", "");
                    return $"https://www.rusprofile.ru{id}";
                }
            }
        }
        /// <summary>
        /// Функция выполняющая GET запрос на сайт
        /// </summary>
        /// <param name="inn">ИНН компании</param>
        /// <returns>JSON ответ</returns>
        private string  GET_Query(string inn)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create($"https://www.rusprofile.ru/ajax.php?&query={inn}&action=search");
            webRequest.Method = "GET";
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3879.0 Safari/537.36 Edg/78.0.249.1";
            webRequest.MaximumAutomaticRedirections = 4;
            webRequest.MaximumResponseHeadersLength = 4;

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();

                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

                var json = streamReader.ReadToEnd();

                webResponse.Close();
                streamReader.Close();

                return json;
            }
            catch
            { return ""; }
        }
        /// <summary>
        /// Функция для десериализации JSON в класс
        /// </summary>
        /// <param name="json">JSON ответ</param>
        /// <returns>Объект класса с данными</returns>
        private information JSON_Deserialize(string json)
        {
            information information = new JavaScriptSerializer().Deserialize<information>(json);
            return information;
        }
        #endregion

    }
}