// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Windows.Input;

namespace PrimalEditor.Common
{
    public class RelayCommand<T> : ICommand
    {
        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Fields

        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        #endregion

        #region Constructor

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region Methods

        public void Execute(object parameter)
        {
            _execute.Invoke((T)parameter);
        }

        #endregion

        #region Functions

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        #endregion
    }
}
