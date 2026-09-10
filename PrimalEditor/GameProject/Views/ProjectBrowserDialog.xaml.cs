// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Linq;
using System.Windows;

namespace PrimalEditor.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectBrowserDialog.xaml
    /// </summary>
    public partial class ProjectBrowserDialog : Window
    {
        #region Constructor

        public ProjectBrowserDialog()
            : base()
        {
            InitializeComponent();
            Loaded += ProjectBrowserDialog_OnLoaded;
        }

        #endregion

        #region Event Handlers

        private void ProjectBrowserDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ProjectBrowserDialog_OnLoaded;

            if (!OpenProject.Projects.Any())
            {
                openProjectButton.IsEnabled = false;
                openProjectView.Visibility = Visibility.Hidden;

                OnToggleButton_Click(createProjectButton, new RoutedEventArgs());
            }
        }

        private void OnToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == openProjectButton)
            {
                if (createProjectButton.IsChecked == true)
                {
                    createProjectButton.IsChecked = false;
                    browserContent.Margin = new Thickness(0);
                }

                openProjectButton.IsChecked = true;
            }
            else
            {
                if (openProjectButton.IsChecked == true)
                {
                    openProjectButton.IsChecked = false;
                    browserContent.Margin = new Thickness(-800, 0, 0, 0);
                }

                createProjectButton.IsChecked = true;
            }
        }

        #endregion
    }
}
