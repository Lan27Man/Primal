// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Linq;
using System.Windows;
using System.Windows.Controls;

using PrimalEditor.Components;
using PrimalEditor.GameProject;
using PrimalEditor.Utilities;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for ProjectLayoutView.xaml
    /// </summary>
    public partial class ProjectLayoutView : UserControl
    {
        #region Constructor

        public ProjectLayoutView()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void AddGameEntityButton_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = btn.DataContext as Scene;
            vm.AddGameEntityCommand.Execute(
                new GameEntity(vm)
                {
                    Name = "Empty Game Entity"
                }
            );
        }

        private void GameEntitiesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var newSelection = listBox.SelectedItems.Cast<GameEntity>().ToList();
            var previousSelection = newSelection.Except(e.AddedItems.Cast<GameEntity>()).Concat(e.RemovedItems.Cast<GameEntity>()).ToList();

            Project.UndoRedo.Add(
                new UndoRedoAction(
                    () => // Undo action
                    {
                        listBox.UnselectAll();
                        previousSelection.ForEach(x => (listBox.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                    },
                    () => // Redo action
                    {
                        listBox.UnselectAll();
                        newSelection.ForEach(x => (listBox.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                    },
                    "Selection Changed"
                )
            );

            MSGameEntity msEntity = null;

            if (newSelection.Any())
            {
                msEntity = new MSGameEntity(newSelection);
            }

            GameEntityView.Instance.DataContext = msEntity;
        }

        #endregion
    }
}
