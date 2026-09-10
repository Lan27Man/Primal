// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

using PrimalEditor.GameProject;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for WorldEditorView.xaml
    /// </summary>
    public partial class WorldEditorView : UserControl
    {
        #region Constructor

        public WorldEditorView()
            : base()
        {
            InitializeComponent();
            Loaded += WorldEditorView_OnLoaded;
        }

        #endregion

        #region Event Handlers

        private void WorldEditorView_OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= WorldEditorView_OnLoaded;
            Focus();

            ((INotifyCollectionChanged)Project.UndoRedo.UndoList).CollectionChanged += (s, e) => Focus();
        }

        #endregion
    }
}
