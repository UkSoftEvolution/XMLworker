using System.Windows.Forms;

namespace FilesAPI
{
    /// <summary>
    /// Класс для работы с API функция файлов
    /// </summary>
    public class File_API
    {
        #region Function
        /// <summary>
        /// Открытие диалогового окна для выбора файлов
        /// </summary>
        /// <returns>Масив выбраных файлов</returns>
        public string[] Open()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileNames;
                else
                    return openFileDialog.FileNames;
            }
        }
        #endregion
    }
}