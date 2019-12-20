using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Zachestnyibiznes_API
{
    /// <summary>
    /// Класс для работы с сайтом zachestnyibiznes.ru
    /// </summary>
    public class Zachestnyibiznes
    {
        /// <summary>
        /// Функция для получения данных
        /// </summary>
        /// <param name="value">Значение ИНН компании</param>
        /// <returns>Данные по ИНН компании</returns>
        public Dictionary<string, string> getData(string value)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            
            var result = GET($"https://zachestnyibiznes.ru/search?query={value}");
            string[] resultArray = result.Split(new string[] { "<tbody>", "</tbody>" }, System.StringSplitOptions.RemoveEmptyEntries);
            resultArray = resultArray[1].Split(new string[] { "<td", "</td>" }, System.StringSplitOptions.RemoveEmptyEntries);

            var newArray = resultArray[1].Split(new string[] { "=", ">" }, System.StringSplitOptions.RemoveEmptyEntries);
            data.Add("linkINN", $"https://zachestnyibiznes.ru{newArray[4].Replace("'", "")}");

            newArray = resultArray[9].Split(new string[] { "=", ">" }, System.StringSplitOptions.RemoveEmptyEntries);
            data.Add("reg_date", newArray[5]);

            newArray = resultArray[11].Split(new string[] { "=", ">" }, System.StringSplitOptions.RemoveEmptyEntries);
            data.Add("address", newArray[10].Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries)[1]);

            result = GET(data["linkINN"]);
            resultArray = result.Split(new string[] { "<div class=", ">", "</div>", "<br>" }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int index = 0; index < resultArray.Length; index++)
            {
                if (resultArray[index].Contains("Основной вид деятельности:"))
                    data.Add("okved_descr", resultArray[index + 10].Split('\n')[0]);
            }

            return data;
        }
        /// <summary>
        /// Функция для HTTP GET запроса
        /// </summary>
        /// <param name="url">Адресс запроса</param>
        /// <returns>Результат запроса</returns>
        private string GET(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();

                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

                var answer = streamReader.ReadToEnd();

                webResponse.Close();
                streamReader.Close();

                return answer;
            }
            catch
            { return ""; }
        }
    }
}