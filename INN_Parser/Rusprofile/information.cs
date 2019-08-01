using System.Collections.Generic;

namespace INN_Parser.Rusprofile
{
    /// <summary>
    /// Общий класс JSON ответа
    /// </summary>
    public class information
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ip_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ul_data> ul { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ul_count { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор для инициализации данных
        /// </summary>
        public information() => ul = new List<ul_data>();
        #endregion
    }
}