// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Controls;

namespace PrimalEditor.Utilities
{
    /// <summary>
    /// Interaction logic for LoggerView.xaml
    /// </summary>
    public partial class LoggerView : UserControl
    {
        #region Constructor

        public LoggerView()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            Logger.Clear();
        }

        private void MessageFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var filter = 0x0;

            if (toggleInfo.IsChecked == true)
            {
                filter |= (int)MessageType.Info;
            }

            if (toggleWarning.IsChecked == true)
            {
                filter |= (int)MessageType.Warning;
            }

            if (toggleError.IsChecked == true)
            {
                filter |= (int)MessageType.Error;
            }

            Logger.SetMessageFilter(filter);
        }

        #endregion
    }
}
