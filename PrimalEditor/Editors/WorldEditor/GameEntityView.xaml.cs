// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using PrimalEditor.Components;
using PrimalEditor.GameProject;
using PrimalEditor.Utilities;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for GameEntityView.xaml
    /// </summary>
    public partial class GameEntityView : UserControl
    {
        #region Fields

        private Action _undoAction;

        private string _propertyName;

        #endregion

        #region Properties

        public static GameEntityView Instance { get; private set; }

        #endregion

        #region Constructor

        public GameEntityView()
            : base()
        {
            InitializeComponent();
            DataContext = null;
            Instance = this;

            DataContextChanged += (s, e) =>
            {
                if (DataContext != null)
                {
                    (DataContext as MSEntity).PropertyChanged += (s, e) =>
                    {
                        _propertyName = e.PropertyName;
                    };
                }
            };
        }

        #endregion

        #region Event Handlers

        private void NameTextBox_OnKeyboardGotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _undoAction = GetRenameAction();
        }

        private void NameTextBox_OnKeyboardLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (_propertyName == nameof(MSEntity.Name) && _undoAction != null)
            {
                var redoAction = GetRenameAction();

                Project.UndoRedo.Add(
                    new UndoRedoAction(
                        _undoAction,
                        redoAction,
                        "Rename game entity"
                    )
                );

                _propertyName = null;
            }

            _undoAction = null;
        }

        private void IsEnabledCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            var undoAction = GetIsEnabledAction();
            var vm = DataContext as MSEntity;
            vm.IsEnabled = (sender as CheckBox).IsChecked == true;

            var redoAction = GetIsEnabledAction();

            Project.UndoRedo.Add(
                new UndoRedoAction(
                    undoAction,
                    redoAction,
                    vm.IsEnabled == true ? "Enable game entity" : "Disable game entity"
                )
            );
        }

        #endregion

        #region Functions

        private Action GetRenameAction()
        {
            var vm = DataContext as MSEntity;
            var selection = vm.SelectedEntities.Select(entity => (entity, entity.Name)).ToList();

            return new Action(() =>
            { 
                selection.ForEach(item => item.entity.Name = item.Name);
                (DataContext as MSEntity).Refresh();
            });
        }

        private Action GetIsEnabledAction()
        {
            var vm = DataContext as MSEntity;
            var selection = vm.SelectedEntities.Select(entity => (entity, entity.IsEnabled)).ToList();

            return new Action(() =>
            { 
                selection.ForEach(item => item.entity.IsEnabled = item.IsEnabled);
                (DataContext as MSEntity).Refresh();
            });
        }

        #endregion
    }
}
