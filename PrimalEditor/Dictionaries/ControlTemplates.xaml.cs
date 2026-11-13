// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimalEditor.Dictionaries
{
    public partial class ControlTemplates : ResourceDictionary
    {
        #region Constructor

        public ControlTemplates()
            : base()
        {

        }

        #endregion

        #region Event Handlers

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var exp = textBox.GetBindingExpression(TextBox.TextProperty);

            if (exp == null)
            {
                return;
            }

            if (e.Key == Key.Enter)
            {
                if (textBox.Tag is ICommand command && command.CanExecute(textBox.Text))
                {
                    command.Execute(textBox.Text);
                }
                else
                {
                    exp.UpdateSource();
                }

                Keyboard.ClearFocus();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                exp.UpdateTarget();
                Keyboard.ClearFocus();
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }

        private void MaximizeRestoreButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = (window.WindowState == WindowState.Normal) ?
                WindowState.Maximized : WindowState.Normal;
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
