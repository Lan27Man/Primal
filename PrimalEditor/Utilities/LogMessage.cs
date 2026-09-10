// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.IO;

namespace PrimalEditor.Utilities
{
    public class LogMessage
    {
        #region Properties

        public DateTime Time { get; }

        public MessageType MessageType { get; }

        public string Message { get; }

        public string File { get; }

        public string Caller { get; }

        public int Line { get; }

        public string MetaData
        {
            get { return $"{File}: {Caller} ({Line})"; }
        }

        #endregion

        #region Constructor

        public LogMessage(MessageType type, string msg, string file, string caller, int line)
        {
            Time = DateTime.Now;
            MessageType = type;
            Message = msg;
            File = Path.GetFileName(file);
            Caller = caller;
            Line = line;
        }

        #endregion
    }
}
