// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Windows;

using PrimalEditor.GameProject;

namespace PrimalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
            : base()
        {
            InitializeComponent();
            Loaded += MainWindow_OnLoaded;
            Closing += MainWindow_OnClosing;
        }

        #endregion

        #region Event Handlers

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_OnLoaded;
            OpenProjectBrowserDialog();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Closing -= MainWindow_OnClosing;
            Project.Current?.Unload();
        }

        #endregion

        #region Methods

        private void OpenProjectBrowserDialog()
        {
            var projectBrowser = new ProjectBrowserDialog();

            if (projectBrowser.ShowDialog() == false || projectBrowser.DataContext == null)
            {
                Application.Current.Shutdown();
            }
            else
            {
                Project.Current?.Unload();
                DataContext = projectBrowser.DataContext;
            }
        }

        #endregion
    }
}
