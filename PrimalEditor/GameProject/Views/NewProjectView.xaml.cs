// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Controls;

namespace PrimalEditor.GameProject
{
    /// <summary>
    /// Interaction logic for NewProjectView.xaml
    /// </summary>
    public partial class NewProjectView : UserControl
    {
        #region Constructor

        public NewProjectView()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as NewProject;
            var projectPath = vm.CreateProject(templateListBox.SelectedItem as ProjectTemplate);
            var dialogResult = false;
            var win = Window.GetWindow(this);

            if (!string.IsNullOrEmpty(projectPath))
            {
                dialogResult = true;

                var project = OpenProject.Open(
                    new ProjectData()
                    {
                        ProjectName = vm.ProjectName,
                        ProjectPath = projectPath
                    }
                );

                win.DataContext = project;
            }

            win.DialogResult = dialogResult;
            win.Close();
        }

        #endregion
    }
}
