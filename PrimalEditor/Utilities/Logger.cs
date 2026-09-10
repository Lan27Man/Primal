// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace PrimalEditor.Utilities
{
    public static class Logger
    {
        #region Fields

        private static int _messageFilter = (int)(MessageType.Info | MessageType.Warning | MessageType.Error);

        private static readonly ObservableCollection<LogMessage> _messages = new ObservableCollection<LogMessage>();

        #endregion

        #region Properties

        public static ReadOnlyObservableCollection<LogMessage> Messages { get; } = new ReadOnlyObservableCollection<LogMessage>(_messages);

        public static CollectionViewSource FilteredMessages { get; } = new CollectionViewSource() { Source = Messages };

        #endregion

        #region Constructor

        static Logger()
        {
            FilteredMessages.Filter += (s, e) =>
            {
                var type = (int)(e.Item as LogMessage).MessageType;
                e.Accepted = (type & _messageFilter) != 0;
            };
        }

        #endregion

        #region Methods

        public static async void Log(MessageType type, string msg, [CallerFilePath]string file = "", [CallerMemberName]string caller = "", [CallerLineNumber]int line = 0)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Add(new LogMessage(type, msg, file, caller, line));
            }));
        }

        public static async void Clear()
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Clear();
            }));
        }

        public static void SetMessageFilter(int mask)
        {
            _messageFilter = mask;
            FilteredMessages.View.Refresh();
        }

        #endregion
    }
}
