using HandlerXML;
using HandlerXML.xml;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XMLworker.Other;
using XMLworker.View;

namespace XMLworker.ViewModel
{
    /// <summary>
    /// Модель представление для данных
    /// </summary>
    public class DataViewModel : MVVM
    {
        #region Fields
        private ObservableCollection<Document> documents; //Колекция документов
        private ObservableCollection<Document> originalDocuments; //Колекция оригинальных документов
        private MainViewModel mainVM; //Модель представление главного окна
        private bool indeterminate; //Отображение ожидания
        private bool enabled; //Доступность HEADER

        //SLIDER
        private int minimum; //Минимальное значение
        private int maximum; //Максимальное значение
        private int valueSliderMin; //Значение слайдера
        private int valueSliderMax; //Значение слайдера
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор для инициализации данных
        /// </summary>
        /// <param name="mainVM">Модель представление главного окна</param>
        /// <param name="documents">Колекция документов</param>
        public DataViewModel(MainViewModel mainVM, ObservableCollection<Document> documents)
        {
            this.mainVM = mainVM;
            originalDocuments = documents;
            this.documents = documents;

            Indeterminate = false;
            Enabled = true;

            Minimum = documents.Min(x => x.SwedSSHR.ColRab);
            Maximum = documents.Max(x => x.SwedSSHR.ColRab);
            ValueSliderMin = Minimum;
            ValueSliderMax = Maximum;
        }
        #endregion

        #region Commands
        /// <summary>
        /// Комманда для возвращение на предыдущую страницу
        /// </summary>
        public RelayCommand Back_Click => new RelayCommand(obj =>
        {
            FilesView filesView = new FilesView();
            mainVM.ActivePage = filesView;
            filesView.DataContext = new FilesViewModel(mainVM);
        });
        /// <summary>
        /// Комманда для подтверждения фильтра
        /// </summary>
        public RelayCommand Ok_Click => new RelayCommand(obj =>
        {
            Task.Factory.StartNew(() =>
            {
                Enabled = false;
                Indeterminate = true;

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => Documents = new ObservableCollection<Document>()));
                
                foreach (var doc in originalDocuments)
                {
                    if (doc.SwedSSHR.ColRab >= ValueSliderMin && doc.SwedSSHR.ColRab <= ValueSliderMax)
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => Documents.Add(doc)));
                    else
                        continue;
                }

                Indeterminate = false;
                Enabled = true;
            });
        });
        #endregion

        #region Methods
        /// <summary>
        /// Колекция документов
        /// </summary>
        public ObservableCollection<Document> Documents
        {
            get => documents;
            set
            {
                documents = value;
                OnPropertyChanged(nameof(documents));
            }
        }
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public int Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                OnPropertyChanged(nameof(minimum));
            }
        }
        /// <summary>
        /// Максимальное значение
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
        /// Значение слайдера
        /// </summary>
        public int ValueSliderMin
        {
            get => valueSliderMin;
            set
            {
                valueSliderMin = value;
                OnPropertyChanged(nameof(valueSliderMin));
            }
        }
        /// <summary>
        /// Значение слайдера
        /// </summary>
        public int ValueSliderMax
        {
            get => valueSliderMax;
            set
            {
                valueSliderMax = value;
                OnPropertyChanged(nameof(valueSliderMax));
            }
        }
        /// <summary>
        /// Отображение ожидания
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
        /// Доступность HEADER
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged(nameof(enabled));
            }
        }
        #endregion
    }
}