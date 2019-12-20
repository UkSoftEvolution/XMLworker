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
        /// Функция для получения данных о компании
        /// </summary>
        /// <param name="inn">ИНН компании</param>
        /// <returns>Данные о компании</returns>
        public information GetData(string inn)
        {
            var json = GET_Query(inn);

            if (json.Length == 0)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Сайт заблокировал доступ, пожалуйста, перейдите на сайт, пройдите капчу, после чего нажмите кнопку 'ОК'", "Капча", MessageBoxButton.OK, MessageBoxImage.Exclamation))
                    return GetData(inn);

                return GetData(inn);
            }
            else
            {
                var information = JSON_Deserialize(json);
                if (information.ul.Count == 0)
                {
                    if (MessageBoxResult.OK == MessageBox.Show("Сайт заблокировал доступ, пожалуйста, перейдите на сайт, пройдите капчу, после чего нажмите кнопку 'ОК'", "Капча", MessageBoxButton.OK, MessageBoxImage.Exclamation))
                        return GetData(inn);

                    return GetData(inn);
                }
                else
                    return information;
            }
        }
        /// <summary>
        /// Функция выполняющая GET запрос на сайт
        /// </summary>
        /// <param name="inn">ИНН компании</param>
        /// <returns>JSON ответ</returns>
        private string  GET_Query(string inn)
        {
            WebHeaderCollection whc = new WebHeaderCollection
            {
                { "Cookie", "_ga=GA1.2.2145558115.1576784534; _gid=GA1.2.789566207.1576784534; _gat=1; _gat_UA-49620281-2=1; screen_for_ad=tablet" }
            };

            var webRequest = (HttpWebRequest)WebRequest.Create($"https://www.rusprofile.ru/ajax.php?&query={inn}&action=search");
            webRequest.Headers = whc;
            webRequest.Method = "GET";
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.0 Safari/537.36 Edg/80.0.361.5";
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