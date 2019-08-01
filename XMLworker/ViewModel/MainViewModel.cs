using System.Windows;
using System.Windows.Controls;
using XMLworker.Other;
using XMLworker.View;

namespace XMLworker.ViewModel
{
    /// <summary>
    /// Модель представлений для главного окна
    /// </summary>
    public class MainViewModel : MVVM
    {
        #region Fields
        private WindowState state; //Состояние окна
        private Visibility visibilityExit; //Отображение выхода
        private Visibility visibilityContent; //Отображение контента
        private Page activePage; //Активная страница
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор для инициализации данных
        /// </summary>
        public MainViewModel()
        {
            State = WindowState.Normal;
            VisibilityExit = Visibility.Hidden;
            VisibilityContent = Visibility.Visible;

            FilesView filesView = new FilesView();
            ActivePage = filesView;
            filesView.DataContext = new FilesViewModel(this);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Функциональность кнопок окна
        /// </summary>
        public RelayCommand WindowButton_Click => new RelayCommand(obj =>
        {
            switch (obj)
            {
                case "Minimized":
                    {
                        State = WindowState.Minimized;
                        break;
                    }
                case "Maximized":
                    {
                        State = State == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                        break;
                    }
                case "CloseWindow":
                    {
                        VisibilityContent = Visibility.Hidden;
                        VisibilityExit = Visibility.Visible;
                        break;
                    }
            }
        });
        /// <summary>
        /// Функциональность кнопок выхода из программы
        /// </summary>
        public RelayCommand ExitButton_Click => new RelayCommand(obj =>
        {
            switch (obj)
            {
                case "Yes":
                    {
                        Application.Current.Shutdown();
                        break;
                    }
                case "No":
                    {
                        VisibilityExit = Visibility.Hidden;
                        VisibilityContent = Visibility.Visible;
                        break;
                    }
            }
        });
        #endregion

        #region Methods
        /// <summary>
        /// Состояние окна
        /// </summary>
        public WindowState State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged(nameof(state));
            }
        }
        /// <summary>
        /// Отображение выхода
        /// </summary>
        public Visibility VisibilityExit
        {
            get => visibilityExit;
            set
            {
                visibilityExit = value;
                OnPropertyChanged(nameof(visibilityExit));
            }
        }
        /// <summary>
        /// Отображение контента
        /// </summary>
        public Visibility VisibilityContent
        {
            get => visibilityContent;
            set
            {
                visibilityContent = value;
                OnPropertyChanged(nameof(visibilityContent));
            }
        }
        /// <summary>
        /// Активная страница
        /// </summary>
        public Page ActivePage
        {
            get => activePage;
            set
            {
                activePage = value;
                OnPropertyChanged(nameof(activePage));
            }
        }
        #endregion
    }
}