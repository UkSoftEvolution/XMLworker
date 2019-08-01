using INN_Parser.Rusprofile;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

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
                return "";
            else
            {
                information information = JSON_Deserialize(json);
                var id = information.ul[0].url.Replace(@"\", "");
                return $"https://www.rusprofile.ru{id}";
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