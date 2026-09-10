// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace PrimalEditor.EngineAPIStructs
{
    [StructLayout(LayoutKind.Sequential)]
    internal class GameEntityDescriptor
    {
        #region Fields

        public TransformComponent Transform = new TransformComponent();

        #endregion

        #region Constructor

        public GameEntityDescriptor()
        {

        }

        #endregion
    }
}
