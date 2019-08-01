using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XMLworker.Other
{
    /// <summary>
    /// Класс для работы с патерном MVVM
    /// </summary>
    public class MVVM : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}