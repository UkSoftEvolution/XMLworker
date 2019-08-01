using FilesAPI;
using HandlerXML;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using XMLworker.Other;
using XMLworker.View;

namespace XMLworker.ViewModel
{
    /// <summary>
    /// Модель представлений для страницы файлов
    /// </summary>
    public class FilesViewModel : MVVM
    {
        #region Fields
        private Visibility visibilityText; //Отображение текста
        private Visibility visiblityNext; //Отображение кнопки Next
        private bool enabledButton; //Доступность кнопки
        private bool indeterminate; //Иникатор ожидания
        private int count; //Количество обработанных файлов
        private int maximum; //Всего файлов для обработки
        private string text; //Текст подписи
        private ObservableCollection<DocumentModel> documents; //Коллекция документов
        private MainViewModel mainVM; //Модель предствление главного окна
        private int countData; //Количество данных
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор для инициадизации данных
        /// </summary>
        public FilesViewModel(MainViewModel mainVM)
        {
            Indeterminate = true;
            Count = 0;
            Maximum = 100;
            EnabledButton = true;
            VisibilityText = Visibility.Hidden;
            VisiblityNext = Visibility.Hidden;

            this.mainVM = mainVM;
        }
        #endregion

        #region Command
        /// <summary>
        /// Комманда для кнопки выбора файлов
        /// </summary>
        public RelayCommand Choose_Click => new RelayCommand(obj =>
        {
            File_API file_API = new File_API();
            string[] listFiles = file_API.Open();

            if (listFiles.Length == 0)
                return;
            else
            {
                Count = 0;
                CountData = 0;
                EnabledButton = false;
                Maximum = listFiles.Length;
                Indeterminate = false;
                Text = "Обрабатывается...";
                VisibilityText = Visibility.Visible;
                VisiblityNext = Visibility.Hidden;

                documents = new ObservableCollection<DocumentModel>();

                Task.Factory.StartNew(() =>
                {
                    Handler_XML handler_XML = new Handler_XML();
                    foreach (string file in listFiles)
                    {
                        documents = handler_XML.Process(file, documents);
                        CountData = documents.Count;
                        Count++;
                    }

                    EnabledButton = true;
                    Text = "Обработано...";
                    VisiblityNext = Visibility.Visible;
                });
            }
        });
        /// <summary>
        /// Команда для перехода на следующую страницу
        /// </summary>
        public RelayCommand Next_Click => new RelayCommand(obj =>
        {
            DataView dataView = new DataView();
            mainVM.ActivePage = dataView;
            dataView.DataContext = new DataViewModel(mainVM, documents);
        });
        #endregion

        #region Methods
        /// <summary>
        /// Отображение текста
        /// </summary>
        public Visibility VisibilityText
        {
            get => visibilityText;
            set
            {
                visibilityText = value;
                OnPropertyChanged(nameof(visibilityText));
            }
        }
        /// <summary>
        /// Доступность кнопки
        /// </summary>
        public bool EnabledButton
        {
            get => enabledButton;
            set
            {
                enabledButton = value;
                OnPropertyChanged(nameof(enabledButton));
            }
        }
        /// <summary>
        /// Иникатор ожидания
        /// </summary>
        public bool Indeterminate
        {
            get => indeterminate;
            set
            {
                indeterminate = value;
                OnPropertyChanged(nameof(indeterminate));
            }
        }
        /// <summary>
        /// Количество обработанных файлов
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(count));
            }
        }
        /// <summary>
        /// Всего файлов для обработки
        /// </summary>
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                OnPropertyChanged(nameof(maximum));
            }
        }
        /// <summary>
        /// Отображение кнопки Next
        /// </summary>
        public Visibility VisiblityNext
        {
            get => visiblityNext;
            set
            {
                visiblityNext = value;
                OnPropertyChanged(nameof(visiblityNext));
            }
        }
        /// <summary>
        /// Текст подписи
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(text));
            }
        }
        public int CountData
        {
            get => countData;
            set
            {
                countData = value;
                OnPropertyChanged(nameof(countData));
            }
        }
        #endregion
    }
}