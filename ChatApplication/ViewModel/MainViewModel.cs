using System.Windows;
using System.Windows.Input;

namespace ChatApplication.ViewModel
{
    class MainViewModel :BaseViewModel
    {
        #region Private Member
        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;
        /// <summary>
        /// The radius of thhe edges of the window
        /// </summary>
        private int mWindowRadius = 10;
        #endregion

        #region Public Property
        /// <summary>
        /// The window minimum width size
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The window minimum height
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;
        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;
        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        /// <summary>
        /// The height of the title bar /caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);
        /// <summary>
        /// The paddig of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding => new Thickness(ResizeBorder);
        #endregion

        #region Command 
        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }
        #endregion

        #region Contructor
        public MainViewModel(Window window)
        {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };


            // Create Commands

            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, window.PointToScreen(Mouse.GetPosition(window))));

            var resizer = new WindowResizer(mWindow);
        }
        #endregion
    }
}
